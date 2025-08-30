using Domain.Extensions;
using Domain.Queries;
using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Queries.GetAllTasks;

public class GetTasksQueryHandler(IGetTasksStorage storage) : IRequestHandler<GetTasksQuery, PaginationList<TaskModel>>
{
    private readonly IGetTasksStorage _storage = storage;

    public Task<PaginationList<TaskModel>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_storage.GetAll(request).ToPagination(request));
    }
}
