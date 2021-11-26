using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class ReviewCreateRequest {
        [Range(1, 5)]
        public int? Grade { get; set; }
        [Required, MaxLength(25)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256)]
        public string Description { get; set; }
    }
}