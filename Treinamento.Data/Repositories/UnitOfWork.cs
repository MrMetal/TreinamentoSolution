using Treinamento.Data.Context;
using Treinamento.Domain.Interfaces;

namespace Treinamento.Data.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork, IAsyncDisposable
{
    public async Task SaveChangesAsync()
    {
        try
        {
            await context.Database.BeginTransactionAsync();
            await context.SaveChangesAsync();
            await context.Database.CommitTransactionAsync();

            await DisposeAsync();
        }
        catch (Exception e)
        {
            await context.Database.RollbackTransactionAsync();
            Console.WriteLine(e);
        }
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
    
}