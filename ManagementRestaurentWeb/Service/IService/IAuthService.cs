using ManagementRestaurentWeb.Models.ModelDTO;

namespace ManagementRestaurentWeb.Service.IService
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO);
    }
}
