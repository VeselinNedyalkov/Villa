using TrainingAPI.Data;
using TrainingAPI.Models.DTO;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Repository
{
    public class VillaRepository : Repository<Villa> , IVillaRepository
    {
        private readonly AplicationDbContext db;

        public VillaRepository(AplicationDbContext _db) : base(_db)
        {
            db = _db;
        }


        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            db.Villas.Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
