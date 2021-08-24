using System;
using System.ComponentModel.DataAnnotations;
using Api.Authorization;

namespace Api.Database.Models {
    public class User {
        [Key]
        public long Id { get; init; }
        public UserRoleId Role { get; set; }
        [Required, MinLength(5), MaxLength(16), RegularExpression("^[a-z\\d_]+$")]
        public string Login { get; set; }
        [Required, MaxLength(128)]
        public string Password { get; set; }
        [Required, MinLength(5), MaxLength(320), RegularExpression("^..*@..*\\...*$")]
        public string Email { get; set; }
        [MinLength(4), MaxLength(16), RegularExpression("^\\+\\d*$")]
        public string Phone { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Surname { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;    
        public DateTime VerifiedAt { get; set; }    

        public RefreshToken RefreshToken { get; set; }
    }
}