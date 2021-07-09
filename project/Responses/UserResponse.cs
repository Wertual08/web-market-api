using Api.Authorization;
using Api.Models;

namespace Api.Responses {
    public record UserResponse {
        public long Id { get; init; }
        public AccessLevel Role { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserResponse(User model) {
            Id = model.Id;
            Role = model.Role;
            Login = model.Login;
            Email = model.Email;
            Phone = model.Phone;
            Name = model.Name;
            Surname = model.Surname;
        }
    }
}