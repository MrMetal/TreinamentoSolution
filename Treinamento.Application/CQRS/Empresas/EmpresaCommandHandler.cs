using Treinamento.Application.Interfaces;
using Treinamento.Application.Validations;
using Treinamento.Data.Interfaces;
using Treinamento.Data.Repositories;
using Treinamento.Domain;
using Treinamento.Domain.Interfaces;
using Treinamento.Domain.Models;

namespace Treinamento.Application.CQRS.Empresas;

public class EmpresaCommandHandler(INotificador notificador, 
    IUser appUser, 
    IEmpresaRepository empresaRepository, 
    IEnderecoRepository enderecoRepository,
    IUnitOfWork unitOfWork, 
    IEnderecoService enderecoService) : 
    CommandQueryHandler(notificador),
    IRequestHandler<CreateEmpresaCommand, ResultData>,
    IRequestHandler<UpdateEmpresaCommand, ResultData>,
    IRequestHandler<EnderecoCommand, ResultData>
{
    public async Task<ResultData> Handle(CreateEmpresaCommand request, CancellationToken cancellationToken)
    {
        if (!ExecutarValidacao(new CreateEmpresaValidation(), request) ||
            !ExecutarValidacao(new EnderecoValidation(), request.Endereco)) return ErrorResult();

        var empresa = new Empresa(request.Nome, request.RazaoSocial, request.Cnpj, appUser.GetUserId())
        {
            Endereco = new Endereco(request.Endereco.Logradouro, request.Endereco.Numero, request.Endereco.Complemento,
                request.Endereco.Cep, request.Endereco.Bairro, request.Endereco.Cidade, request.Endereco.Estado)
        };

        empresaRepository.Adicionar(empresa);

        await unitOfWork.SaveChangesAsync();
        return SuccessResult();
    }

    //Pode ser adicionado uma logica para atualizar empresa e endereço juntos.
    public async Task<ResultData> Handle(UpdateEmpresaCommand request, CancellationToken cancellationToken)
    {
        if (!ExecutarValidacao(new UpdateEmpresaValidation(), request)) return ErrorResult();

        var empresa = await empresaRepository.ObterPorId(request.Id);

        if (empresa is null) return ErrorResult(["Empresa não encontrada"]);

        empresa.AtualizarTodosDados(request.Nome, request.RazaoSocial, request.Cnpj, request.Contato, request.Email);

        empresaRepository.Atualizar(empresa);

        await unitOfWork.SaveChangesAsync();
        return SuccessResult();
    }

    /// <summary>
    /// Atualiza somente o endereço da empresa
    /// </summary>
    public async Task<ResultData> Handle(EnderecoCommand request, CancellationToken cancellationToken)
    {
        if (!ExecutarValidacao(new EnderecoValidation(), request)) return ErrorResult();

        var enderecoEmpresa = await enderecoService.GetEnderecoByEmpresa(request.EmpresaId);

        if (enderecoEmpresa is null) return ErrorResult(["Endereço da empresa não encontrado"]);

        enderecoEmpresa.AtualizarTodosDados(request.Logradouro, request.Numero, request.Complemento, request.Cep, request.Bairro, request.Cidade, request.Estado);

        enderecoRepository.Atualizar(enderecoEmpresa);

        await unitOfWork.SaveChangesAsync();
        return SuccessResult();
    }
}