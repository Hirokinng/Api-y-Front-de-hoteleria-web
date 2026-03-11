using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class PagoConfiguration : IEntityTypeConfiguration<Pago>
{
    public void Configure(EntityTypeBuilder<Pago> builder)
    {
        builder.ToTable("Pago");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id_pago");

        builder.Property(p => p.IdReserva).HasColumnName("id_reserva");

        builder.Property(p => p.Monto)
               .HasColumnName("monto")
               .HasColumnType("decimal(10,2)")
               .IsRequired();

        builder.Property(p => p.Estado)
               .HasColumnName("estado")
               .HasMaxLength(20)
               .HasDefaultValue(EstadoPago.Pendiente)
               .HasConversion<string>();

        builder.Property(p => p.ReferenciaExterna)
               .HasColumnName("referencia_externa")
               .HasMaxLength(150);

        builder.Property(p => p.FechaPago).HasColumnName("fecha_pago");

        builder.HasOne(p => p.Reserva)
               .WithMany(r => r.Pagos)
               .HasForeignKey(p => p.IdReserva)
               .OnDelete(DeleteBehavior.Restrict);
    }
}