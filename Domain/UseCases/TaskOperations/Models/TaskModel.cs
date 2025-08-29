using Domain.UseCases.AccountOperations.Models;
using System.Threading.Tasks;

namespace Domain.UseCases.TaskOperations.Models;

public class TaskModel
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    //public TasksStatus Status { get; set; }

    //public TasksPriority Priority { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public Guid UserId { get; set; }

    public UserModel? User { get; set; }
}
