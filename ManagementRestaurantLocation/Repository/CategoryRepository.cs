using ManagementRestaurantLocation.Data;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Repository.IRepository;

namespace ManagementRestaurantLocation.Repository
{
    public class CategoryRepository : Repository<Categories>, ICategoryRepository
    {
        public CategoryRepository(RestaurentDbContext context) : base(context)
        {
        }
    }
}
