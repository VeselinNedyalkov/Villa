using System.Linq.Expressions;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface IRepository
    {
        Task Create(Villa entity);
        Task Update(Villa entity);
        Task Remove(Villa entity);
        Task<List<Villa>> GetAll(Expression <Func<Villa, bool>> filter = null);
        Task<Villa> GetVilla(Expression<Func<Villa, bool>> filter = null,  bool tracked = true);

        Task Save();
    }
}
