using Treinamento.Domain.Models;

namespace Treinamento.Shared.Results;

public class EnderecoResult : Entity
{
    public EnderecoResult(Endereco? endereco)
    {
        Logradouro = endereco?.Logradouro;
        Numero = endereco?.Numero;
        Complemento = endereco?.Numero;
        Cep = endereco?.Cep;
        Bairro = endereco?.Bairro;
        Cidade = endereco?.Cidade;
        Estado = endereco?.Estado;
        EmpresaId = endereco?.EmpresaId;
    }

    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Cep { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    public Guid? EmpresaId { get; set; }
}