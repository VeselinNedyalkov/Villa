using TrainingAPI.Models;

namespace TrainingAPI.Repository.Contracts
{
    public interface INumberRepository : IRepopositoryGeneric<VillaNumber>
    {
        Task<VillaNumber> UpdateNumberAsync(VillaNumber entity);
        
    }
}
