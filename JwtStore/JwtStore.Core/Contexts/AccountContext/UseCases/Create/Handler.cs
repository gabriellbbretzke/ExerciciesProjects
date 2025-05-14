using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(Request request,CancellationToken cancellationToken)
    {
        #region 01.Valida a requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid) 
                return new Response("Requisição inválida", 400, res.Notifications);
            }
        catch (Exception e)
        {
            return new Response("Não foi possível validar sua requisição", 500, null);
        }

        #endregion

        #region 02. Gerar os objetos

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception e)
        {
            return new Response(e.Message, 400);
        }

        #endregion

        #region 03.Verificar se o usuário existe

        var exists = await _repository.AnyAsync(request.Email, cancellationToken);

        try
        {
            if (exists)
                return new Response("Este email já está em uso", 400);
        }
        catch (Exception e)
        {
            return new Response("Não foi possível verificar se o email já está em uso", 500);
        }

        #endregion

        #region 04.Persistir os dados

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Falha ao persistir os dados", 500);
        }

        #endregion

        #region 05.Enviar email de ativação

        try
        {
            _service.SendVerificationEmailMailTrapAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            //Do nothing;x
        }

        #endregion

        return new Response("Conta criada", new ResponseData(user.Id, user.Name, user.Email));
    }
}
