using Domain.Helpers;
using Domain.UseCases.AccountOperations.Commands.Registration;
using Storage.Entities;

namespace Storage.Storages.AccountStorage;

public class RegistrationStorage(TaskManagerDbContext dbContext) : IRegistrationStorage
{
    private readonly TaskManagerDbContext _dbContext = dbContext;

    public async Task CreateUser(string email, string userName, string password, CancellationToken cancellationToken)
    {
        UserEntity user = new UserEntity
        {
            Email = email,
            UserName = userName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        await dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

