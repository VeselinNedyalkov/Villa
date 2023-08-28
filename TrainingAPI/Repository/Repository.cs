using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainingAPI.Data;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Repository
{
    public class Repository : RepositoryGeneric<Villa> , IRepository
    {
        private readonly AplicationDbContext db;

        public Repository(AplicationDbContext _db) : base(_db)
        {
            db = _db;
        }


        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedTime = DateTime.Now;
            db.Villas.Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
