using ManagementRestaurantLocation.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementRestaurantLocation.Data
{
    public class RestaurentDbContext : DbContext
    {
        public RestaurentDbContext(DbContextOptions<RestaurentDbContext> options) : base(options)
        {
        }

        public DbSet<Restaurents> Restaurents { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
