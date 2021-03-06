using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models {
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


        public IEnumerable<Product> Products { get; set; }
    }
}