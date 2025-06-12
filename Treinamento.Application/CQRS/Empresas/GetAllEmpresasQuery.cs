using Treinamento.Application.Interfaces;
using Treinamento.Domain;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public class GetAllEmpresasQuery : IRequest<ResultData>;