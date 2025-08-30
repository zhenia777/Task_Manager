using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Commands.CreateTask;

public class CreateTaskCommandHandler(ICreateTaskStorage storage) : IRequestHandler<CreateTaskCommand, TaskModel>
{
    private readonly ICreateTaskStorage _storage = storage;

    public async Task<TaskModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        return await _storage.CreateTask(request, cancellationToken);
    }
}
