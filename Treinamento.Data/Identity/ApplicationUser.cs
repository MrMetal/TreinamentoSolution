using Microsoft.AspNetCore.Identity;
using Treinamento.Shared.Enums;

namespace Treinamento.Data.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string Nome { get; set; } 
    public string Sobrenome { get; set; } 
    public string Contato { get; set; } 
    public bool Bloqueado { get; set; }
    public bool Ativo { get; set; } = true;
    public TipoUsuario Tipo { get; set; }

    /// <summary>
    /// Usuario admin que está criando outro usuario comum
    /// </summary>
    public string? UsuarioId { get; set; }

    public void AtualizarDados(string nome, string sobrenome, string contato, TipoUsuario tipo)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Contato = contato;
        Tipo = tipo;
    }
}
