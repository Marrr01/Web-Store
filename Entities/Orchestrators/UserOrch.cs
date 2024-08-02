using Entities.Models;

namespace Entities.Orchestrators
{
    public class UserOrch : BaseOrchestrator<User>
    {
        public UserOrch(ApplicationContext db) : base(db) => table = db.Users;
    }
}
