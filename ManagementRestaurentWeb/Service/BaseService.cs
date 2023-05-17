using ManagementRestaurantLocation.Models;
using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Service.IService;
using Newtonsoft.Json;
using Restaurent_Utinity;
using System;
using System.Text;

namespace ManagementRestaurentWeb.Service
{
    public class BaseService : IBaseService
    {
        public APIRespone aPIRespone { get; set; }
        public IHttpClientFactory HttpClientFactory { get; set; }
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.aPIRespone = new();
            this.HttpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest aPIRequest)
        {
            try
            {
                var client = HttpClientFactory.CreateClient("KhanhTien");

                HttpRequestMessage requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.RequestUri = new Uri(aPIRequest.URL);
                if (aPIRequest.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(aPIRequest.Data), Encoding.UTF8, "application/json");
                }

                switch (aPIRequest.aPIType)
                {
                    case SD.APIType.POST:
                        requestMessage.Method = HttpMethod.Post;
                        break;
                    case SD.APIType.DELETE:
                        requestMessage.Method = HttpMethod.Delete;
                        break;
                    case SD.APIType.PUT:
                        requestMessage.Method = HttpMethod.Put;
                        break;
                    default:
                        requestMessage.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage responseMessage = null;
                responseMessage = await client.SendAsync(requestMessage);
                var aPIContent = await responseMessage.Content.ReadAsStringAsync();

                try
                {
                    APIRespone APIRespone = JsonConvert.DeserializeObject<APIRespone>(aPIContent);
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest || responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        APIRespone.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        APIRespone.IsSuccess = false;
                        var resp = JsonConvert.SerializeObject(APIRespone);
                        var returnResp = JsonConvert.DeserializeObject<T>(resp);
                        return returnResp;
                    }
                }
                catch (Exception ex)
                {
                    var exRespone = JsonConvert.DeserializeObject<T>(aPIContent);
                    return exRespone;
                }

                var respone = JsonConvert.DeserializeObject<T>(aPIContent);
                return respone;
            }

            catch (Exception ex)
            {
                var dto = new APIRespone
                {
                    ErrorsMessge = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var resp = JsonConvert.SerializeObject(dto);
                var respone = JsonConvert.DeserializeObject<T>(resp);
                return respone;
            }

        }
    }
}