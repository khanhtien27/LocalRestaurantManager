using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO;
using ManagementRestaurentWeb.Service.IService;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using Unitity;

namespace ManagementRestaurentWeb.Service
{
    public class CategoryService : BaseService, ICategoryService
    {
        private string cateURL;
        public CategoryService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            cateURL = configuration.GetValue<string>("ServiceUrls:RestaurantAPI");
        }

        public Task<T> Create<T>(CategoryCreateDTO categoryCreateDTO, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                aPIType = Unitity.SD.APIType.POST,
                URL = cateURL + "/API/Category",
                Data = categoryCreateDTO,
                Token = token
            });
        }

        public Task<T> DeleteById<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.DELETE,
                URL = cateURL + "/API/Category/Id?Id=" + Id,
                Token = token
            });
        }

        public Task<T> GetAll<T>( string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.GET,
                URL = cateURL + "/API/Category/",
                Token = token
            });
        }

        public Task<T> GetById<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIType = SD.APIType.GET,
                URL = cateURL + "/API/Category/Id?Id=" + Id,
                Token = token
            });
        }

        public Task<T> Update<T>(CategoryUpdateDTO categoryUpdateDTO, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                aPIType = Unitity.SD.APIType.PUT,
                URL = cateURL + "/API/Category/Id?Id=" + categoryUpdateDTO.Id,
                Data = categoryUpdateDTO,
                Token = token
            });
        }
    }
}
