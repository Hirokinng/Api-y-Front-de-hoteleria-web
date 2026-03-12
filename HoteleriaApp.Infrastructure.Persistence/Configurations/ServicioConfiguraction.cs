using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        builder.ToTable("Servicio");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id_servicio");

        builder.Property(s => s.Nombre)
               .HasColumnName("nombre")
               .HasMaxLength(80)
               .IsRequired();

        builder.Property(s => s.Descripcion)
               .HasColumnName("descripcion")
               .HasMaxLength(300);

        builder.Property(s => s.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.HasIndex(s => s.Nombre).IsUnique();
    }
}
