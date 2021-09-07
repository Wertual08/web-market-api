using Api.Authorization;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record ProfileResponse {
        public long Id { get; init; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ProfileResponse(User model) {
            Id = model.Id;
            Role = model.Role.ToString();
            Login = model.Login;
            Email = model.Email;
            Phone = model.Phone;
            Name = model.Name;
            Surname = model.Surname;
        }
    }
}