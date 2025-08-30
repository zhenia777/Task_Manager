using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Queries.GetTaskById;

public record class GetTaskByIdQuery(Guid Id) : IRequest<TaskModel>;
