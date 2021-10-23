using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Models {
    public class CartProduct {
        public long UserId { get; init; }
        public long ProductId { get; init; }
        public int Amount { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;


        public User User { get; set; }
        public Product Product { get; set; }
    }
}