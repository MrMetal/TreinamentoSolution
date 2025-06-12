using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Treinamento.Application.Interfaces;
using Treinamento.Application.Validations;
using Treinamento.Data.Identity;
using Treinamento.Data.Identity.Jwt;
using Treinamento.Domain.Interfaces;
using Treinamento.Shared.Enums;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Auth;

public class AuthCommandHandler(INotificador notificador,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IOptions<AppSettings> appSettings,
    TokenSettings tokenSettings) : 
    CommandQueryHandler(notificador), 
    IRequestHandler<RegisterCommand, ResultData>, 
    IRequestHandler<LoginCommand, ResultData>
{
    public async Task<ResultData> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!ExecutarValidacao(new RegisterCommandValidator(), request)) return ErrorResult();

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true,
            UsuarioId = Guid.Empty.ToString(),
            Contato = request.Contato,
            Nome = request.Nome,
            Sobrenome = request.SobreNome,
            Tipo = request.Tipo
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToArray();
            return ErrorResult(errors);
        }

        var claimType = request.Tipo is TipoUsuario.Admin or TipoUsuario.SuperAdmin ? "Admin" : "Comum";

        await userManager.AddClaimAsync(user, new Claim(claimType, "Create,Read,Update,Delete"));
        await signInManager.SignInAsync(user, false);

        return SuccessResult();
    }

    public async Task<ResultData> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        request.Email = request.Email!.ToLower();
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return ErrorResult(["Usuario nao encontrado."]);

        if (user.EmailConfirmed != true)
            return ErrorResult(["Email Não confirmado."]);

        if (user.Bloqueado)
            return ErrorResult(["Usuario Bloqueado! Entre em contato com o suporte para mais informações."]);

        var result = await signInManager.PasswordSignInAsync(request.Email, request.Password!, false, true);

        if (result.IsLockedOut)
            return ErrorResult(["Usuário temporariamente bloqueado por tentativas inválidas"]);

        if (!result.Succeeded)
            return ErrorResult(["Usuário ou Senha incorretos"]);

        var token = await tokenSettings.GerarJwt(user.Email!, appSettings.Value);
        
        return SuccessResult(token);
    }
}