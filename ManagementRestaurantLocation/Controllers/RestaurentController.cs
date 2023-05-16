using AutoMapper;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurantLocation.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ManagementRestaurantLocation.Controllers
{
    [Route("API/Restaurent")]
    [ApiController]
    public class RestaurentController : ControllerBase
    {
        private readonly IRestaurentRepository _restaurentRepository;
        private readonly IMapper _mapper;
        private readonly APIRespone _APIRespone;
        public RestaurentController(IRestaurentRepository restaurentRepository, IMapper mapper)
        {
            _restaurentRepository = restaurentRepository;
            _mapper = mapper;
            this._APIRespone = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIRespone>> GetAllRestaurent()
        {
            try
            {
                IEnumerable<Restaurents> restaurents = await _restaurentRepository.GetAllAsycn();
                _APIRespone.Result = _mapper.Map<List<RetaurantDTO>>(restaurents);
                _APIRespone.StatusCode = HttpStatusCode.OK;
                return Ok(_APIRespone);
            }
            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }

        [HttpPost]
        
        public async Task<ActionResult<APIRespone>> RestaureenCreate ([FromBody] RetaurantCreateDTO retaurantCreateDTO)
        {
            try
            {
                if (await _restaurentRepository.GetAsycn(res => res.Name.ToLower() == retaurantCreateDTO.Name.ToLower()) != null)
                {
                    return BadRequest(ModelState);
                }
                if (retaurantCreateDTO == null)
                {
                    return BadRequest(retaurantCreateDTO);
                }

                var model = _mapper.Map<Restaurents>(retaurantCreateDTO);
                await _restaurentRepository.CreateAsycn(model);
                _APIRespone.StatusCode = HttpStatusCode.OK;
                return Ok(_APIRespone);
            }
            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }
    }
}
