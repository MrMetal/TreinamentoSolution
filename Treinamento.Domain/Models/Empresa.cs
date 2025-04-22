namespace Treinamento.Domain.Models;

public class Empresa : Entity
{
    public Empresa(string nome, string razaoSocial, string cnpj, Guid userId)
    {
        Nome = nome;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
        UserId = userId;
    }

    public string Nome { get; private set; } 
    public string RazaoSocial { get; private set; }
    public string Cnpj { get; private set; }
    public string? Contato { get; private set; }
    public string? Email { get; private set; }
    public Guid UserId { get; private set; }

    public virtual Endereco? Endereco { get; set; }
    public virtual ICollection<Setor> Setor { get; set; } = [];

    public void AtualizarTodosDados(string nome, string razaoSocial, string cnpj, string? contato, string? email)
    {
        Nome = nome;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
        Contato = contato;
        Email = email;
    }

    public void AtualizarEndereco(Endereco endereco)
    {
        Endereco!.AtualizarTodosDados(endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Cep, endereco.Bairro, endereco.Cidade, endereco.Estado);
    }
}