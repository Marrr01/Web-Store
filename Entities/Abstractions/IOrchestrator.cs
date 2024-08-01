namespace Entities.Abstractions
{
    /// <summary>
    /// CRUD - Create, Read, Update, Delete
    /// </summary>
    interface IOrchestrator<T>
    {
        public void Create(T entity);
        public void Create(IEnumerable<T> entities);
        public T Read(Guid id);
        public IEnumerable<T> Read(IEnumerable<Guid> ids);
        public IEnumerable<T> Read();
        public void Update(Guid id, T entity);
        public void Delete(Guid id);
        public void Delete(IEnumerable<Guid> ids);
        public void Delete();
    }
}
