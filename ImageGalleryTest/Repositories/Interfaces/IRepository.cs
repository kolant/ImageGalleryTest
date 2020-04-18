using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingTest.Domain.Models;

namespace AccountingTest.Infrastructure.Abstractions
{
    public interface IRepository<T> where T : IBaseEntity
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Get(string id);
        Task<T> GetAsync(string id);
        T Create(T entity);
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateBatchAsync(IEnumerable<T> entitiesToCreate);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync();
    }
}