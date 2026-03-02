using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class ReservaServicioConfiguration : IEntityTypeConfiguration<ReservaServicio>
{
    public void Configure(EntityTypeBuilder<ReservaServicio> builder)
    {
        builder.ToTable("Reserva_Servicio");

        builder.HasKey(rs => rs.Id);
        builder.Property(rs => rs.Id).HasColumnName("id_reserva_servicio");

        builder.Property(rs => rs.IdReserva).HasColumnName("id_reserva");
        builder.Property(rs => rs.IdServicio).HasColumnName("id_servicio");

        builder.Property(rs => rs.PrecioAplicado)
               .HasColumnName("precio_aplicado")
               .HasColumnType("decimal(10,2)")
               .IsRequired();

        builder.HasIndex(rs => new { rs.IdReserva, rs.IdServicio }).IsUnique();

        builder.HasOne(rs => rs.Reserva)
               .WithMany(r => r.ReservaServicios)
               .HasForeignKey(rs => rs.IdReserva)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rs => rs.Servicio)
               .WithMany(s => s.ReservaServicios)
               .HasForeignKey(rs => rs.IdServicio)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
