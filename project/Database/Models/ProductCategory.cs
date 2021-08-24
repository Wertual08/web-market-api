using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Models {
    public class ProductCategory {
        public long ProductId { get; init; }
        public long CategoryId { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;


        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}