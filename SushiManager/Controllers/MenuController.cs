using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using SushiRestaurant.Data;
using SushiRestaurant.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Web;

namespace SushiRestaurant.Controllers
{
    [Authorize(Roles = "Table")]
    public class MenuController : Controller
    {
        ApplicationDbContext _dbContext;
        Utility _utility;
        public MenuController(ApplicationDbContext dbContext, Utility utility)
        {
            _dbContext = dbContext;
            _utility = utility;
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
            orderDetail.ProductName = _utility.getProductName(orderDetail.ProductId);

            var products = new List<OrderDetail>() { orderDetail };
            var raw_producsts = Request.Cookies["cartProducts"];
            
            if (raw_producsts != null)
            {
                products = JsonConvert.DeserializeObject<List<OrderDetail>>(raw_producsts);

                var same_product = products.Find(x => x.ProductId == orderDetail.ProductId);

                if (same_product != null)
                    same_product.Quantity += orderDetail.Quantity;
                else
                    products.Add(orderDetail);                
            }
            
            Response.Cookies.Append("cartProducts", JsonConvert.SerializeObject(products));

            return RedirectToAction("Index");
        }

        public IActionResult EditItem(int productID)
        {
            var raw_producsts = Request.Cookies["cartProducts"];
            var products = JsonConvert.DeserializeObject<List<OrderDetail>>(raw_producsts);
            var product_to_edit = products.Find(product => product.ProductId == productID);

            return View(product_to_edit);
        }

        [HttpPost]
        public IActionResult EditItem(OrderDetail orderDetail)
        {
            var raw_producsts = Request.Cookies["cartProducts"];
            var products = JsonConvert.DeserializeObject<List<OrderDetail>>(raw_producsts);
            var product_to_edit_index = products.FindIndex(product => product.ProductId == orderDetail.ProductId);

            products[product_to_edit_index].Quantity = orderDetail.Quantity;

            Response.Cookies.Append("cartProducts", JsonConvert.SerializeObject(products));

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult DeleteItem(int productID)
        {
            var raw_producsts = Request.Cookies["cartProducts"];
            var products = JsonConvert.DeserializeObject<List<OrderDetail>>(raw_producsts);
            var product_to_delete = products.Find(product => product.ProductId == productID);

            return View(product_to_delete);
        }

        [HttpPost]
        public IActionResult DeleteItem(OrderDetail orderDetail)
        {
            var raw_producsts = Request.Cookies["cartProducts"];
            var products = JsonConvert.DeserializeObject<List<OrderDetail>>(raw_producsts);
            products = products.Where(product => product.ProductId != orderDetail.ProductId).ToList();

            Response.Cookies.Append("cartProducts", JsonConvert.SerializeObject(products));

            return RedirectToAction("Index", "Cart");
        }
    }
}
