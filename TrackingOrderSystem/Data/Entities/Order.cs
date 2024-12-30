using System.ComponentModel.DataAnnotations;

namespace TrackingOrderSystem.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]

        public DateTime OrderDate { get; set; }
        [Required]

        public decimal TotalAmount { get; set; }
        [Required]

        public string ShippingAddress { get; set; }
        [Required]

        public string Status { get; set; }
        [Required]
        public string PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // n-1
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
