using Project.Domain.Accounts.ValueObjects.Exceptions;
using Project.Domain.Shared.ValueObjects;

namespace Project.Domain.Accounts.ValueObjects;

public sealed record Name : ValueObject
{
    #region Constants

    public const int MinLength = 3;
    public const int MaxLength = 60;

    #endregion
    
    #region Constructor

    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    #endregion

    #region Factories

    public static Name Create(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName)
            || string.IsNullOrEmpty(lastName)
            || string.IsNullOrWhiteSpace(firstName)
            || string.IsNullOrWhiteSpace(lastName))
        {
            throw new InvalidNameException($"First and Last name cannot be empty");
        }
        
        if (firstName.Length < MinLength) 
            throw new InvalidFirstNameLenghtException($"First name must be at least {MinLength} characters long.");
        
        if (firstName.Length > MaxLength) 
            throw new InvalidFirstNameLenghtException($"First name cannot be more than {MaxLength} characters");
        
        if (lastName.Length < MinLength) 
            throw new InvalidLastNameLenghtException($"Last name must be at least {MinLength} characters long.");
        
        if (lastName.Length > MaxLength) 
            throw new InvalidLastNameLenghtException($"Last name cannot be more than {MaxLength} characters.");
        
        return new Name(firstName, lastName);
    }

    #endregion
    
    #region Properties
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    #endregion

    #region Operators
    
    public static implicit operator string(Name name) => name.ToString();
    
    #endregion

    #region Overrides

    public override string ToString()
        => $"{FirstName} {LastName}";

    #endregion
}