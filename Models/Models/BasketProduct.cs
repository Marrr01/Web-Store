using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("baskets_products")]
    public class BasketProduct
    {
        [Column("product_amount"), Required]
        public double ProductAmount { get; set; }



        public Guid BasketId { get; set; }
        public virtual Basket Basket { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
