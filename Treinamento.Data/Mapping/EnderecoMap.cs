using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Mapping;

public class EnderecoMap : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(p => p.EmpresaId);

        builder.Property(c => c.Logradouro)
            .IsRequired()
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);

        builder.Property(c => c.Numero)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(c => c.Cep)
            .IsRequired()
            .HasColumnType("varchar(8)")
            .HasMaxLength(8);

        builder.Property(c => c.Complemento)
            .HasColumnType("varchar(250)")
            .HasMaxLength(250);

        builder.Property(c => c.Bairro)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(c => c.Cidade)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(c => c.Estado)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.ToTable("Enderecos");
    }
}