using JwtStore.Core;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Service : IService
{
    public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
        var subject = "Verifique sua conta";
        var to = new EmailAddress(user.Email, user.Name);
        var content = $"Código {user.Email.Verification.Code}";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        await client.SendEmailAsync(msg, cancellationToken);
    }

    public void SendVerificationEmailMailTrapAsync(User user, CancellationToken cancellationToken)
    {
        var subject = "Verifique sua conta";
        var content = $"Código {user.Email.Verification.Code}";
        var client = new SmtpClient(Configuration.MailTrapIo.Server, int.Parse(Configuration.MailTrapIo.Port))
        {
            Credentials = new NetworkCredential(Configuration.MailTrapIo.CredentialUsername, Configuration.MailTrapIo.Password),
            EnableSsl = true
        };
        //client.Send("hello@demomailtrap.co", "beolbb@gmail.com", "Hello world", "testbody");
        client.Send(Configuration.Email.DefaultFromEmail, user.Email.Address, subject, content);

        //var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        //{
        //    Credentials = new NetworkCredential("13501ea3ec3cb3", "49a7bb05b55602"),
        //    EnableSsl = true
        //};
        //client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
    }
}
