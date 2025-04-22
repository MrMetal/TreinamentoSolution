using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Mapping;

public class EmpresaMap : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(p => p.RazaoSocial)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Contato)
            .HasMaxLength(14)
            .HasColumnType("varchar(14)");

        builder.Property(p => p.Cnpj)
            .HasMaxLength(14)
            .HasColumnType("varchar(14)")
            .IsRequired();

        // 1 : 1 => Empresa : Endereco
        builder.HasOne(e => e.Endereco)
            .WithOne(c => c.Empresa)
            .HasForeignKey<Endereco>(e => e.EmpresaId) // 👈 Essencial
            .OnDelete(DeleteBehavior.Cascade);

        // 1 : N => Empresa : Setores
        builder.HasMany(p => p.Setor)
            .WithOne(m => m.Empresa)
            .HasForeignKey(p => p.EmpresaId);

        builder.ToTable("Empresas");
    }
}