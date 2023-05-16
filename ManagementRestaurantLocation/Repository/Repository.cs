using ManagementRestaurantLocation.Data;
using ManagementRestaurantLocation.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ManagementRestaurantLocation.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RestaurentDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(RestaurentDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
        }

        public async Task CreateAsycn(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsycn();
        }

        public async Task DeleteAsycn(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsycn();
        }

        public async Task<List<T>> GetAllAsycn(Expression<Func<T, bool>>? fillter = null, string? includeProperties = null)
        {
            IQueryable<T> querry = _dbSet;
            if(fillter != null) querry = querry.Where(fillter);
            if(includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    querry = querry.Include(item);
                }
            }
            return await querry.ToListAsync();
        }

        public async Task<T> GetAsycn(Expression<Func<T, bool>>? fillter = null, bool track = true, string? includeProperties = null)
        {
            IQueryable<T> querry = _dbSet;
            if (!track) querry = querry.AsNoTracking();
            if (fillter != null) querry = querry.Where(fillter);
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    querry = querry.Include(item);
                }
            }
            return await querry.FirstOrDefaultAsync();
        }

        public async Task SaveAsycn()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsycn(T entity)
        {
            _context.Update(entity);
            await SaveAsycn();
        }
    }
}
