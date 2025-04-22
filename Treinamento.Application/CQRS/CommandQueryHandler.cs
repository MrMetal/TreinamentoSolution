using FluentValidation;
using FluentValidation.Results;
using Treinamento.Domain;
using Treinamento.Domain.Interfaces;
using Treinamento.Domain.ValueObjects;

namespace Treinamento.Application.CQRS;

public abstract class CommandQueryHandler(INotificador notificador)
{
    protected ResultData Notificar(string mensagem)
    {
        notificador.Handle(new NotificacaoVo(mensagem));
        return ErrorResult();
    }

    protected ResultData SuccessResult(object? data = null, string? message = null) => notificador.ToSuccessResult(data, message);
    protected ResultData ErrorResult(string[]? errors = null) => notificador.ToErrorResult(errors);
    

    protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificar(validator);

        return false;
    }

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
            Notificar(error.ErrorMessage);
    }
}