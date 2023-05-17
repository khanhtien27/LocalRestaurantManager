using ManagementRestaurantLocation.Models;

namespace ManagementRestaurentWeb.Service.IService
{
    public interface IBaseService
    {
        APIRespone APIRespone { get; set; }
        Task<T> SendAsync<T>();
    }
}
