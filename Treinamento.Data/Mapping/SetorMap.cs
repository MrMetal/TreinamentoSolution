using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Treinamento.Domain.Models;

namespace Treinamento.Data.Mapping;

public class SetorMap : IEntityTypeConfiguration<Setor>
{
    public void Configure(EntityTypeBuilder<Setor> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.ToTable("Setores");
    }
}