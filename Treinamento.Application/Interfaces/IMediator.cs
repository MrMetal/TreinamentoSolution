namespace Treinamento.Application.Interfaces;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Send(IRequest request, CancellationToken cancellationToken = default);
}

public class Mediator(IServiceProvider provider) : IMediator
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        var handler = provider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException("Handler not found");

        var method = handlerType.GetMethod("Handle");
        if (method == null)
            throw new InvalidOperationException("Handle method not found");

        return await (Task<TResponse>)method.Invoke(handler, [request, cancellationToken])!;
    }

    public async Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<>)
            .MakeGenericType(request.GetType());

        var handler = provider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException("Handler not found");

        var method = handlerType.GetMethod("Handle");
        if (method == null)
            throw new InvalidOperationException("Handle method not found");

        await (Task)method.Invoke(handler, [request, cancellationToken])!;
    }
}