namespace Domain.Exceptions;

public enum ErrorDomainStatus
{
    ValidationError = 409,
    InternalServerError = 500,
    BadRequest = 400,
    UnAuthorize = 401,
    NotFound = 404
}
