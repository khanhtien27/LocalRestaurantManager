using Microsoft.AspNetCore.Mvc;
using static Unitity.SD;

namespace ManagementRestaurentWeb.Models
{
    public class APIRequest
    {
        public APIType aPIType;
        public string URL { get; set; }
        public object Data { get; set; }
    }
}
