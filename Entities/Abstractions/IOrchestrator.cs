using Entities.Models;
using System.Linq.Expressions;

namespace Entities.Abstractions
{
    /// <summary>
    /// CRUD - Create, Read, Update, Delete
    /// </summary>
    interface IOrchestrator<T> where T : ModelBase
    {
        public void Create(T entity);
        public void Create(IEnumerable<T> entities);
        public T Read(string id);
        public IEnumerable<T> Read(IEnumerable<string> ids);
        public IEnumerable<T> Read(Expression<Func<T,bool>> predicate);
        public IEnumerable<T> ReadAll();
        public void Update(string id, T entity);
        public void Delete(string id);
        public void Delete(IEnumerable<string> ids);
        public void Delete(Expression<Func<T, bool>> predicate);
        public void DeleteAll();
    }
}
