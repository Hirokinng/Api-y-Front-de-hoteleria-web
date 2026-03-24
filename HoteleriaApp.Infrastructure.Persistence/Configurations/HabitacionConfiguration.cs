using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HoteleriaApp.Core.Domain.Entities;

public class HabitacionConfiguration : IEntityTypeConfiguration<Habitacion>
{
    public void Configure(EntityTypeBuilder<Habitacion> builder)
    {
        builder.ToTable("Habitacion");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id_habitacion");

        builder.Property(h => h.Numero)
               .HasColumnName("numero")
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(h => h.Piso)
               .HasColumnName("piso")
               .IsRequired();

        builder.Property(h => h.Capacidad)
               .HasColumnName("capacidad")
               .IsRequired();

        builder.Property(h => h.Estado)
               .HasColumnName("estado")
               .HasConversion<string>();

        builder.Property(h => h.TipoHabitacionId)
               .HasColumnName("tipo_habitacion_id");

        builder.HasOne(h => h.TipoHabitacion)
               .WithMany(t => t.Habitaciones)
               .HasForeignKey(h => h.TipoHabitacionId);

        builder.HasOne(h => h.Categoria)
       .WithMany(c => c.Habitaciones)
       .HasForeignKey(h => h.IdCategoria)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(h => h.Numero).IsUnique();
    }
}