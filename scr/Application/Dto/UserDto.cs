namespace Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PathIcon { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
    }
}
