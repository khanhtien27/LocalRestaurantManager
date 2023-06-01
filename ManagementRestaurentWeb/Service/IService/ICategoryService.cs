using ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO;

namespace ManagementRestaurentWeb.Service.IService
{
    public interface ICategoryService
    {
        Task<T> GetAll<T>(string token);
        Task<T> GetById<T>(int Id, string token);
        Task<T> Create<T>(CategoryCreateDTO categoryCreateDTO, string token);
        Task<T> Update<T>(CategoryUpdateDTO categoryUpdateDTO, string token);
        Task<T> DeleteById<T>(int Id, string token);
    }
}
