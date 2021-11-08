using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class ReviewCreateRequest {
        public int? Grade { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
    }
}