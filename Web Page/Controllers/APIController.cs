using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    public class APIController : Controller
    {
        private ApplicationContext db;
        public APIController(ApplicationContext db) => this.db = db;

        [HttpGet, HttpDelete, Route("api/users/{login?}"), Authorize]
        public IActionResult Users(string? login)
        {
            if (Request.Method == "GET")
            {
                if (login == null) return Json(db.Users);
                var user = db.Users.FirstOrDefault(u => u.Login == login);
                if (user == null) return NotFound();
                return Json(user);
            }
            if (Request.Method == "DELETE")
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login);
                if (user == null) return NotFound();
                db.Users.Remove(user);
                db.SaveChanges();
                return Ok();
            }
            else return BadRequest();
        }

        [HttpPost, HttpPut, Route("api/users"), Authorize]
        public IActionResult Users(User userData)
        {
            if (Request.Method == "POST")
            {
                db.Users.Add(userData);
                db.SaveChanges();
                return Ok();
            }
            if (Request.Method == "PUT")
            {
                var user = db.Users.FirstOrDefault(u => u.Login == userData.Login);
                if (user == null) return NotFound();
                user.Password = userData.Password;
                return Ok();
            }
            else return BadRequest();
        }
    }
}
