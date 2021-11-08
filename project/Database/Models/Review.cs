using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Authorization;

namespace Api.Database.Models {
    public class Review {
        [Key]
        public long Id { get; init; }

        public long? UserId { get; init; }

        public int? Grade { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; 

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;    

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }
    }
}