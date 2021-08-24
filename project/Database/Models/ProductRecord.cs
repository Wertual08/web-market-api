using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Models {
    public class ProductRecord {
        public long ProductId { get; init; }
        public long RecordId { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;


        public Product Product { get; set; }
        public Record Record { get; set; }
    }
}