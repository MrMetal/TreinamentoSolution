namespace Treinamento.Domain.Models;

public class Setor : Entity
{
    public Setor(string nome, int qtdPessoas, Guid empresaId)
    {
        Nome = nome;
        QtdPessoas = qtdPessoas;
        EmpresaId = empresaId;
    }

    public string Nome { get; private set; }
    public int QtdPessoas { get; private set; }

    public Guid EmpresaId { get; private set; }
    public virtual Empresa Empresa { get; set; }
}