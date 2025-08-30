using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Queries.GetTaskById;

public class GetTaskByIdQueryHandler(IGetTaskByIdStorage storage) : IRequestHandler<GetTaskByIdQuery, TaskModel>
{
    private readonly IGetTaskByIdStorage _storage = storage;

    public async Task<TaskModel> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _storage.GetTaskById(request.Id, cancellationToken);
    }
}
