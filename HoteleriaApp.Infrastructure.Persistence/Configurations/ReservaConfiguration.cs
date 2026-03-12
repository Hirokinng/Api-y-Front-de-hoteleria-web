using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reserva");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id_reserva");

        builder.Property(r => r.NumeroReserva)
               .HasColumnName("numero_reserva")
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(r => r.IdCliente).HasColumnName("id_cliente");
        builder.Property(r => r.IdUsuario).HasColumnName("id_usuario");
        builder.Property(r => r.IdCategoria).HasColumnName("id_categoria");
        builder.Property(r => r.FechaEntrada).HasColumnName("fecha_entrada").IsRequired();
        builder.Property(r => r.FechaSalida).HasColumnName("fecha_salida").IsRequired();
        builder.Property(r => r.NumeroHuespedes).HasColumnName("numero_huespedes").IsRequired();

        builder.Property(r => r.Estado)
               .HasColumnName("estado")
               .HasMaxLength(20)
               .HasDefaultValue(EstadoReserva.Confirmada)
               .HasConversion<string>();

        builder.Property(r => r.PrecioBaseNoche)
               .HasColumnName("precio_base_noche")
               .HasColumnType("decimal(10,2)");

        builder.Property(r => r.TotalNoches)
               .HasColumnName("total_noches");

        builder.Property(r => r.SubtotalHabitacion)
               .HasColumnName("subtotal_habitacion")
               .HasColumnType("decimal(10,2)");

        builder.Property(r => r.TotalServicios)
               .HasColumnName("total_servicios")
               .HasColumnType("decimal(10,2)")
               .HasDefaultValue(0m);

        builder.Property(r => r.Total)
               .HasColumnName("total")
               .HasColumnType("decimal(10,2)");

        builder.Property(r => r.FechaCreacion).HasColumnName("fecha_creacion");
        builder.Property(r => r.FechaCancelacion).HasColumnName("fecha_cancelacion");

        builder.Property(r => r.ReferenciaExterna)
               .HasColumnName("referencia_externa")
               .HasMaxLength(100);

        builder.HasIndex(r => r.NumeroReserva).IsUnique();

        builder.HasOne(r => r.Cliente)
               .WithMany(c => c.Reservas)
               .HasForeignKey(r => r.IdCliente)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Usuario)
               .WithMany(u => u.Reservas)
               .HasForeignKey(r => r.IdUsuario)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Categoria)
               .WithMany(c => c.Reservas)
               .HasForeignKey(r => r.IdCategoria)
               .OnDelete(DeleteBehavior.Restrict);
    }
}