using Treinamento.Domain;
using Treinamento.Shared.Results;

namespace Treinamento.Application.Interfaces;

public interface IRequest<TResponse>;
public interface IRequest;

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}


public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task<ResultData> Handle(TRequest request, CancellationToken cancellationToken);
}
