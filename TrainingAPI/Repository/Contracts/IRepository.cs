using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface IRepository : IRepopositoryGeneric<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
        
    }
}
