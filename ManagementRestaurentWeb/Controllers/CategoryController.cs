using AutoMapper;
using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO;
using ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO.CateListProduct;
using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO;
using ManagementRestaurentWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Unitity;

namespace ManagementRestaurentWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IProductService productService, IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mapper = mapper;
        }
        [Authorize]
        public async Task<IActionResult> Index(int? Id)
        {
            List<CategoryDTO> listcate = new List<CategoryDTO>();
            if (Id == null)
            {
                var respone = await _categoryService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    listcate = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(respone.Result));
                    return View(listcate);
                }
                return View();
            }
            else
            {
                var respone = await _categoryService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    listcate = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(respone.Result));
                    var model = listcate.Where(i => i.ProductID == Id).ToList();
                    return View(model);
                }
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(int Id)
        {
            CateListProductCreate cateListProductCreate = new CateListProductCreate();
            var respone = await _productService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                cateListProductCreate.productList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(respone.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            cateListProductCreate.categoryCreateDTO.ProductID = Id;
            return View(cateListProductCreate);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CateListProductCreate cateListProductCreate)
        {
            if (ModelState.IsValid)
            { 
                var respone = await _categoryService.Create<APIRespone>(cateListProductCreate.categoryCreateDTO, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Created successfully";
                    return RedirectToAction("Index", "Category", new { Id = cateListProductCreate.categoryCreateDTO.ProductID });
                }
            }

            var rsp = await _productService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (rsp != null && rsp.IsSuccess)
            {
                cateListProductCreate.productList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(rsp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            return View(cateListProductCreate);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int Id)
        {
            CateListProductUpdate cateListProductUpdate = new CateListProductUpdate();
            var respone = await _categoryService.GetById<APIRespone>(Id, HttpContext.Session.GetString(SD.SessionToken));
            var rsp = await _productService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess && rsp != null && rsp.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<Categories>(Convert.ToString(respone.Result));
                cateListProductUpdate.categoryUpdateDTO = _mapper.Map<CategoryUpdateDTO>(model);
                cateListProductUpdate.productList = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(rsp.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(cateListProductUpdate);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CateListProductUpdate cateListProductUpdate)
        {
            if (ModelState.IsValid)
            {
                var respone = await _categoryService.Update<APIRespone>(cateListProductUpdate.categoryUpdateDTO, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    TempData["success"] = "Created successfully";
                    return RedirectToAction("Index", "Category", new { Id = cateListProductUpdate.categoryUpdateDTO.ProductID });
                }
                else
                {
                    if (respone.ErrorsMessge.Count > 0)
                    {
                        ModelState.AddModelError("Errors", respone.ErrorsMessge.FirstOrDefault());
                    }
                }
            }
            var rsp = await _productService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));
            if (rsp != null && rsp.IsSuccess)
            {
                cateListProductUpdate.productList = JsonConvert.DeserializeObject<List<ProductDTO>>
                    (Convert.ToString(rsp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            return View(cateListProductUpdate);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var respone = await _categoryService.GetById<APIRespone>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (CategoryDTO category)
        {
            var respon = await _categoryService.DeleteById<APIRespone>(category.Id, HttpContext.Session.GetString(SD.SessionToken));
            if(respon  != null && respon.IsSuccess)
            {
                TempData["success"] = "Created successfully";
                return RedirectToAction("Index", "Category", new { Id = category.ProductID });
            }
            return View(category);
        }
    }

}

