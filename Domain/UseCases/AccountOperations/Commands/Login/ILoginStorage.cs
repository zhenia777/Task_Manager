namespace Domain.UseCases.AccountOperations.Commands.Login;

public interface ILoginStorage
{
    Task<bool> CheckPassword(string email, string password, CancellationToken cancellationToken);

    Task<bool> IsUserExist(string email, CancellationToken cancellationToken);

    Task<Guid> GetUserId(string email, CancellationToken cancellationToken);
}
