using Entities.Models;
using System.Linq.Expressions;

namespace Entities.Abstractions
{
    /// <summary>
    /// Async CRUD - Create, Read, Update, Delete
    /// </summary>
    public interface IAsyncOrchestrator<T> where T : ModelBase
    {
        public Task CreateAsync(T entity);
        public Task CreateAsync(IEnumerable<T> entities);
        public Task <T> ReadAsync(string id);
        public Task <IEnumerable<T>> ReadAsync(IEnumerable<string> ids);
        public Task <IEnumerable<T>> ReadAsync(Expression<Func<T, bool>> predicate);
        public Task <IEnumerable<T>> ReadAllAsync();
        public Task UpdateAsync(string id, T entity);
        public Task DeleteAsync(string id);
        public Task DeleteAsync(IEnumerable<string> ids);
        public Task DeleteAsync(Expression<Func<T, bool>> predicate);
        public Task DeleteAllAsync();
    }
}

