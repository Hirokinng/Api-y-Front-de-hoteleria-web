using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HoteleriaApp.Core.Domain.Entities;
using System;

public class PisoConfiguration : IEntityTypeConfiguration<Piso>
{
    public void Configure(EntityTypeBuilder<Piso> builder)
    {
        builder.ToTable("Piso");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id_piso");

        builder.Property(p => p.NumeroPiso)
               .HasColumnName("numero_piso")
               .IsRequired();

        builder.Property(p => p.Descripcion)
               .HasColumnName("descripcion")
               .HasMaxLength(100);

        builder.Property(p => p.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.HasIndex(p => p.NumeroPiso).IsUnique();
    }
}