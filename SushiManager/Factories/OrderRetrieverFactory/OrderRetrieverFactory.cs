using SushiManager.Services;
using SushiRestaurant.Models;

namespace SushiManager.Factories.OrderRetrieverFactory
{
    public class OrderRetrieverFactory
    {
        ApplicationDbContext _dbContext;
        public OrderRetrieverFactory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderRetriever GetOrderRetriever(int userID)
        {
            return new OrderRetrieverFromUser(_dbContext, userID);
        }

        public OrderRetriever GetOrderRetriever(OrderDetail orderDetail)
        {
            return new OrderRetrieverFromOrderDetail(_dbContext, orderDetail);
        }
    }
}
