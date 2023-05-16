using ManagementRestaurantLocation.Data;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Repository.IRepository;

namespace ManagementRestaurantLocation.Repository
{
    public class RestaurantRepository : Repository<Restaurents>, IRestaurentRepository
    {
        public RestaurantRepository(RestaurentDbContext context) : base(context)
        {
        }
    }
}
