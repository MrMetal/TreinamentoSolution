using Treinamento.Application.Interfaces;
using Treinamento.Shared.Results;
using Treinamento.Shared.ViewModels;

namespace Treinamento.Application.CQRS.Auth;

public class RegisterCommand : RegisterViewModel, IRequest<ResultData>;