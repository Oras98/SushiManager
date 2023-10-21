using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SushiManager.Factories.OrderRetrieverFactory;
using SushiManager.Services;

namespace SushiRestaurant.Controllers
{
    [Authorize(Roles = "Table")]
    public class CartController : Controller
    {
        ApplicationDbContext _dbContext;
        Utility _utility;
        OrderRetrieverFactory _orderRetrieverFactory;
        public CartController(ApplicationDbContext dbContext, Utility utility, OrderRetrieverFactory orderRetrieverFactory)
        {
            _dbContext = dbContext;
            _utility = utility;
            _orderRetrieverFactory = orderRetrieverFactory;
        }

        public IActionResult Index()
        {
            var user = _utility.GetSessionUser(User);

            var orderRetriever = _orderRetrieverFactory.GetOrderRetriever(user.Id);
            var order = orderRetriever.GetOrder();
            var order_details = _dbContext.OrderDetails
                .Where(order_detail => order_detail.OrderId == order.Id)
                .Include(order_detail => order_detail.Product)
                .ToArray();            
           
            return View(order_details);
        }
    }
}
