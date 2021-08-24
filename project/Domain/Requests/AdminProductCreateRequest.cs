using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class AdminProductCreateRequest {
        public decimal Price { get; init; }

        [Required]
        [MaxLength(256)]
        public string Name { get; init; }

        [Required]
        [MaxLength(4096)]
        public string Description { get; init; }

        [Required]
        public List<long> Records { get; init; }

        [Required]
        public List<long> Categories { get; init; }
        
        [Required]
        public List<long> Sections { get; init; }
    }
}