using Domain.UseCases.TaskOperations.Models;

namespace Domain.UseCases.TaskOperations.Commands.CreateTask;

public interface ICreateTaskStorage
{
    Task<TaskModel> CreateTask(CreateTaskCommand data, CancellationToken cancellationToken);
}
