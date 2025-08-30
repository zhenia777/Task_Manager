using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Commands.UpdateTask;

public record class UpdateTaskCommand(Guid Id, string Title, string? Description, DateTimeOffset? DueDate,
                                      int Status, int Priority) : IRequest<TaskModel>;
