using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class RegisterRequest {
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