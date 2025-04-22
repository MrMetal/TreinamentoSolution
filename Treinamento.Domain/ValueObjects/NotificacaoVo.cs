namespace Treinamento.Domain.ValueObjects;

public class NotificacaoVo(string mensagem)
{
    public string Mensagem { get; set; } = mensagem;
}