using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table ("users")]
    public class User : ModelBase
    {
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