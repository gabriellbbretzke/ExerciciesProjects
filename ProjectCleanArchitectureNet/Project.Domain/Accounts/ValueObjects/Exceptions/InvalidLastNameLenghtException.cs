using Project.Domain.Shared.Exceptions;

namespace Project.Domain.Accounts.ValueObjects.Exceptions;

public sealed class InvalidLastNameLenghtException(string message) 
    : DomainException(message);