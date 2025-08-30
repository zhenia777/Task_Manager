using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.UseCases.TaskOperations.Models;
using Domain.UseCases.TaskOperations.Queries.GetAllTasks;
using Microsoft.EntityFrameworkCore;

namespace Storage.Storages.TaskStorage;

public class GetTasksStorage(TaskManagerDbContext dbContext, IMapper mapper) : IGetTasksStorage
{
    private readonly TaskManagerDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public IQueryable<TaskModel> GetAll(GetTasksQuery query)
    {
        var tasks = _dbContext.Tasks.AsNoTracking();

        if (query.Status != null)
        {
            var status = (Storage.Enum.TasksStatus)query.Status.Value;
            tasks = tasks.Where(x => x.Status == status);
        }

        if (query.DueDate != null)
        {
            tasks = tasks.Where(x => x.DueDate == query.DueDate);
        }

        if (query.Priority != null)
        {
            var priority = (Storage.Enum.TasksPriority)query.Priority.Value;
            tasks = tasks.Where(x => x.Priority == priority);
        }

        return tasks.ProjectTo<TaskModel>(_mapper.ConfigurationProvider);
    }
}
