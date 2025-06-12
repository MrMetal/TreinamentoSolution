using Treinamento.Domain.Models;

namespace Treinamento.Shared.Results;

public class EmpresaResult : Entity
{
    public string Nome { get; set; }
    public string RazaoSocial { get; set; }
    public string Cnpj { get; set; }
    public string? Contato { get; set; }
    public string? Email { get; set; }
    public Guid UserId { get; set; }

    public virtual EnderecoResult? Endereco { get; set; }
    public virtual ICollection<SetorResult> Setor { get; set; } = [];
}