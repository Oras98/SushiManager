using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.Models;
using Newtonsoft.Json;

namespace SushiRestaurant.Controllers
{
    [Authorize(Roles = "Table")]
    public class CartController : Controller
    {        
        public IActionResult Index()
        {
            if (Request.Cookies["cartProducts"] is not null)
            {
                var producsts = JsonConvert.DeserializeObject<List<OrderDetail>>(Request.Cookies["cartProducts"]);

                return View(producsts);
            }
            else
                return View(new List<OrderDetail>());
        }
    }
}
