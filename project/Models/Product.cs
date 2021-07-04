using System;
using System.ComponentModel.DataAnnotations;

namespace app.Models {
    public class Product {
        public long Id { get; init; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}