using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using SushiRestaurant.Models;
using Microsoft.EntityFrameworkCore;
using SushiManager.Factories.OrderRetrieverFactory;
using SushiManager.Exceptions;
using SushiManager.Data;
using SushiManager.Services;

namespace SushiRestaurant.Controllers
{
    [Authorize(Roles = "Table")]
    public class MenuController : Controller
    {
        ApplicationDbContext _dbContext;
        Utility _utility;
        OrderRetrieverFactory _orderRetrieverFactory;
        public MenuController(ApplicationDbContext dbContext, Utility utility, OrderRetrieverFactory orderRetrieverFactory)
        {
            _dbContext = dbContext;
            _utility = utility;
            _orderRetrieverFactory = orderRetrieverFactory;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();

            return View(products);
        }
        
        public IActionResult AddItem(int productId) 
        { 
            var order_detail = new OrderDetail() { ProductId = productId };

            return View(order_detail);
        }

        [HttpPost]        
        public IActionResult AddItem(OrderDetail orderDetail)
        {
            var user = _utility.GetSessionUser(User);

            var orderRetriever = _orderRetrieverFactory.GetOrderRetriever(user.Id);
            var order = orderRetriever.GetOrder();

            var order_manager = new OrderManager(_dbContext, order);
            order_manager.AddOrderItem(orderDetail);

            return RedirectToAction("Index");
        }

        public IActionResult EditItem(int order_detail_id)
        {
            if (_dbContext.OrderDetails == null)            
                return NotFound();            

            var order_detail = _dbContext.OrderDetails.Find(order_detail_id);

            if (order_detail == null)            
                return NotFound();           

            return View(order_detail);
        }

        [HttpPost]
        public IActionResult EditItem(OrderDetail orderDetail)
        {
            try
            {
                var orderRetriever = _orderRetrieverFactory.GetOrderRetriever(orderDetail);
                var order = orderRetriever.GetOrder();

                var order_manager = new OrderManager(_dbContext, order);
                order_manager.EditOrderItem(orderDetail);
            }
            catch (OrderDeletedException)
            {
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailNotFound(orderDetail.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Cart");
        }

        private bool OrderDetailNotFound(int id)
        {
            return (_dbContext.OrderDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult DeleteItem(int order_detail_id)
        {
            if (_dbContext.OrderDetails is null)
                return NotFound();

            var order_detail = _dbContext.OrderDetails.Find(order_detail_id);

            if (order_detail is null)
                return NotFound();

            return View(order_detail);
        }

        [HttpPost, ActionName("DeleteItem")]
        public IActionResult DeleteItemConfirmed(int order_detail_id)
        {
            try
            {
                var user = _utility.GetSessionUser(User);

                var orderRetriever = _orderRetrieverFactory.GetOrderRetriever(user.Id);
                var order = orderRetriever.GetOrder();

                var order_manager = new OrderManager(_dbContext, order);
                order_manager.DeleteOrderItem(order_detail_id);

                return RedirectToAction("Index", "Cart");
            }
            catch (Exception err)
            {
                return Problem(err.Message);
            }
        }

        [HttpPost]
        public IActionResult SubmitOrder(int orderID)
        {
            try
            {
                var order = _dbContext.Orders.Find(orderID);
                var order_manager = new OrderManager(_dbContext, order);
                order_manager.SubmitOrder();

                return RedirectToAction("Index", "Menu");
            }
            catch (OrderDeletedException)
            {
                return NotFound();
            }
        }
    }
}
