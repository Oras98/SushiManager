using Microsoft.EntityFrameworkCore;
using SushiManager.Exceptions;
using SushiManager.Services;
using SushiRestaurant.Models;

namespace SushiManager.Factories.OrderRetrieverFactory
{
    internal class OrderRetrieverFromUser : OrderRetriever
    {
        int _userID;
        ApplicationDbContext _dbContext;
        internal OrderRetrieverFromUser(ApplicationDbContext dbContext, int userId) : base(dbContext)
        {
            _dbContext = dbContext;
            _userID = userId;
        }

        public override Order GetOrder()
        {
            try
            {
                return getActiveOrder();
            }
            catch (NoActiveOrderFound)
            {
                return createNewOrder();
            }
        }

        private Order getActiveOrder()
        {
            var order = _dbContext.Orders.FirstOrDefault(order => order.UserId == _userID && order.SubmitDate == null);

            if (order is null)
                throw new NoActiveOrderFound();

            return order;
        }

        private Order createNewOrder()
        {
            var order = new Order()
            {
                UserId = _userID,
                CreateDate = DateTime.Now
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order;
        }
    }
}
