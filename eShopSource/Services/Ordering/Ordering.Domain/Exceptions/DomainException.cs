namespace Ordering.Domain.Exceptions;

public class DomainException: Exception
{
    public DomainException(string message) 
        : base($"Domain Exception: \"{message}\" throws from Domain Layer.")
    {
    }
    public DomainException(string message, string paramName) 
        : base($"{message} Parameter name: {paramName}")
    {
    }
}
