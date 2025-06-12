using Newtonsoft.Json;
using Treinamento.Domain.Interfaces;
using Treinamento.Domain.ValueObjects;

namespace Treinamento.Shared.Results;

public class ResultData<T>
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
    public IList<string> Messages { get; set; } = new List<string>();
    public required T? Data { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(this);
}

public class ResultData
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
    public object? Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];

    public override string ToString() => JsonConvert.SerializeObject(this);

}

public static class ResultExtensions
{
    public static ResultData ToErrorResult(this INotificador notificador, string[]? errors = null, int code = 400)
    {
        if (errors is { Length: > 0 })
        {
            foreach (var erro in errors)
            {
                notificador.Handle(new NotificacaoVo(erro));
            }
        }

        return new ResultData
        {
            Success = false,
            Code = code,
            Data = null,
            Errors = notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList(),
            Message = "Erro ao processar a requisição.",
        };
    }

    public static ResultData ToSuccessResult(this INotificador notificador, object? data = null, string? message = null)
    {
        return new ResultData
        {
            Success = true,
            Code = 200,
            Message = message ?? "Operação realizada com sucesso!",
            Data = data
        };
    }
}