using Treinamento.Application.Interfaces;
using Treinamento.Domain;

namespace Treinamento.Application.CQRS.Empresas;

public class GetAllEmpresasQuery : IRequest<ResultData>;