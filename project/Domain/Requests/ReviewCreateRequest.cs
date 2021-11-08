using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class ReviewCreateRequest {
        [Range(1, 5)]
        public int? Grade { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
    }
}