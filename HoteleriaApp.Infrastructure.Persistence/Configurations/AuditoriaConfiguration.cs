using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> builder)
    {
        builder.ToTable("Auditoria");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id_auditoria");

        builder.Property(a => a.IdUsuario).HasColumnName("id_usuario");

        builder.Property(a => a.Tabla)
               .HasColumnName("tabla")
               .HasMaxLength(60)
               .IsRequired();

        builder.Property(a => a.Operacion)
               .HasColumnName("operacion")
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(a => a.Descripcion)
               .HasColumnName("descripcion")
               .HasMaxLength(500);

        builder.Property(a => a.Fecha).HasColumnName("fecha");
    }
}