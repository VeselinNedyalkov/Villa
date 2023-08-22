using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainingAPI.Data;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Repository
{
    public class Repository : IRepository
    {
        private readonly AplicationDbContext db;

        public Repository(AplicationDbContext _db)
        {
            db = _db;
        }

        public async Task CreateAsync(Villa entity)
        {
            await db.Villas.AddAsync(entity);
            await Save();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> query = db.Villas;

            if(filter != null)
            {
                 query = query.Where(filter);
            } 

            return await query.ToListAsync();
        }

        public async Task<Villa> GetVillaAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = db.Villas;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            db.Villas.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Villa entity)
        {
            db.Update(entity);
            await Save();
        }
    }
}
