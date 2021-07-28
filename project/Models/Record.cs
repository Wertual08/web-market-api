using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class Record {
        [Key]
        public long Id { get; init; }

        [Required]
        public Guid Identifier { get; init; } = Guid.NewGuid();
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        [Required, MaxLength(255)]
        public string ContentType { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
    }
}