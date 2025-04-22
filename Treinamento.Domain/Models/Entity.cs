namespace Treinamento.Domain.Models;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DataCadastro { get; set; }
    public DateTime DataAlteracao { get; set; }
    public bool Ativo { get; set; } = true;
    public bool IsSynced { get; set; } = true;
}