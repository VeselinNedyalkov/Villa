using TrainingAPI.Data;
using TrainingAPI.Models;
using TrainingAPI.Repository.Contracts;

namespace TrainingAPI.Repository
{
    public class NumberRepository : GenericRepository<VillaNumber>, INumberRepository
    {
        private readonly AplicationDbContext db;

        public NumberRepository(AplicationDbContext _db) : base(_db)
        {
            db = _db;
        }


        public async Task<VillaNumber> UpdateNumberAsync(VillaNumber entity)
        {
            entity.UpdateDate = DateTime.Now;
            db.VillaNumbers.Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
 