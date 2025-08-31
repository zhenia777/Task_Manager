using Domain.Exceptions;
using Domain.UseCases.AccountOperations.Commands.Login;
using Microsoft.EntityFrameworkCore;

namespace Storage.Storages.AccountStorage;

public class LoginStorage(TaskManagerDbContext dbContext) : ILoginStorage
{
    private readonly TaskManagerDbContext _dbContext = dbContext;

    public async Task<bool> CheckPassword(string email, string password, 
                                          CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(u => u.Email == email, 
                                                       cancellationToken);
        if (user == null)
        {
            throw new DomainException(ErrorDomainStatus.NotFound, $"User with: {email} not found!");
        }
        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }

    public Task<Guid> GetUserId(string email, CancellationToken cancellationToken)
    {
        return dbContext.Users
                        .AsNoTracking()
                        .Where(u => u.Email == email)
                        .Select(u => u.Id)
                        .FirstAsync(cancellationToken);
    }


    public Task<bool> IsUserExist(string email, CancellationToken cancellationToken)
    {
        return dbContext.Users
                       .AsNoTracking()
                       .AnyAsync(u => u.Email == email, cancellationToken);
    }
}
