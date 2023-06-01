using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO;
using ManagementRestaurentWeb.Service.IService;
using Unitity;

namespace ManagementRestaurentWeb.Service
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string productURL;
        public ProductService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            productURL = configuration.GetValue<string>("ServiceUrls:RestaurantAPI");
        }

        public Task<T> Create<T>(ProductCreatDTO productCreatDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.POST,
                Data = productCreatDTO,
                URL = productURL + "/API/Product",
                Token = token
            });
        }

        public Task<T> DeleteById<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.DELETE,
                URL = productURL + "/API/Product/Id?Id=" + Id,
                Token = token
            });
        }

        public Task<T> GetAll<T>( string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.GET,
                URL = productURL + "/API/Product/",
                Token = token
            });
        }

        public Task<T> GetById<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.GET,
                URL = productURL + "/API/Product/Id?Id=" + Id,
                Token = token
            });
        }

        public Task<T> Update<T>(ProductUpdateDTO productUpdateDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.PUT,
                Data = productUpdateDTO,
                URL = productURL + "/API/Product/Id?Id=" + productUpdateDTO.Id,
                Token = token
            });
        }
    }
}
