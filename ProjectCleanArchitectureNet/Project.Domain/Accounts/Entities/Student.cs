using Project.Domain.Accounts.ValueObjects;
using Project.Domain.Shared.Entities;

namespace Project.Domain.Accounts.Entities;

public sealed class Student : Entity
{
    #region Constructors

    public Student(
        string firstName, 
        string lastName, 
        string email, 
        string password) : base(Guid.CreateVersion7())
    {
        Name = Name.Create(firstName, lastName);
        Email = email;
        Password = password;
    }

    #endregion

    public Name Name { get; }
    public string Email { get; } = string.Empty;
    public string Password { get; } = string.Empty;
}