namespace Entities.Models
{
    public class Basket : ModelBase
    {
        public User User { get; set; }
        public bool IsPurchased { get; set; }
        public DateTime? PurchaseTime { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
