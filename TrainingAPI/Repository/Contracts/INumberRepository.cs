using System.Linq.Expressions;
using TrainingAPI.Models;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface INumberRepository : IRepopositoryGeneric<VillaNumber>
    {
        Task<VillaNumber> UpdateNumberAsync(VillaNumber entity);
        
    }
}
