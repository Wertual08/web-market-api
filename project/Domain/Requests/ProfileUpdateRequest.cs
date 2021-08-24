using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class ProfileUpdateRequest {
        [Required, MinLength(5), MaxLength(16), RegularExpression("^[a-z\\d_]+$")]
        public string Login { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Surname { get; set; }
    }
}