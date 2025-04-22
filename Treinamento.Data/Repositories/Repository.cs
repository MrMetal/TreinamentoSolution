using Microsoft.EntityFrameworkCore;
using Treinamento.Data.Context;
using Treinamento.Domain.Interfaces;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Repositories;

public class Repository<TEntity>(ApplicationDbContext db) : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext Db = db;
    protected readonly DbSet<TEntity> DbSet = db.Set<TEntity>();

    public void Adicionar(TEntity entity) => DbSet.Add(entity);

    public void Atualizar(TEntity entity) => DbSet.Update(entity);

    public async Task Remover(Guid id)
    {
        var entity = await DbSet.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        DbSet.Remove(entity ?? throw new Exception($"{entity} não encontrado(a)."));
    }

    public async Task<TEntity?> ObterPorId(Guid id)
        => await DbSet.AsNoTracking().IgnoreAutoIncludes().Where(x => x.Id == id && x.Ativo).FirstOrDefaultAsync();

    public async Task<IEnumerable<TEntity>> ObterTodos()
        => await DbSet.AsNoTracking().Where(x => x.Ativo).IgnoreAutoIncludes().ToListAsync();
    
    public Task SaveChangesAsync() => Db.SaveChangesAsync();

    public void Dispose()
    {
        Db.Dispose();
        GC.SuppressFinalize(this);
    }
}