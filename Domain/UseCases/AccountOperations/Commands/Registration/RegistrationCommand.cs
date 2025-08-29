using MediatR;

namespace Domain.UseCases.AccountOperations.Commands.Registration;

public record class RegistrationCommand(string UserName, string Email, string Password) : IRequest;
