using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainingAPI.Data;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AplicationDbContext db;
        internal DbSet<T> dbSet;

        public Repository(AplicationDbContext _db)
        {
            db = _db;
            //db.VillaNumbers.Include(u => u.Villa).ToList();
            dbSet = db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetVillaAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

           

            if (includeProperties != null)
            {
                
                    query = query.Include(includeProperties);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
            int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (pageSize > 0)
            {
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            if (includeProperties != null)
            {
                foreach (var inlcude in includeProperties.Split(",",
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query =  query.Include(inlcude);
                }
            }

            return await query.ToListAsync();
        }

        

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
