using MediatR;

namespace Domain.UseCases.TaskOperations.Commands.DeleteTask;

public record class DeleteTaskCommand(Guid Id) : IRequest;

