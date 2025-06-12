using Treinamento.Domain.Models;

namespace Treinamento.Shared.Results;

public class SetorResult : Entity
{
    public string Nome { get;  set; }
    public int QtdPessoas { get;  set; }
    public Guid EmpresaId { get;  set; }
}