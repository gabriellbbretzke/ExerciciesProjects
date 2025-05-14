using Project.Domain.Shared.Exceptions;

namespace Project.Domain.Accounts.ValueObjects.Exceptions;

public sealed class InvalidNameException(string message) : DomainException(message)
{
    
}