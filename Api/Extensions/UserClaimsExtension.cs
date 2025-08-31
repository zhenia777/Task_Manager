using System.Security.Claims;

namespace API.Extensions;

public static class UserClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claims)
    {
        Guid.TryParse(claims.FindFirst("UserId")?.Value, out Guid userId);
        return userId;
    }
}
