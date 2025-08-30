using Domain.UseCases.TaskOperations.Models;

namespace Domain.UseCases.TaskOperations.Queries.GetTaskById;

public interface IGetTaskByIdStorage
{
    Task<TaskModel> GetTaskById(Guid id, CancellationToken cancellationToken);
}
