using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Models {
    public class ProductSection {
        public long ProductId { get; init; }
        public long SectionId { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;


        public Product Product { get; set; }
        public Section Section { get; set; }
    }
}