using Treinamento.Application.Interfaces;
using Treinamento.Domain;
using Treinamento.Domain.Models;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public class UpdateEmpresaCommand : Entity, IRequest<ResultData>
{
    public string Nome { get; set; } = default!;
    public string RazaoSocial { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public string? Contato { get; set; }
    public string? Email { get; set; }
    public Guid UserId { get; set; }
}