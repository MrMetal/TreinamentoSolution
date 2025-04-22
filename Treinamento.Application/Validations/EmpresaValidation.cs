using FluentValidation;
using Treinamento.Application.CQRS.Empresas;
using Treinamento.Application.Validations.Docs;

namespace Treinamento.Application.Validations;

public class CreateEmpresaValidation : AbstractValidator<CreateEmpresaCommand>
{
    public CreateEmpresaValidation()
    {

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("Campo obrigatório")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.RazaoSocial)
            .NotEmpty().WithMessage("Campo obrigatório");

        RuleFor(f => f.Cnpj.Length).Equal(CnpjValidacao.TamanhoCnpj)
            .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

        RuleFor(f => CnpjValidacao.Validar(f.Cnpj)).Equal(true)
            .WithMessage("O documento fornecido é inválido.");

    }
}


public class UpdateEmpresaValidation : AbstractValidator<UpdateEmpresaCommand>
{
    public UpdateEmpresaValidation()
    {

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("Campo obrigatório")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.RazaoSocial)
            .NotEmpty().WithMessage("Campo obrigatório");

        RuleFor(f => f.Cnpj.Length).Equal(CnpjValidacao.TamanhoCnpj)
            .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

        RuleFor(f => CnpjValidacao.Validar(f.Cnpj)).Equal(true)
            .WithMessage("O documento fornecido é inválido.");

    }
}