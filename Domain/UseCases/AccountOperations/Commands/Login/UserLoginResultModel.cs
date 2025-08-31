namespace Domain.UseCases.AccountOperations.Commands.Login;

public class UserLoginResultModel
{
    public required string Token { get; set; }

    public required string Email { get; set; }
}
