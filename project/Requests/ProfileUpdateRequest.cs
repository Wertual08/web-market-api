using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class ProfileUpdateRequest {
        [Required, MinLength(5), MaxLength(16), RegularExpression("^[a-z\\d_]+$")]
        public string Login { get; set; }
        [Required, MinLength(5), MaxLength(320), RegularExpression("^..*@..*\\...*$")]
        public string Email { get; set; }
        [MinLength(4), MaxLength(16), RegularExpression("^\\+\\d*$")]
        public string Phone { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Surname { get; set; }
    }
}