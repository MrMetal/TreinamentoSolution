using System.ComponentModel.DataAnnotations;

namespace Treinamento.Shared.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A Senha é obrigatório")]
    [StringLength(15, ErrorMessage = "A Senha precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string? Password { get; set; }
}