using Microsoft.EntityFrameworkCore;
using SushiRestaurant.Models;

namespace SushiRestaurant.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {            
        }        

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SushiRestaurant.Models.User> User { get; set; } = default!;
    }
}
