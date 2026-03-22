using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HoteleriaApp.Core.Domain.Entities;
using System;

public class HistorialEstadoReservaConfiguration : IEntityTypeConfiguration<HistorialEstadoReserva>
{
    public void Configure(EntityTypeBuilder<HistorialEstadoReserva> builder)
    {
        builder.ToTable("Historial_Estado_Reserva");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id_historial");

        builder.Property(h => h.IdReserva).HasColumnName("id_reserva");
        builder.Property(h => h.IdUsuario).HasColumnName("id_usuario");

        builder.Property(h => h.EstadoAnterior)
               .HasColumnName("estado_anterior")
               .HasMaxLength(20)
               .HasConversion<string?>();

        builder.Property(h => h.EstadoNuevo)
               .HasColumnName("estado_nuevo")
               .HasMaxLength(20)
               .HasConversion<string>();

        builder.Property(h => h.FechaCambio).HasColumnName("fecha_cambio");

        builder.Property(h => h.Observacion)
               .HasColumnName("observacion")
               .HasMaxLength(300);

        builder.HasOne(h => h.Reserva)
               .WithMany(r => r.Historial)
               .HasForeignKey(h => h.IdReserva)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.Usuario)
               .WithMany(u => u.Historiales)
               .HasForeignKey(h => h.IdUsuario)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
