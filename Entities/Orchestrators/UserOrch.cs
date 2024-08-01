using Entities.Abstractions;
using Entities.Models;

namespace Entities.Orchestrators
{
    public class UserOrch : IOrchestrator<User>
    {
        private ApplicationContext _db;
        public UserOrch(ApplicationContext db) => _db = db;

        public void Create(User entity)
        {
            _db.Users.Add(entity);
            _db.SaveChanges();
        }

        public void Create(IEnumerable<User> entities)
        {
            _db.Users.AddRange(entities);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _db.Users.First(u => u.Id == id);
            _db.Users.Remove(user);
            _db.SaveChanges();
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            var users = _db.Users.Where(u => ids.Contains(u.Id));
            _db.Users.RemoveRange(users);
            _db.SaveChanges();
        }

        public void Delete()
        {
            _db.Users.RemoveRange(Read());
            _db.SaveChanges();
        }

        public User Read(Guid id)
        {
            return _db.Users.First(u => u.Id == id);
        }

        public IEnumerable<User> Read(IEnumerable<Guid> ids)
        {
            return _db.Users.Where(u => ids.Contains(u.Id));
        }

        public IEnumerable<User> Read()
        {
            return _db.Users;
        }

        public void Update(Guid id, User entity)
        {
            var user = _db.Users.First(u => u.Id == id);
            user.Age = entity.Age;
            user.Name = entity.Name;
            _db.SaveChanges();
        }
    }
}
