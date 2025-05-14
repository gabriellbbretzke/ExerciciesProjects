using Project.Domain.Shared.Exceptions;

namespace Project.Domain.Accounts.ValueObjects.Exceptions;

public sealed class InvalidEmailLenghtException(string message) 
    : DomainException(message);