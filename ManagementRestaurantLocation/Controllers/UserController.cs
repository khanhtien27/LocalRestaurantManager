using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO;
using ManagementRestaurantLocation.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ManagementRestaurantLocation.Controllers
{
    [Route("API/UserAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected APIRespone _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new APIRespone();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepository.Login(loginRequestDTO);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessge.Add("User or password is incorrect");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO registerationRequestDTO)
        {
            bool isUserNameUnique = _userRepository.IsUniqueUser(registerationRequestDTO.UserName);
            if(!isUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessge.Add("User already exits");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(registerationRequestDTO);
            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorsMessge.Add("Have some errors in while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
