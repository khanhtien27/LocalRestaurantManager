using AutoMapper;
using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO;
using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO.ProductListRestaurent;
using ManagementRestaurentWeb.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurentWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using Unitity;

namespace ManagementRestaurentWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IRestaurentService _restaurentService;
        public ProductController(IProductService productService, IMapper mapper, IRestaurentService restaurentService)
        {
            _mapper = mapper;
            _productService = productService;
            _restaurentService = restaurentService;
        }

        [Authorize]
        public async Task<IActionResult> Index ()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            var respone = await _productService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(respone.Result));

                return View(products);
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexID(int Id)
        {
            List<ProductDTO> products = new List<ProductDTO>();
            var respone = await _productService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(respone.Result));

                List<ProductDTO> model = products.Where(i => i.RetaurentID == Id).ToList();
                return View(model);
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create (int Id)
        {
            ProductListRestaurent productListRestaurent = new ProductListRestaurent();

            var respone = await _restaurentService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess)
            {
                productListRestaurent.restaurentList = JsonConvert.DeserializeObject<List<RetaurantDTO>>
                    (Convert.ToString(respone.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            productListRestaurent.ProductCreatDTO.RetaurentID = Id;
            return View(productListRestaurent);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (ProductListRestaurent productListRestaurent)
        {
            if(ModelState.IsValid)
            {
                var respone = await _productService.Create<APIRespone>(productListRestaurent.ProductCreatDTO, HttpContext.Session.GetString(SD.SessionToken));

                var id = productListRestaurent.ProductCreatDTO.RetaurentID;
                
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Create successfully";
                    return RedirectToAction("IndexID", "Product", new { Id = id });
                }
                else
                {
                    if(respone.ErrorsMessge.Count != 0)
                    {
                        ModelState.AddModelError("ErrorMessage", respone.ErrorsMessge.FirstOrDefault());
                    }
                }
            }
            var rsp = await _restaurentService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (rsp != null && rsp.IsSuccess)
            {
                productListRestaurent.restaurentList = JsonConvert.DeserializeObject<List<RetaurantDTO>>
                    (Convert.ToString(rsp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(productListRestaurent);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update (int Id)
        {
            ProductUpdateListRestaurent productListRestaurent = new ProductUpdateListRestaurent();
            var respone = await _productService.GetById<APIRespone>(Id, HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess) 
            { 
                var model = JsonConvert.DeserializeObject<Products>(Convert.ToString(respone.Result));
                productListRestaurent.productUpdateDTO = _mapper.Map<ProductUpdateDTO>(model);
            }

            var rsp = await _restaurentService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if(rsp != null && rsp.IsSuccess)
            {
                productListRestaurent.restaurentList = JsonConvert.DeserializeObject<List<RetaurantDTO>>
                    (Convert.ToString (rsp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(productListRestaurent);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update (ProductUpdateListRestaurent productUpdateListRestaurent)
        {
            if (ModelState.IsValid)
            {
                var respone = await _productService.Update<APIRespone>(productUpdateListRestaurent.productUpdateDTO, HttpContext.Session.GetString(SD.SessionToken));
                if(respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Update successfully";
                    return RedirectToAction("IndexRestaurent", "Restaurent");
                }
                else
                {
                    if(respone.ErrorsMessge.Count > 0)
                    {
                        ModelState.AddModelError("ErrorsMessage", respone.ErrorsMessge.FirstOrDefault());
                    }
                }
            }

            var rsp = await _restaurentService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (rsp != null && rsp.IsSuccess)
            {
                productUpdateListRestaurent.restaurentList = JsonConvert.DeserializeObject<List<RetaurantDTO>>
                    (Convert.ToString(rsp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(productUpdateListRestaurent);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete (int Id)
        {
            var respone = await _productService.GetById<APIRespone>(Id, HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductDTO productDTO)
        {
            var respone = await _productService.DeleteById<APIRespone>(productDTO.Id, HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess)
            {
                TempData["success"] = "Delete successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }
    }
}
