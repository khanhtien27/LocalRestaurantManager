using AutoMapper;
using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurentWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Unitity;

namespace ManagementRestaurentWeb.Controllers
{
    public class RestaurentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRestaurentService _restaurentService;
        public RestaurentController(IMapper mapper, IRestaurentService restaurentService)
        {
            _mapper = mapper;
            _restaurentService = restaurentService;
        }

        [Authorize]
        public async Task<IActionResult> IndexRestaurent()
        {
            List<RetaurantDTO> list = new List<RetaurantDTO>();
            var respone = await _restaurentService.GetAll<APIRespone>(HttpContext.Session.GetString(SD.SessionToken));

            if(respone != null && respone.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<RetaurantDTO>>(Convert.ToString(respone.Result));
            }
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create ()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (RetaurantCreateDTO retaurantCreateDTO)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Create successfully";
                var respone = await _restaurentService.Create<APIRespone>(retaurantCreateDTO, HttpContext.Session.GetString(SD.SessionToken));
                if (respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexRestaurent));
                }
            }
            return View(retaurantCreateDTO);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update (int Id)
        {
            var respone = await _restaurentService.GetById<APIRespone>(Id, HttpContext.Session.GetString(SD.SessionToken));
            if (respone != null && respone.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<RetaurantUpdateDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update (RetaurantUpdateDTO retaurantUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Update successfully";
                var respone = await _restaurentService.Update<APIRespone>(retaurantUpdateDTO, HttpContext.Session.GetString(SD.SessionToken));
                if(respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexRestaurent));
                }
            }
            return View(retaurantUpdateDTO);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete (int Id)
        {
            var respone = await _restaurentService.GetById<APIRespone>(Id, HttpContext.Session.GetString(SD.SessionToken));
            if(respone != null && respone.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<RetaurantDTO> (Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(RetaurantDTO retaurantDTO)
        {
            var respone = await _restaurentService.DeleteById<APIRespone>(retaurantDTO.Id, HttpContext.Session.GetString(SD.SessionToken));
            if(respone !=null && respone.IsSuccess)
            {
                TempData["success"] = "Delete successfully";
                return RedirectToAction(nameof(IndexRestaurent));
            }
            return View(retaurantDTO);
        }
    }
}
