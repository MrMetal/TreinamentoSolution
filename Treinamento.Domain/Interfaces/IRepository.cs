using Treinamento.Domain.Models;

namespace Treinamento.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    void Adicionar(TEntity entity);
    void Atualizar(TEntity entity);
    Task Remover(Guid id);
    Task<TEntity?> ObterPorId(Guid id);
    Task<IEnumerable<TEntity>> ObterTodos();
    Task SaveChangesAsync();
}