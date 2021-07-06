

using System.ComponentModel.DataAnnotations;

namespace Api.Models {
    public class Image {
        public long Id { get; init; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}