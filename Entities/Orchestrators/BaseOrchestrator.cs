using Entities.Abstractions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Entities.Orchestrators
{
    public class BaseOrchestrator<T> : IOrchestrator<T>, IAsyncOrchestrator<T> where T : ModelBase
    {
        protected ApplicationContext db;
        protected DbSet<T> table;
        
        public BaseOrchestrator(ApplicationContext db) => this.db = db;

        private void Check()
        {
            if (table == null) throw new Exception("В конструкторе класса необходимо присвоить значение полю table");
        }

        #region Create
        public void Create(T entity)
        {
            Check();
            table.Add(entity);
            db.SaveChanges();
        }
        public Task CreateAsync(T entity) => Task.Run(() => Create(entity));

        public void Create(IEnumerable<T> entities)
        {
            Check();
            table.AddRange(entities);
            db.SaveChanges();
        }
        public Task CreateAsync(IEnumerable<T> entities) => Task.Run(() => Create(entities));
        #endregion

        #region Read
        public T Read(string id)
        {
            Check();
            return table.First(e => e.Id == id);
        }
        public Task<T> ReadAsync(string id) => Task.Run(() => Read(id));

        public IEnumerable<T> Read(IEnumerable<string> ids)
        {
            Check();
            return table.Where(e => ids.Contains(e.Id));
        }
        public Task<IEnumerable<T>> ReadAsync(IEnumerable<string> ids) => Task.Run(() => Read(ids));

        public IEnumerable<T> Read(Expression<Func<T, bool>> predicate)
        {
            Check();
            return table.Where(predicate);
        }
        public Task<IEnumerable<T>> ReadAsync(Expression<Func<T, bool>> predicate) => Task.Run(() => Read(predicate));

        public IEnumerable<T> ReadAll()
        {
            Check();
            return table;
        }
        public Task<IEnumerable<T>> ReadAllAsync() => Task.Run(() => ReadAll());
        #endregion

        #region Update
        public virtual void Update(string id, T entity)
        {
            Check();
            var oldEntity = table.First(e => e.Id == id);
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                if (property.Name != "Id")
                {
                    property.SetValue(oldEntity, type.GetProperty(property.Name).GetValue(entity));
                }
            }
            db.SaveChanges();
        }
        public virtual Task UpdateAsync(string id, T entity) => Task.Run(() => Update(id, entity));
        #endregion

        #region Delete
        public void Delete(string id)
        {
            Check();
            var entity = table.First(e => e.Id == id);
            table.Remove(entity);
            db.SaveChanges();
        }
        public Task DeleteAsync(string id) => Task.Run(() => Delete(id));

        public void Delete(IEnumerable<string> ids)
        {
            Check();
            var entities = table.Where(e => ids.Contains(e.Id));
            table.RemoveRange(entities);
            db.SaveChanges();
        }
        public Task DeleteAsync(IEnumerable<string> ids) => Task.Run(() => Delete(ids));

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            Check();
            var entities = table.Where(predicate);
            table.RemoveRange(entities);
            db.SaveChanges();
        }
        public Task DeleteAsync(Expression<Func<T, bool>> predicate) => Task.Run(() => Delete(predicate));

        public void DeleteAll()
        {
            Check();
            table.RemoveRange(ReadAll());
            db.SaveChanges();
        }
        public Task DeleteAllAsync() => Task.Run(() => DeleteAll());
        #endregion
    }
}
