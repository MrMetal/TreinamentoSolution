using Microsoft.AspNetCore.Mvc;
using Treinamento.Application.Interfaces;
using Treinamento.Domain;
using Treinamento.Domain.Interfaces;

namespace Treinamento.Api.Controllers;

[ApiController]
public abstract class MainController(INotificador notificador, IMediator mediator) : ControllerBase
{
    public readonly IMediator Mediator = mediator;

    protected bool OperacaoValida() => !notificador.TemNotificacao();

    protected ActionResult CustomResponse(object? result = null!)
    {
        if (OperacaoValida())
            return Ok(result);

        var errors = notificador.ObterNotificacoes().Select(n => n.Mensagem);

        return BadRequest(new ResultData
        {
            Success = false,
            Errors = errors
        });
    }
}