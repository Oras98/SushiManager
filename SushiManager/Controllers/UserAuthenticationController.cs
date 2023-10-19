using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SushiRestaurant.Data;
using SushiRestaurant.Exceptions;
using SushiRestaurant.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SushiRestaurant.Controllers
{
    public class UserAuthenticationController : Controller
    {
        ApplicationDbContext _dbContext;
        IConfiguration _config;
        public UserAuthenticationController(ApplicationDbContext dbContext, IConfiguration config)
        {                
            _dbContext = dbContext;
            _config = config;
        }

        [AllowAnonymous]
        public IActionResult Login(string? ReturnUrl)
        {            
            if (ReturnUrl is not null)
            {
                TempData["AlertMessage"] = "Per effettuare questa operazione devi prima effettuare l'accesso";
                TempData["returnUrl"] = ReturnUrl;
            }                

            var user_login_model = new UserLogin();

            return View(user_login_model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserLogin userLogin) 
        { 
            try
            {
                var user = authenticate(userLogin);
                var generated_token = generate(user);
                addTokenToCookies(generated_token);                

                TempData["AlertMessage"] = "Login effettuato!";

                if (TempData.ContainsKey("returnUrl"))
                    return Redirect((string)TempData["returnUrl"]);
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (UserNotFound ex) 
            {
                ModelState.AddModelError("Username", ex.Message);
                ModelState.AddModelError("Password", ex.Message);

                return View(userLogin);
            }            
            catch 
            {
                TempData["AlertMessage"] = "Si è verificato un errore in fase di log in, riprova più tardi!";

                return RedirectToAction("Index", "Home");
            }
        }

        User authenticate(UserLogin userLogin)
        {
            var user = _dbContext.User.FirstOrDefault(user => user.Username == userLogin.Username &&
                user.Password == userLogin.Password);

            if (user == null)
                throw new UserNotFound();

            return user;
        }

        string generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:MinutesBeforeExpires"])), 
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        void addTokenToCookies(string token)
        {
            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:MinutesBeforeExpires"]))
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            removeTokenFromCookies();
            return RedirectToAction("Index", "Home");
        }

        void removeTokenFromCookies()
        {
            Response.Cookies.Delete("token");
        }
    }
}
