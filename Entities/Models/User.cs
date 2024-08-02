namespace Entities.Models
{
    public class User : ModelBase
    {
        public string Password { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public IEnumerable<Basket>? Baskets { get; set; }
    }
}