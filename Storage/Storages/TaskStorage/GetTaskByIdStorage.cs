using AutoMapper;
using Domain.UseCases.TaskOperations.Models;
using Domain.UseCases.TaskOperations.Queries.GetTaskById;
using Microsoft.EntityFrameworkCore;

namespace Storage.Storages.TaskStorage;

public class GetTaskByIdStorage(TaskManagerDbContext dbContext, IMapper mapper) : IGetTaskByIdStorage
{
    private readonly TaskManagerDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<TaskModel> GetTaskById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks
                                   .AsNoTracking()
                                   .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

        return _mapper.Map<TaskModel>(task);
    }
}
