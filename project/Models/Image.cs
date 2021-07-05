

using System.ComponentModel.DataAnnotations;

namespace api.Models {
    public class Image {
        public long Id { get; init; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}