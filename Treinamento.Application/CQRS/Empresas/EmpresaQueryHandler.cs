using Treinamento.Application.Interfaces;
using Treinamento.Data.Repositories;
using Treinamento.Domain;
using Treinamento.Domain.Interfaces;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public class EmpresaQueryHandler(INotificador notificador, IEmpresaRepository empresaRepository) : 
    CommandQueryHandler(notificador), 
    IRequestHandler<GetAllEmpresasQuery, ResultData>,
    IRequestHandler<GetEmpresaByIdQuery, ResultData>
{
    public async Task<ResultData> Handle(GetAllEmpresasQuery request, CancellationToken cancellationToken)
    {
        var empresas = await empresaRepository.ObterTodos();
        var result = empresas.Select(x => new EmpresaResult { Nome = x.Nome, RazaoSocial = x.RazaoSocial }).ToArray();
        
        return SuccessResult(result);
    }

    public async Task<ResultData> Handle(GetEmpresaByIdQuery request, CancellationToken cancellationToken)
    {
        var empresa = await empresaRepository.ObterPorId(request.Id);

        if (empresa is null) return ErrorResult(["Empresa não encontrada"]);

        var result = new EmpresaResult
        {
            Nome = empresa.Nome,
            RazaoSocial = empresa.RazaoSocial
        };

        return SuccessResult(result);
    }
}