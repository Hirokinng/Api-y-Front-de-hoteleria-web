using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class TemporadaConfiguration : IEntityTypeConfiguration<Temporada>
{
    public void Configure(EntityTypeBuilder<Temporada> builder)
    {
        builder.ToTable("Temporada");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id_temporada");

        builder.Property(t => t.Nombre)
               .HasColumnName("nombre")
               .HasMaxLength(60)
               .IsRequired();

        builder.Property(t => t.FechaInicio)
               .HasColumnName("fecha_inicio")
               .IsRequired();

        builder.Property(t => t.FechaFin)
               .HasColumnName("fecha_fin")
               .IsRequired();

        builder.Property(t => t.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);
    }
}
