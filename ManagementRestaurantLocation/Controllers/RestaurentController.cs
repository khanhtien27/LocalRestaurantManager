using AutoMapper;
using Azure;
using ManagementRestaurantLocation.Global;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurantLocation.Models.RestaurantDTO;
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

        [HttpGet("Id", Name = "GetRestaurent")]
        
        public async Task<ActionResult<APIRespone>> GetRestaurent (int Id)
        {
            try
            {
                if (Id == 0) return BadRequest();
                var model = await _restaurentRepository.GetAsycn(res => res.Id == Id);
                if (model == null) return NotFound();
                _APIRespone.Result = _mapper.Map<RetaurantDTO>(model);
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
                model.Creat_At = DateTime.Now;
                model.Update_At = DateTime.Now;
                model.Slug = Slug.convertToUnSign2(model.Name);
                await _restaurentRepository.CreateAsycn(model);
                _APIRespone.StatusCode = HttpStatusCode.OK;
                _APIRespone.Result = _mapper.Map<Restaurents>(retaurantCreateDTO);

                return Ok(_APIRespone);
            }
            catch (Exception ex)
            {
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge = new List<string> { ex.ToString() };
            }
            return _APIRespone;
        }

        [HttpPut]
        public async Task<ActionResult<APIRespone>> UpdateRestaurent([FromBody] RetaurantUpdateDTO retaurantUpdateDTO, int Id)
        {
            try
            {
                if (retaurantUpdateDTO == null || Id != retaurantUpdateDTO.Id) return BadRequest();
                var model = _mapper.Map<Restaurents>(retaurantUpdateDTO);
                model.Slug = Slug.convertToUnSign2(model.Name);
                model.Update_At = DateTime.Now;

                await _restaurentRepository.UpdateAsycn(model);
                _APIRespone.Result = model;
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

        [HttpDelete("Id", Name = "DeleteRestaurant")]
        public async Task<ActionResult<APIRespone>> DeleteRestaurant(int Id)
        {
            if(Id == 0) return BadRequest();
            var model = await _restaurentRepository.GetAsycn(res => res.Id == Id);
            if (model == null) return NotFound();

            await _restaurentRepository.DeleteAsycn(model);
            _APIRespone.StatusCode = HttpStatusCode.NoContent;
            _APIRespone.IsSuccess = true;
            _APIRespone.ErrorsMessge = new List<string>
            {
                "Delete Succesful"
            };
            return Ok(_APIRespone);
        }

    }
}
