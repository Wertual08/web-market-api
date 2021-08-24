using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models {
    public class RefreshToken {
        public long UserId { get; set; }
        [Key, Required, MinLength(1024), MaxLength(1024)]
        public string Name { get; init; }
    }
}