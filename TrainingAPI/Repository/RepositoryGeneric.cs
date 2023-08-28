using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainingAPI.Data;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Repository
{
    public class RepositoryGeneric<T> : IRepopositoryGeneric<T> where T : class
    {
        private readonly AplicationDbContext db;
        internal DbSet<T> dbSet;

        public RepositoryGeneric(AplicationDbContext _db)
        {
            db = _db;
            this.dbSet = db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await Save();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetVillaAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
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

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
