using Treinamento.Application.Interfaces;
using Treinamento.Data.Repositories;
using Treinamento.Domain;
using Treinamento.Domain.Interfaces;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public class EmpresaQueryHandler(INotificador notificador, 
    IEmpresaRepository empresaRepository, 
    IEmpresaService empresaService) : 
    CommandQueryHandler(notificador), 
    IRequestHandler<GetAllEmpresasQuery, ResultData>,
    IRequestHandler<GetEmpresaByIdQuery, ResultData>
{
    public async Task<ResultData> Handle(GetAllEmpresasQuery request, CancellationToken cancellationToken)
    {
        var empresas = await empresaRepository.ObterTodos();

        var result = empresas
            .Select(x => new EmpresaResult { Id = x.Id, Nome = x.Nome, RazaoSocial = x.RazaoSocial })
            .ToArray();
        
        return SuccessResult(result);
    }

    public async Task<ResultData> Handle(GetEmpresaByIdQuery request, CancellationToken cancellationToken)
    {
        var empresa = await empresaService.GetEmpresaById(request.Id);

        return empresa is null ? ErrorResult(["Empresa não encontrada"]) : SuccessResult(empresa);
    }
}