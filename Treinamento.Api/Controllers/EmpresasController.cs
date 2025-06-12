using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Treinamento.Application.CQRS.Empresas;
using Treinamento.Application.Interfaces;
using Treinamento.Data.Identity;
using Treinamento.Domain;
using Treinamento.Domain.Interfaces;
using Treinamento.Shared.Results;

namespace Treinamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class EmpresasController(INotificador notificador, IMediator mediator) : MainController(notificador, mediator)
    {
        [HttpGet]
        [CustomAuthorization.ClaimsAuthorize("Admin,Comum", "Read")]
        public async Task<ResultData> GetAll()
            => await Mediator.Send(new GetAllEmpresasQuery());

        [HttpGet("{id:guid}")]
        public async Task<ResultData> GetById(Guid id)
            => await Mediator.Send(new GetEmpresaByIdQuery{Id = id});

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateEmpresaCommand command) 
            => CustomResponse(await Mediator.Send(command));

        [HttpPut("/empresa")]
        public async Task<IActionResult> Update([FromBody] UpdateEmpresaCommand command) 
            => CustomResponse(await Mediator.Send(command));

        [HttpPut("/endereco")]
        public async Task<IActionResult> Update([FromBody] EnderecoCommand command) 
            => CustomResponse(await Mediator.Send(command));
    }
}
