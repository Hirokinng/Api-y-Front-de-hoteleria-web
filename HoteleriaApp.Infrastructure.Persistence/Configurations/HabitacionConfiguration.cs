using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class HabitacionConfiguration : IEntityTypeConfiguration<Habitacion>
{
    public void Configure(EntityTypeBuilder<Habitacion> builder)
    {
        builder.ToTable("Habitacion");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id_habitacion");

        builder.Property(h => h.NumeroHabitacion)
               .HasColumnName("numero_habitacion")
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(h => h.IdPiso).HasColumnName("id_piso");
        builder.Property(h => h.IdCategoria).HasColumnName("id_categoria");

        builder.Property(h => h.Estado)
               .HasColumnName("estado")
               .HasMaxLength(20)
               .HasDefaultValue(EstadoHabitacion.Disponible)
               .HasConversion<string>();

        builder.Property(h => h.DescripcionAdicional)
               .HasColumnName("descripcion_adicional")
               .HasMaxLength(300);

        builder.Property(h => h.FechaUltimaActualizacion)
               .HasColumnName("fecha_ultima_actualizacion");

        builder.HasIndex(h => h.NumeroHabitacion).IsUnique();

        builder.HasOne(h => h.Piso)
               .WithMany(p => p.Habitaciones)
               .HasForeignKey(h => h.IdPiso)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.Categoria)
               .WithMany(c => c.Habitaciones)
               .HasForeignKey(h => h.IdCategoria)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
