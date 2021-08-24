using Api.Authorization;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record ProfileResponse {
        public long Id { get; init; }
        public UserRoleId Role { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ProfileResponse(User model) {
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