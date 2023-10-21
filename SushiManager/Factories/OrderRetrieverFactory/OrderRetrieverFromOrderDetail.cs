using SushiManager.Exceptions;
using SushiManager.Services;
using SushiRestaurant.Models;

namespace SushiManager.Factories.OrderRetrieverFactory
{
    internal class OrderRetrieverFromOrderDetail : OrderRetriever
    {
        OrderDetail _orderDetail;
        ApplicationDbContext _dbContext;
        internal OrderRetrieverFromOrderDetail(ApplicationDbContext dbContext, OrderDetail orderDetail) : base(dbContext)
        {
            _dbContext = dbContext;
            _orderDetail = orderDetail;
        }

        public override Order GetOrder()
        {
            return getActiveOrder();
        }

        private Order getActiveOrder()
        {
            var order = _dbContext.Orders.FirstOrDefault(order => order.Id == _orderDetail.OrderId);

            if (order is null)
                throw new OrderDeletedException();

            return order;
        }
    }
}
