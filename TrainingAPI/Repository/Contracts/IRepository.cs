using System.Linq.Expressions;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface IRepository
    {
        Task CreateAsync(Villa entity);
        Task UpdateAsync(Villa entity);
        Task RemoveAsync(Villa entity);
        Task<List<Villa>> GetAllAsync(Expression <Func<Villa, bool>> filter = null);
        Task<Villa> GetVillaAsync(Expression<Func<Villa, bool>> filter = null,  bool tracked = true);

        Task Save();
    }
}
