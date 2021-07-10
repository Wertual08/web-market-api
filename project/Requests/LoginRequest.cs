using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class LoginRequest {
        [Required, MinLength(5), MaxLength(320)]
        public string LoginLogin { get; set; }
        [Required]
        public string Password { get; set; }
    }
}