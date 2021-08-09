using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class Category {
        [Key]
        public long Id { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        [Required, MaxLength(255)]
        public string Name { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}