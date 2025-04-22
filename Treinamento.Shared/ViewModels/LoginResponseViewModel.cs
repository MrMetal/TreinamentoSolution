using Treinamento.Shared.Enums;

namespace Treinamento.Shared.ViewModels;

public class LoginResponseViewModel
{
    public string AccessToken { get; set; } = default!;
    public double ExpiresIn { get; set; }
    public UserTokenViewModel UserToken { get; set; } = default!;
}

public class UserTokenViewModel
{
    public string Id { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string NomeUsuario { get; set; } = default!;
    public string SobreNome { get; set; } = default!;
    public TipoUsuario TipoUsuario { get; set; } = default!;
    public bool Bloqueado { get; set; }
    public bool Ativo { get; set; }
    public IEnumerable<ClaimViewModel> Claims { get; set; } = default!;
}

public class ClaimViewModel
{
    public string Value { get; set; } = default!;
    public string Type { get; set; } = default!;
}