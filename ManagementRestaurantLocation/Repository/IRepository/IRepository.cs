using System.Collections.Generic;
using System.Linq.Expressions;

namespace ManagementRestaurantLocation.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsycn (Expression<Func<T, bool>>? fillter = null, string? includeProperties = null);
        Task<T> GetAsycn(Expression<Func<T, bool>>? fillter = null, bool track = true, string? includeProperties = null);
        Task DeleteAsycn(T entity);
        Task CreateAsycn(T entity);
        Task UpdateAsycn(T entity);
        Task SaveAsycn();
    }
}
