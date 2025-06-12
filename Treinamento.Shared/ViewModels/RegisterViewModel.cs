using Treinamento.Shared.Enums;

namespace Treinamento.Shared.ViewModels;

public class RegisterViewModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public required TipoUsuario Tipo { get; set; }
    public required string Nome { get; set; }
    public required string SobreNome { get; set; }
    public required string Contato { get; set; }
}