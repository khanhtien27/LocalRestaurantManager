

using ManagementRestaurentWeb.Models.ModelDTO.RestaurantDTO;

namespace ManagementRestaurentWeb.Service.IService
{
    public interface IRestaurentService 
    {
        Task<T> GetAll<T>(string token);
        Task<T> GetById<T>(int Id, string token);
        Task<T> Create<T>(RetaurantCreateDTO retaurantCreateDTO, string token);
        Task<T> Update<T>(RetaurantUpdateDTO retaurantUpdateDTO, string token);
        Task<T> DeleteById<T>(int Id, string token);
    }
}
