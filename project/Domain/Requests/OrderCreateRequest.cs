using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public record OrderCreateRequest {
        [Required, EmailAddress]
        public string Email { get; init; }
        [Phone]
        public string Phone { get; init; }
        [Required, MaxLength(127)]
        public string Name { get; init; }
        [Required, MaxLength(127)]
        public string Surname { get; init; }
        [MaxLength(1023)]
        public string Address { get; init; }
        [MaxLength(63)]
        public string PromoCode { get; init; }
        [MaxLength(1023)]
        public string Description { get; init; }
        [Required]
        public Dictionary<long, int> Products { get; init; }
    }
}