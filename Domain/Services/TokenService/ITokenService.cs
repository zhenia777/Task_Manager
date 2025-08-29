namespace Domain.Services.TokenService;

public interface ITokenService
{
    string CreateToken(Guid userId);
}
