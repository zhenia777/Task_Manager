using Domain.Exceptions;
using Domain.Services.TokenService;
using MediatR;

namespace Domain.UseCases.AccountOperations.Commands.Login;

public class LoginCommandHandler(ITokenService tokenService,
                                 ILoginStorage loginStorage)
                         : IRequestHandler<LoginCommand, UserLoginResultModel>
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly ILoginStorage _loginStorage = loginStorage;

    public async Task<UserLoginResultModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (!await _loginStorage.IsUserExist(request.Email, cancellationToken) ||
            !await _loginStorage.CheckPassword(request.Email, request.Password, cancellationToken))
        {
            throw new DomainException(ErrorDomainStatus.UnAuthorize, "Invalid login or password!");
        }

        var userId = await _loginStorage.GetUserId(request.Email, cancellationToken);
        return new UserLoginResultModel
        {
            Token = _tokenService.CreateToken(userId),
            UserName = request.Email
        };
    }
}

