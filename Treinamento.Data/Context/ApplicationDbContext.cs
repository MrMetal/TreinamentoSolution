using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Treinamento.Data.Identity;
using Treinamento.Data.Mapping;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Context;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    #region Entities

    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Setor> Setores { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region Mapping Fluent Api

        builder.ApplyConfiguration(new EmpresaMap());
        builder.ApplyConfiguration(new EnderecoMap());
        builder.ApplyConfiguration(new SetorMap());


        #endregion

        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added) entry.Property("DataCadastro").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified) entry.Property("DataCadastro").IsModified = false;
        }

        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
        {
            if (entry.State == EntityState.Added) entry.Property("DataAlteracao").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified) entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}