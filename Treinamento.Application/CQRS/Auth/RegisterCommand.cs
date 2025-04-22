using Treinamento.Application.Interfaces;
using Treinamento.Domain;
using Treinamento.Shared.Enums;

namespace Treinamento.Application.CQRS.Auth;

public class RegisterCommand : IRequest<ResultData>
{
    public  required string Email { get; set; }
    public  required string Password { get; set; }
    public  required string ConfirmPassword { get; set; }
    public  required TipoUsuario Tipo { get; set; }
    public  required string Nome { get; set; }
    public  required string SobreNome { get; set; }
    public  required string Contato { get; set; }
}