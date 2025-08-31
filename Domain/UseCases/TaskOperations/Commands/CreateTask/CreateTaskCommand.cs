using Domain.UseCases.TaskOperations.Models;
using MediatR;

namespace Domain.UseCases.TaskOperations.Commands.CreateTask;

public record class CreateTaskCommand(string Title, string? Description, DateTimeOffset? DueDate,
                                      int Status, int Priority) : IRequest<TaskModel>
{
    public Guid UserId { get; set; }
}