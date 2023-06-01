
using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurentWeb.Service.IService;
using Unitity;

namespace ManagementRestaurentWeb.Service
{
    public class RestaurentService : BaseService, IRestaurentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string RestaurentURL;
        public RestaurentService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            RestaurentURL = configuration.GetValue<string>("ServiceUrls:RestaurantAPI");
        }

        public Task<T> Create<T>(RetaurantCreateDTO retaurantCreateDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.POST,
                Data = retaurantCreateDTO,
                URL = RestaurentURL + "/API/Restaurent",
                Token = token
            }); 
        }

        public Task<T> DeleteById<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.DELETE,
                URL = RestaurentURL + "/API/Restaurent/Id?Id=" + Id,
                Token = token
            });
        }

        public Task<T> GetAll<T>( string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.GET,
                URL = RestaurentURL + "/API/Restaurent/",
                Token = token
            });
        }

        public Task<T> GetById<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.GET,
                URL = RestaurentURL + "/API/Restaurent/Id?Id=" + Id,
                Token = token
            });
        }

        public Task<T> Update<T>(RetaurantUpdateDTO retaurantUpdateDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.PUT,
                Data = retaurantUpdateDTO,
                URL = RestaurentURL + "/API/Restaurent/Id?Id=" + retaurantUpdateDTO.Id,
                Token = token
            });
        }
    }
}
