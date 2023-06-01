using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO;
using ManagementRestaurentWeb.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Unitity;

namespace ManagementRestaurentWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            return View(loginRequestDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginRequestDTO loginRequestDTO)
        {
            var response = await _authService.LoginAsync<APIRespone>(loginRequestDTO);
            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Login successfully";
                var model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, model.User.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, model.User.Role));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                HttpContext.Session.SetString(SD.SessionToken, model.Token);
                if (model.User.Role == "admin")
                    return RedirectToAction("IndexRestaurent", "Restaurent");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomErrors", response.ErrorsMessge.FirstOrDefault());
                return View(loginRequestDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            var response = await _authService.RegisterAsync<APIRespone>(registerationRequestDTO);
            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Register successfully";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            TempData["success"] = "Logout successfully";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
