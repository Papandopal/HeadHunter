using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; init; }
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public long Version { get; set; } = 0;
        public void Block()
        {
            Status = UserStatus.Blocked;
        }
        public void Unblock()
        {
            Status = UserStatus.Active;
        }
    }
}
