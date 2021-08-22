using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class Product {
        [Key]
        public long Id { get; init; }

        public decimal Price { get; set; }

        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(4096)]
        public string Description { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public IEnumerable<Record> Records { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Section> Sections { get; set; }
    }
}