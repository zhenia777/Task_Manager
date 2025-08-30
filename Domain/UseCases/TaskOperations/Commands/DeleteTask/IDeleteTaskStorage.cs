namespace Domain.UseCases.TaskOperations.Commands.DeleteTask;

public interface IDeleteTaskStorage
{
    Task Delete(Guid id, CancellationToken cancellationToken);
}
