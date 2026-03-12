using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class TarifaConfiguration : IEntityTypeConfiguration<Tarifa>
{
    public void Configure(EntityTypeBuilder<Tarifa> builder)
    {
        builder.ToTable("Tarifa");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id_tarifa");

        builder.Property(t => t.IdCategoria).HasColumnName("id_categoria");
        builder.Property(t => t.IdTemporada).HasColumnName("id_temporada");

        builder.Property(t => t.PrecioNoche)
               .HasColumnName("precio_noche")
               .HasColumnType("decimal(10,2)")
               .IsRequired();

        builder.Property(t => t.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.HasOne(t => t.Categoria)
               .WithMany(c => c.Tarifas)
               .HasForeignKey(t => t.IdCategoria)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Temporada)
               .WithMany(te => te.Tarifas)
               .HasForeignKey(t => t.IdTemporada)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}