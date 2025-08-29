using Domain.UseCases.TaskOperations.Models;

namespace Domain.UseCases.AccountOperations.Models;

public class UserModel
{
    public required string UserName { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<TaskModel>? Tasks { get; set; }
}
