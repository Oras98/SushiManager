using Org.BouncyCastle.Asn1.X509;
using SushiManager.Exceptions;
using SushiManager.Factories.OrderRetrieverFactory;
using SushiManager.Services;
using SushiRestaurant.Models;

namespace SushiManager.Data
{
    public class OrderManager
    {
        ApplicationDbContext _dbContext;
        Order _order;
        public OrderManager(ApplicationDbContext dbContext, Order order)
        {
            _dbContext = dbContext;
            _order = order;
        }

        public void AddOrderItem(OrderDetail orderDetail)
        {
            orderDetail.Order = _order;

            var detail_with_same_product = _dbContext.OrderDetails
                .FirstOrDefault(x => x.OrderId == _order.Id && x.ProductId == orderDetail.ProductId);

            if (detail_with_same_product is null)
                _dbContext.OrderDetails.Add(orderDetail);
            else
                detail_with_same_product.Quantity += orderDetail.Quantity;

            saveOrderChanges();
        }

        public void EditOrderItem(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Update(orderDetail);

            saveOrderChanges();
        }

        public void DeleteOrderItem(int order_detail_id)
        {
            if (_dbContext.OrderDetails is null)
                throw new Exception("Entity set 'ApplicationDbContext.OrderDetails'  is null.");

            var order_detail = _dbContext.OrderDetails.Find(order_detail_id);

            if (order_detail != null)
            {
                _dbContext.OrderDetails.Remove(order_detail);
                saveOrderChanges();
            }            
        }

        private void saveOrderChanges()
        {
            _order.UpdatedDate = DateTime.Now;
            _dbContext.SaveChanges();
        }

        public void SubmitOrder()
        {
            if (_order is null)
                throw new OrderDeletedException();

            if (_order.SubmitDate is null)
            {
                _order.SubmitDate = DateTime.Now;
                _dbContext.SaveChanges();
            }
        }
    }
}
