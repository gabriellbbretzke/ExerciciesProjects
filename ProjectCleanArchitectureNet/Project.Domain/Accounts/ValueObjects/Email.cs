using System.Text.RegularExpressions;
using Project.Domain.Accounts.ValueObjects.Exceptions;
using Project.Domain.Shared.ValueObjects;

namespace Project.Domain.Accounts.ValueObjects;

public sealed partial record Email : ValueObject
{
    #region Constants

    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    public const int MinLength = 6;
    public const int MaxLength = 160;

    #endregion

    #region Constructor

    private Email(string address)
    {
        Address = address;
    }

    #endregion

    #region Factories

    public static Email Create(string address)
    {
        if (string.IsNullOrEmpty(address)
            || string.IsNullOrWhiteSpace(address))
            throw new InvalidEmailLenghtException($"Email cannot be null or empty.");
        
        address = address.Trim();
        address = address.ToLower();
        
        if (!EmailRegex().IsMatch(address))
            throw new InvalidEmailException("Invalid email address.");
        
        return new Email(address);
    }

    #endregion
    
    #region Properties

    public string Address { get; }
    

    #endregion

    #region Source Generators
    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
    #endregion
}