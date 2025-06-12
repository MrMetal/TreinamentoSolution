using Microsoft.EntityFrameworkCore;
using Treinamento.Data.Context;
using Treinamento.Domain.Models;
using Treinamento.Shared.Results;

namespace Treinamento.Application.CQRS.Empresas;

public interface IEmpresaService
{
    Task<Endereco?> GetEnderecoByEmpresa(Guid id);
    Task<EmpresaResult?> GetEmpresaById(Guid id);
}

public class EmpresaService(ApplicationDbContext context) : IEmpresaService
{
    public async Task<Endereco?> GetEnderecoByEmpresa(Guid id)
    {
        return await context.Enderecos.FirstOrDefaultAsync(x => x.EmpresaId == id);
    }

    public async Task<EmpresaResult?> GetEmpresaById(Guid id)
    {
        var empresa = await context.Empresas
            .AsNoTracking()
            .Include(x => x.Endereco)
            .Include(x => x.Setor)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (empresa is null) return null;

        var result = new EmpresaResult
        {
            Nome = empresa.Nome,
            RazaoSocial = empresa.RazaoSocial,
            Cnpj = empresa.Cnpj,
            Contato = empresa.Contato,
            Email = empresa.Email,
            UserId = empresa.UserId,
            Endereco = new EnderecoResult(empresa.Endereco)
        };

        return result;
    }
}