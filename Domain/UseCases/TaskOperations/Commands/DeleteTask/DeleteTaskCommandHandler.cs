using MediatR;

namespace Domain.UseCases.TaskOperations.Commands.DeleteTask;

public class DeleteTaskCommandHandler(IDeleteTaskStorage storage) : IRequestHandler<DeleteTaskCommand>
{
    private readonly IDeleteTaskStorage _storage = storage;

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await _storage.Delete(request.Id, cancellationToken);
    }
}
