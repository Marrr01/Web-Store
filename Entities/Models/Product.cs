namespace Entities.Models
{
    public class Product : ModelBase
    {
        public Basket? Basket { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Byte[]? Image { get; set; }
    }
}
