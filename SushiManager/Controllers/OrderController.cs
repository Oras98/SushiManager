using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SushiManager.Factories.OrderRetrieverFactory;
using SushiManager.Services;

namespace SushiManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        ApplicationDbContext _dbContext;
        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var orders = _dbContext.Orders
                .Where(order => order.SubmitDate != null)
                .OrderByDescending(order => order.SubmitDate)
                .Include(order => order.User)
                .ToArray();

            return View(orders);
        }

        [HttpGet]
        public IActionResult Details([FromQuery] int orderID)
        { 
            var order_details = _dbContext.OrderDetails.
                Where(detail => detail.OrderId == orderID)
                .Include(detail => detail.Product)
                .Include(detail => detail.Order)                
                .Include(detail => detail.Order.User)                
                .ToArray();

            return PartialView(order_details);
        }
    }
}
