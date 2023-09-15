using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
        
    }
}
