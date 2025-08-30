using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Commands.UpdateTask;

public class UpdateTaskCommandHandler(IUpdateTaskStorage storage) : IRequestHandler<UpdateTaskCommand, TaskModel>
{
    private readonly IUpdateTaskStorage _storage = storage;

    public async Task<TaskModel> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        return await _storage.Update(request, cancellationToken);
    }
}
