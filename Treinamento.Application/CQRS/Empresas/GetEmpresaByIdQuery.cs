using Treinamento.Application.Interfaces;
using Treinamento.Domain;

namespace Treinamento.Application.CQRS.Empresas;

public class GetEmpresaByIdQuery : IRequest<ResultData>
{
    public Guid Id { get; set; }
}