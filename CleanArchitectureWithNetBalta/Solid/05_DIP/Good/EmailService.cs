namespace Solid._05_DIP.Good;

public interface IEmailService
{
    void Send();
}

public class EmailService : IEmailService
{
    public void Send()
    {
        Console.WriteLine("Send Email");
    }
}

public class FakeEmailService() : IEmailService
{
    public void Send()
    {
        Console.WriteLine("Fake Send Email");
    }

}

public class UserService(IEmailService service)
{

}