using Treinamento.Application.Interfaces;
using Treinamento.Domain.Models;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public class CreateEmpresaCommand : Entity, IRequest<ResultData>
{
    public required string Nome { get; set; }
    public required string RazaoSocial { get; set; }
    public required string Cnpj { get; set; }
    public string? Contato { get; set; }
    public string? Email { get; set; }
    public Guid UserId { get; set; }

    public EnderecoCommand Endereco { get; set; } = default!;
}