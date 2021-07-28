using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class ProductRecord {
        public long ProductId { get; init; }
        public long RecordId { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}