using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("baskets")]
    public class Basket : ModelBase
    {
        [Column("is_purchased")]
        public bool IsPurchased { get; set; }

        [Column("purchase_date")]
        public DateTime? PurchaseDate { get; set; }

        [Column("is_delivered")]
        public bool IsDelivered { get; set; }

        [Column("delivery_date")]
        public DateTime? DeliveryDate { get; set; }



        public virtual User User { get; set; }
        public virtual List<BasketProduct>? BasketProducts { get; set; }
    }
}
