using Microsoft.EntityFrameworkCore;
using Treinamento.Data.Context;
using Treinamento.Domain.Models;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public interface IEnderecoService
{
    Task<Endereco?> GetEnderecoByEmpresa(Guid id);
}

public class EnderecoService(ApplicationDbContext context) : IEnderecoService
{
    public async Task<Endereco?> GetEnderecoByEmpresa(Guid id)
    {
        var enderecoEmpresa = await context.Enderecos.FirstOrDefaultAsync(x => x.EmpresaId == id);
        
        return enderecoEmpresa;
    }
}