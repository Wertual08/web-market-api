using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class Product {
        [Key]
        public long Id { get; init; }

        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(4096)]
        public string Description { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}