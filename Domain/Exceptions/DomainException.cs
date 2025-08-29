namespace Domain.Exceptions;

public class DomainException : Exception
{
    public ErrorDomainStatus Code { get; set; }

    public List<string> Messages { get; set; } = new();

    public DomainException(ErrorDomainStatus code, string message) : base(message)
    {
        Code = code;

    }
    public DomainException(ErrorDomainStatus code, List<string> messages)
    {
        Code = code;
        Messages = messages;
    }
}
