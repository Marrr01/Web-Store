using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq.Expressions;

namespace Controllers
{
    public class APIController : Controller
    {
        private ApplicationContext db;
        public APIController(ApplicationContext db) => this.db = db;

        [HttpGet, Route("api/users/logins"), Authorize]
        public IActionResult GetAllLogins()
        {
            // Операторы запросов, var - IQueryable<string>
            var logins1 = from u in db.Users
                          select u.Login;
            // SQL запрос:
            // SELECT "u"."login"
            // FROM "users" AS "u"

            // Методы расширения, var - IQueryable<string>
            var logins2 = db.Users.Select(u => u.Login);
            // SQL запрос:
            // SELECT "u"."login"
            // FROM "users" AS "u"

            var selector = new Func<User, string>(u => u.Login);
            // Методы расширения, var - IEnumerable<string>
            var logins3 = db.Users.Select(selector);
            // SQL запрос:
            // SELECT "u"."login", "u"."age", "u"."email", "u"."is_admin", "u"."name", "u"."password", "u"."surname"
            // FROM "users" AS "u"
            return Json(logins1);
        }

        [HttpGet, Route("api/users/all"), Authorize]
        public IActionResult GetAllUsers() => Json(db.Users);

        [HttpGet, Route("api/users/user"), Authorize]
        public IActionResult GetUser(string login)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == login);
            if (user == null) return NotFound();
            return Json(user);
        }

        [HttpDelete, Route("api/users/delete"), Authorize]
        public IActionResult DeleteUser(string login, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == login &&
                                                    u.Password == password);
            if (user == null) return NotFound();
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut, Route("api/users/changepassword"), Authorize]
        public IActionResult ChangePassword(string login, string oldPass, string newPass)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == login &&
                                                    u.Password == oldPass);
            if (user == null) return NotFound();
            user.Password = newPass;
            db.SaveChanges();
            return Ok();
        }

        [HttpPost, Route("api/users/newuser"), Authorize]
        public IActionResult Users(User userData)
        {
            db.Users.Add(userData);
            db.SaveChanges();
            return Ok();
        }
    }
}
