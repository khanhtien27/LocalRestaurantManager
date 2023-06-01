using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO;
using ManagementRestaurentWeb.Service.IService;
using Microsoft.Win32;

namespace ManagementRestaurentWeb.Service
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string restaurantUrl;
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration) :base(httpClientFactory)
        {
             _httpClientFactory = httpClientFactory;
            restaurantUrl = configuration.GetValue<string>("ServiceUrls:RestaurantAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIRequest
            {
                aPIType = Unitity.SD.APIType.POST,
                Data = loginRequestDTO,
                URL = restaurantUrl + "/API/UserAuth/Login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO localUserDTO)
        {
            return SendAsync<T>(new APIRequest
            {
                aPIType = Unitity.SD.APIType.POST,
                Data = localUserDTO,
                URL = restaurantUrl + "/API/UserAuth/Register"
            });
        }
    }
}
