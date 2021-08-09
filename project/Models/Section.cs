using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class Section {
        [Key]
        public long Id { get; init; }

        public long? SectionId { get; set; } = null;

        [Required, MaxLength(255)]
        public string Name { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public ICollection<Section> Sections { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}