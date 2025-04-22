using System.ComponentModel;

namespace Treinamento.Shared.Enums;

public enum TipoUsuario
{
    Selecione = 0,

    [Description("Super Admin")]
    SuperAdmin = 1,

    [Description("Admin")]
    Admin = 2,

    [Description("Comum")]
    Comum = 3,
}