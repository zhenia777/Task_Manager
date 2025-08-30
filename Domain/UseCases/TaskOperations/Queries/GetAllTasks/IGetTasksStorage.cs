using Domain.UseCases.TaskOperations.Models;

namespace Domain.UseCases.TaskOperations.Queries.GetAllTasks;

public interface IGetTasksStorage
{
    IQueryable<TaskModel> GetAll(GetTasksQuery query);
}
