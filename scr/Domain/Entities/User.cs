using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PathIcon { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRoles? Role { get; set; }
    }
}
