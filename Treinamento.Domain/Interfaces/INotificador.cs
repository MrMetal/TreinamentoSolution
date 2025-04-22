using Treinamento.Domain.ValueObjects;

namespace Treinamento.Domain.Interfaces;

public interface INotificador
{
    bool TemNotificacao();
    List<NotificacaoVo> ObterNotificacoes();
    void Handle(NotificacaoVo notificacao);
}

public class Notificador : INotificador
{
    private readonly List<NotificacaoVo> _notificacoes = [];

    public bool TemNotificacao() => _notificacoes.Any();

    public List<NotificacaoVo> ObterNotificacoes() => _notificacoes;

    public void Handle(NotificacaoVo notificacao) => _notificacoes.Add(notificacao);
}