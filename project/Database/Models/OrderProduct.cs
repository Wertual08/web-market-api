using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Models {
    public class OrderProduct {
        public long OrderId { get; init; }
        public long ProductId { get; init; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;


        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}