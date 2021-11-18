using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models {
    public class Article {
        [Key]
        public long Id { get; init; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }
        
        public string Annotation { get; set; }

        public string Description { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public bool Visible { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}