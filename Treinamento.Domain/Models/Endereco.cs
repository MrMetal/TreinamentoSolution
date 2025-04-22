namespace Treinamento.Domain.Models;

public class Endereco : Entity
{
    public Endereco(string logradouro, string numero, string? complemento, 
        string cep, string bairro, string cidade, string estado)
    {
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Cep = cep;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }

    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string Cep { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }

    public Guid EmpresaId { get; private set; }
    public virtual Empresa Empresa { get; set; }

    public void AtualizarTodosDados(string logradouro, string numero, string? complemento, string cep, string bairro, string cidade, string estado)
    {
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Cep = cep;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }
}