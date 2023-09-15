using TrainingAPI.Models;

namespace TrainingAPI.Repository.Contracts
{
    public interface INumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateNumberAsync(VillaNumber entity);
        
    }
}
