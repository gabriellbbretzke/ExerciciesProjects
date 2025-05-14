using Project.Domain.Accounts.Entities;

namespace Project.Domain.Test.Accounts.Entities;

public class StudentTest
{
    [Fact]
    public void Test1()
    {
        var student = new Student(
            "Name", 
            "LastName", 
            "teste@teste.com.br", 
            "12345678");
        
        //Persistir no banco de dados
        
    }
    
}