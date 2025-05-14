using Project.Domain.Shared.Exceptions;

namespace Project.Domain.Accounts.ValueObjects.Exceptions;

public sealed class InvalidEmailException(string message) 
    : DomainException(message);