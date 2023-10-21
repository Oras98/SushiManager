using SushiRestaurant.Models;
using System.Security.Claims;

namespace SushiManager.Services
{
    public class Utility
    {
        ApplicationDbContext _dbContext;
        public Utility(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string getProductName(int productID)
        {
            var product = _dbContext.Products.FirstOrDefault(product => product.Id == productID);

            return product.Name;
        }

        public User GetSessionUser(ClaimsPrincipal claimsPrincipal)
        {
            var session_username = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var user = _dbContext.User.First(user => user.Username == session_username);

            return user;
        }
    }
}
