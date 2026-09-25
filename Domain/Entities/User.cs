using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; init; }
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public long Version { get; set; } = 0;
    }
}
