using Api.Authorization;
using Api.Database.Models;

namespace Api.Managers {
    public class AccessToken {
        public long UserId { get; set; }
        public string Email { get; set; }
        public UserRoleId UserRole { get; set; }
    }
}