﻿using System.ComponentModel.DataAnnotations;

namespace TrackingOrderSystem.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        public decimal UnitPrice { get; set; }
        [Required]

        public decimal TotalPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
