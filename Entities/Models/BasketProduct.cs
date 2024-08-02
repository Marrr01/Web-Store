using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("baskets_products")]
    public class BasketProduct : ModelBase
    {
        [Column("product_amount")]
        public double ProductAmount { get; set; }



        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
