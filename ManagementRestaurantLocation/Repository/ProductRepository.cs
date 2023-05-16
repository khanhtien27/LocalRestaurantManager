using ManagementRestaurantLocation.Data;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Repository.IRepository;

namespace ManagementRestaurantLocation.Repository
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        public ProductRepository(RestaurentDbContext context) : base(context)
        {
        }
    }
}
