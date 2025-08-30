using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.UseCases.TaskOperations.Commands.CreateTask;
using Domain.UseCases.TaskOperations.Models;
using Microsoft.EntityFrameworkCore;
using Storage.Entities;

namespace Storage.Storages.TaskStorage;

public class CreateTaskStorage(TaskManagerDbContext dbContext, IMapper mapper ) : ICreateTaskStorage
{
    private readonly TaskManagerDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<TaskModel> CreateTask(CreateTaskCommand data, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstAsync(u => u.Id == data.UserId, cancellationToken);

        TaskEntity task = new TaskEntity
        {
            Title = data.Title,
            Description = data.Description,
            DueDate = data.DueDate,
            Status = (Storage.Enum.TasksStatus)data.Status,
            Priority = (Storage.Enum.TasksPriority)data.Priority,
            UserId = data.UserId
        };

        await _dbContext.Tasks.AddAsync(task, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return await _dbContext.Tasks
            .AsNoTracking()
            .ProjectTo<TaskModel>(_mapper.ConfigurationProvider)
            .FirstAsync();
    }
}
