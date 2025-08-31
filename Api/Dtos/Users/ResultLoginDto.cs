namespace API.Dtos.Users;

public class ResultLoginDto
{
    public required string Email { get; set; }

    public required string Token { get; set; }
}
