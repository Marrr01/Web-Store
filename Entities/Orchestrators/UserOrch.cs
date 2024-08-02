using Entities.Models;

namespace Entities.Orchestrators
{
    public class UserOrch : BaseOrchestrator<User>
    {
        public UserOrch(ApplicationContext db) : base(db) => table = db.Users;

        public override void Update(string id, User entity)
        {
            var oldEntity = table.First(e => e.Id == id);
            oldEntity.Age = entity.Age;
            oldEntity.Name = entity.Name;
            db.SaveChanges();
        }

        public override Task UpdateAsync(string id, User entity) => Task.Run(() => Update(id, entity));
    }
}
