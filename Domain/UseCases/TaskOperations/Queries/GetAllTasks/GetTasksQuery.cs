using Domain.Queries;
using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Queries.GetAllTasks;

public record class GetTasksQuery(Guid UserId) : PaginationQuery, IRequest<PaginationList<TaskModel>>
{
    public int? Status { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public int? Priority { get; set; }
}

