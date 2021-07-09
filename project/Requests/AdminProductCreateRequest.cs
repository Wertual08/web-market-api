using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class AdminProductCreateRequest {
        [Required]
        [MaxLength(256)]
        public string Name { get; init; }
        [Required]
        [MaxLength(4096)]
        public string Description { get; init; }
    }
}