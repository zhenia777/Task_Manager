namespace Domain.UseCases.AccountOperations.Commands.Registration;

public interface IRegistrationStorage
{
    Task CreateUser(string email, string userName, string password, CancellationToken cancellationToken);
}
