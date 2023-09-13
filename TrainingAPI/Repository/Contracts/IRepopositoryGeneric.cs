using System.Linq.Expressions;

namespace TrainingAPI.Repository.Contracts
{
    public interface IRepopositoryGeneric<T> where T : class
    {
        Task RemoveAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetVillaAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task CreateAsync(T entity);
        Task SaveAsync();
    }
}
