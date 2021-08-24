using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models {
    public class Section {
        [Key]
        public long Id { get; init; }

        public long? SectionId { get; set; } = null;

        public long? RecordId { get; set; } = null;

        [Required, MaxLength(255)]
        public string Name { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Record Record { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}