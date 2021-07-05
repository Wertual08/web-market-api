using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Admin.Products {
    public class ProductCreateRequest {
        [Required]
        [MaxLength(256)]
        public string Name { get; init; }
        [Required]
        [MaxLength(4096)]
        public string Description { get; init; }
    }
}