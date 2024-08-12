using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Controllers.Controllers
{
    public class AuthenticationController : Controller
    {
        private ApplicationContext db;
        public AuthenticationController(ApplicationContext db) => this.db = db;

        [HttpPost, Route("login")]
        public IActionResult Login(User user)
        {
            var person = db.Users.FirstOrDefault(u => u.Login == user.Login &&
                                                      u.Password == user.Password);
            if (person is null) return NotFound();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Login) };

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                username = person.Login
            };

            return Json(response);
        }
    }
}
