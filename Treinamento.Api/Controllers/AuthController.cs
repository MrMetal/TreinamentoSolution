using Microsoft.AspNetCore.Mvc;
using Treinamento.Application.CQRS.Auth;
using Treinamento.Application.Interfaces;
using Treinamento.Domain.Interfaces;

namespace Treinamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(INotificador notificador, IMediator mediator) : MainController(notificador, mediator)
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
            => CustomResponse(await Mediator.Send(command));

        [HttpPost("Login")]
        public async Task<IActionResult> Register([FromBody] LoginCommand command)
            => CustomResponse(await Mediator.Send(command));
    }
}
