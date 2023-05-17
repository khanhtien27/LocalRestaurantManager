using Microsoft.AspNetCore.Mvc;
using static Restaurent_Utinity.SD;

namespace ManagementRestaurentWeb.Models
{
    public class APIRequest
    {
        public APIType aPIType;
        public string URL { get; set; }
        public object Data { get; set; }
    }
}
