using System.Net;

namespace ManagementRestaurentWeb.Models
{
    public class APIRespone
    {
        public APIRespone()
        {
            ErrorsMessge = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorsMessge { get; set; }
        public object Result { get; set; }
    }
}
