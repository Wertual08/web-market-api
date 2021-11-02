using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Authorization;

namespace Api.Database.Models {
    public class MainSlide {
        [Key]
        public long Id { get; init; }

        public long RecordId { get; set; }

        public int Position { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; 

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;    
        
        [Required]
        public string Name { get; set; }


        public Record Record { get; set; }
    }
}