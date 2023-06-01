using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO;

namespace ManagementRestaurentWeb.Service.IService
{
    public interface IProductService
    {
        Task<T> GetAll<T>( string token);
        Task<T> GetById<T>(int Id, string token);
        Task<T> Create<T>(ProductCreatDTO productCreatDTO, string token);
        Task<T> Update<T>(ProductUpdateDTO productUpdateDTO, string token);
        Task<T> DeleteById<T>(int Id, string token);
    }
}
