using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HoteleriaApp.Core.Domain.Entities;
using System;

public class DetalleReservaConfiguration : IEntityTypeConfiguration<DetalleReserva>
{
    public void Configure(EntityTypeBuilder<DetalleReserva> builder)
    {
        builder.ToTable("Detalle_Reserva");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id_detalle");

        builder.Property(d => d.IdReserva).HasColumnName("id_reserva");
        builder.Property(d => d.IdHabitacion).HasColumnName("id_habitacion");

        builder.HasOne(d => d.Reserva)
               .WithMany(r => r.DetallesReserva)
               .HasForeignKey(d => d.IdReserva)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Habitacion)
               .WithMany(h => h.DetallesReserva)
               .HasForeignKey(d => d.IdHabitacion)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
