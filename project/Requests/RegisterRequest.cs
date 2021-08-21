using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class RegisterRequest {
        [Required, MinLength(5), MaxLength(16), RegularExpression("^[a-z\\d_]+$")]
        public string Login { get; set; }
        [Required, MinLength(5)]
        public string Password { get; set; }
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