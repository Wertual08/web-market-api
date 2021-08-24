using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models {
    public class Category {
        [Key]
        public long Id { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        [Required, MaxLength(255)]
        public string Name { get; set; }


        public IEnumerable<Product> Products { get; set; }
    }
}