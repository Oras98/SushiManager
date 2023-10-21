using SushiManager.Exceptions;
using SushiManager.Services;
using SushiRestaurant.Models;

namespace SushiManager.Factories.OrderRetrieverFactory
{
    public abstract class OrderRetriever
    {        
        ApplicationDbContext _dbContext;
        public OrderRetriever(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public abstract Order GetOrder();
    }
}
