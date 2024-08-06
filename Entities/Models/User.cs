using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table ("users"), PrimaryKey(nameof(Login))]
    public class User
    {
        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("surname")]
        public string? Surname { get; set; }

        [Column("age")]
        public int? Age { get; set; }



        public virtual List<Basket>? Baskets { get; set; }
    }
}