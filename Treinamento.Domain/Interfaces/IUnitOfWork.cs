namespace Treinamento.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}