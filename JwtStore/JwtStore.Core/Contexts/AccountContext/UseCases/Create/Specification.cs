using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Name.Length, 160, "Name",  "O nome deve conter menos que 160 caracteres")
            .IsGreaterThan(request.Name.Length, 3, "Name",  "O nome deve conter mais que 3 caracteres")
            .IsLowerThan(request.Password.Length, 40, "Password",  "O senha deve conter menos que 40 caracteres")
            .IsGreaterThan(request.Password.Length, 8, "Password",  "O senha deve conter mais que 8 caracteres")
            .IsNotNullOrEmpty(request.Password, "Password", "É necessário informar a senha")
            .IsNotNullOrEmpty(request.Email, "Email", "É necessário informar o email")
            .IsEmail(request.Email, "Email", "Email inválido");
}
