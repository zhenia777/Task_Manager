using Domain.Exceptions;
using Domain.UseCases.AccountOperations.Commands.Login;
using MediatR;

namespace Domain.UseCases.AccountOperations.Commands.Registration;

public class RegistrationCommandHandler(
    IRegistrationStorage registrationStorage,
    ILoginStorage loginStorage)
    : IRequestHandler<RegistrationCommand>
{
    private readonly IRegistrationStorage _registrationStorage = registrationStorage;
    private readonly ILoginStorage _loginStorage = loginStorage;
    public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        if (await _loginStorage.IsUserExist(request.Email, cancellationToken))
        {
            throw new DomainException(ErrorDomainStatus.BadRequest, "Email is exists!");
        }
        await _registrationStorage.CreateUser(request.Email, request.UserName,request.Password, 
                                              cancellationToken);
    }
}
