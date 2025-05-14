using Project.Domain.Accounts.ValueObjects;
using Project.Domain.Accounts.ValueObjects.Exceptions;

namespace Project.Domain.Test.Accounts.ValueObjects;

public class EmailTest
{
    [Theory]
    [InlineData("test@gmail.com")]
    [InlineData("test@hotmail.com")]
    [InlineData("test@test.com.br")]
    public void ShouldCreateAnEmail(string address)
    {
        var email = Email.Create(address);
        Assert.Equal(email.Address, address);
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void ShouldFailToCreateAnEmail(string address)
    {
        Assert.Throws<InvalidEmailLenghtException>(() =>
        {
            var email = Email.Create(address);
        });
    }
    
    [Theory]
    [InlineData("asdasdasdasd")]
    [InlineData("asd.com")]
    public void ShouldFailToCreateAnEmailIfAddressIsNotValid(string address)
    {
        Assert.Throws<InvalidEmailException>(() =>
        {
            var email = Email.Create(address);
        });
    }

}