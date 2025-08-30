using Domain.UseCases.TaskOperations.Commands.DeleteTask;
using Microsoft.EntityFrameworkCore;

namespace Storage.Storages.TaskStorage;

public class DeleteTaskStorage(TaskManagerDbContext dbContext) : IDeleteTaskStorage
{
    private readonly TaskManagerDbContext _dbContext = dbContext;

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks
                                   .AsNoTracking()
                                   .FirstAsync(p => p.Id == id, cancellationToken);
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
