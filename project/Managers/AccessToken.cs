using Api.Authorization;
using Api.Models;

namespace Api.Managers {
    public class AccessToken {
        public long UserId { get; set; }
        public string Login { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}