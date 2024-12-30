using System.ComponentModel.DataAnnotations;

namespace TrackingOrderSystem.Data.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //ForeignKey 1-n
        public ICollection<Order> Orders { get; set; }
    }
}
