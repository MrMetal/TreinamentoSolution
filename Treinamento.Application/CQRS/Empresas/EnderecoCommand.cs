using Treinamento.Application.Interfaces;
using Treinamento.Domain;
using Treinamento.Domain.Models;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public class EnderecoCommand : Entity, IRequest<ResultData>
{
    public string Logradouro { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string? Complemento { get; set; }
    public string Cep { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Estado { get; set; } = default!;  
    public Guid EmpresaId { get; set; }
}