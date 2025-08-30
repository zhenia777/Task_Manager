using Domain.UseCases.TaskOperations.Models;

namespace Domain.UseCases.TaskOperations.Commands.UpdateTask;

public interface IUpdateTaskStorage
{
    Task<TaskModel> Update(UpdateTaskCommand taskModel, CancellationToken cancellationToken);

}
