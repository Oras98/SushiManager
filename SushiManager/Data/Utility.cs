namespace SushiRestaurant.Data
{
    public class Utility
    {
        ApplicationDbContext _dbContext;
        public Utility(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string getProductName(int productID)
        {
            var product = _dbContext.Products.FirstOrDefault(product => product.Id == productID);

            return product.Name;            
        }
    }
}
