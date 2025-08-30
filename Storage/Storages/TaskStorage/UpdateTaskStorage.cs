using AutoMapper;
using Domain.UseCases.TaskOperations.Commands.UpdateTask;
using Domain.UseCases.TaskOperations.Models;
using Microsoft.EntityFrameworkCore;
using Storage.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Storage.Storages.TaskStorage;

public class UpdateTaskStorage(TaskManagerDbContext dbContext, IMapper mapper) : IUpdateTaskStorage
{
    private readonly TaskManagerDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<TaskModel> Update(UpdateTaskCommand taskModel, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks
                                   .FirstAsync(x => x.Id == taskModel.Id, cancellationToken);
        task.Id = taskModel.Id;
        task.Title = taskModel.Title;
        task.Description = taskModel.Description;
        task.DueDate = taskModel.DueDate;
        task.Status = (Storage.Enum.TasksStatus)taskModel.Status;
        task.Priority = (Storage.Enum.TasksPriority)taskModel.Priority;
        await _dbContext.SaveChangesAsync(cancellationToken);

        TaskEntity result = await _dbContext.Tasks
                                            .AsNoTracking()
                                            .SingleAsync(p => p.Id == task.Id, cancellationToken);
        return _mapper.Map<TaskModel>(result);
    }
}
