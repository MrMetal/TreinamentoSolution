﻿using FluentValidation;
using Treinamento.Application.CQRS.Empresas;

namespace Treinamento.Application.Validations;


public class EnderecoValidation : AbstractValidator<EnderecoCommand>
{
    public EnderecoValidation()
    {
        RuleFor(c => c.Logradouro)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Bairro)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Cep)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(8).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");

        RuleFor(c => c.Cidade)
            .NotEmpty().WithMessage("A campo {PropertyName} precisa ser fornecida")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Estado)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Numero)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}