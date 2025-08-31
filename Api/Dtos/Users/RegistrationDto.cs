namespace API.Dtos.Users
{
    public class RegistrationDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
