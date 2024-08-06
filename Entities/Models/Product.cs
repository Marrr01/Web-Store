using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("products")]
    public class Product : ModelBase
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("image")]
        public byte[]? Image { get; set; }



        public virtual List<BasketProduct>? BasketsProduct { get; set; }
    }
}
