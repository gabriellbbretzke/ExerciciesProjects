using Project.Domain.Shared.Exceptions;

namespace Project.Domain.Accounts.ValueObjects.Exceptions;

public sealed class InvalidFirstNameLenghtException(string message) 
    : DomainException(message);