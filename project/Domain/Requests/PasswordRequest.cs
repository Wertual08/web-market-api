using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class PasswordRequest {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}