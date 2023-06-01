using ManagementRestaurantLocation.Models;

namespace ManagementRestaurentWeb.Service.IService
{
    public interface IBaseService
    {
        APIRespone aPIRespone { get; set; }
        Task<T> SendAsync<T>(APIRequest aPIRequest);
    }
}
