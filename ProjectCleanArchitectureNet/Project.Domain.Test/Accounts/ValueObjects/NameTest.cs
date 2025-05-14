using Project.Domain.Accounts.ValueObjects;
using Project.Domain.Accounts.ValueObjects.Exceptions;

namespace Project.Domain.Test.Accounts.ValueObjects;

public class NameTest
{
    //private readonly Name _name = new("John", "Smith");
    private readonly Name _name = Name.Create("John", "Smith");
    
    [Fact]
    public void ShouldOverrideToStringMethod()
    {
        Assert.Equal("John Smith", _name.ToString());
    }
    
    [Fact]
    public void ShouldImplicitConvertNameToString()
    {
        string data = _name;
        Assert.Equal("John Smith", data);
    }
    
    [Fact]
    public void ShoudlCreateNewName()
    {
        var  name = Name.Create("John", "Smith");
        Assert.Equal("John Smith", name.ToString());
    }
    
    [Fact]
    public void ShouldFailIfFirstNameLenghtIsNotValid()
    {
        Assert.Throws<InvalidFirstNameLenghtException>(() =>
        {
            var  name = Name.Create("J", "Smith");            
        });
    }
    
    [Fact]
    public void ShouldFailIfLastNameLenghtIsNotValid()
    {
        Assert.Throws<InvalidLastNameLenghtException>(() =>
        {
            var  name = Name.Create("John", "S");            
        });
    }
}