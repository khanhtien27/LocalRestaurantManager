using AutoMapper;
using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO;
using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO;
using ManagementRestaurentWeb.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurentWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Unitity;

namespace ManagementRestaurentWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestaurentService _restaurentService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public HomeController(IRestaurentService restaurentService, IProductService productService, ICategoryService categoryService, IMapper mapper )
        {
            _restaurentService = restaurentService;
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<RetaurantDTO> restaurentList = new List<RetaurantDTO>();

            var respone = await _restaurentService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess)
            {
                restaurentList = JsonConvert.DeserializeObject<List<RetaurantDTO>>(Convert.ToString(respone.Result));

                return View(restaurentList);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Detail (int Id)
        {
            List<ProductDTO> listproduct = new List<ProductDTO>();
            var respone = await _productService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(respone.Result));
                listproduct = model.Where(i => i.RetaurentID == Id).ToList();
                return View(listproduct);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> CateList (int Id)
        {
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            var respone = await _categoryService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(respone.Result));
                categoryDTOs = model.Where(i => i.ProductID == Id).ToList();
                return View(categoryDTOs);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}