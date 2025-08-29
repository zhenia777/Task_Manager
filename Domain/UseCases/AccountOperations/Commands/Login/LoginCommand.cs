using MediatR;

namespace Domain.UseCases.AccountOperations.Commands.Login;

public record class LoginCommand(string Email, string Password) : IRequest<UserLoginResultModel>;
