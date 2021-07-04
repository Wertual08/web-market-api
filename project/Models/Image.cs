

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class Image {
        public long Id { get; init; }

        [Required]
        [MaxLength(22)]
        [Column(TypeName = "char(22)")]
        public string Name { get; set; }
    }
}