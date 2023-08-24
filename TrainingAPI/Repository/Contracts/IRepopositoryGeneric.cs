﻿using System.Linq.Expressions;
using TrainingAPI.Models.DTO;

namespace TrainingAPI.Repository.Contracts
{
    public interface IRepopositoryGeneric<T> where T : class
    {
        Task RemoveAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetVillaAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task Save();
    }
}