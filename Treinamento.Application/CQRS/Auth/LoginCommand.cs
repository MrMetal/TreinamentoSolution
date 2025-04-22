using Treinamento.Application.Interfaces;
using Treinamento.Domain;

namespace Treinamento.Application.CQRS.Auth;

public class LoginCommand : IRequest<ResultData>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}