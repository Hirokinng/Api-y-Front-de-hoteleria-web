using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categoria");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id_categoria");

        builder.Property(c => c.Nombre)
               .HasColumnName("nombre")
               .HasMaxLength(80)
               .IsRequired();

        builder.Property(c => c.Descripcion)
               .HasColumnName("descripcion")
               .HasMaxLength(500);

        builder.Property(c => c.CapacidadMax)
               .HasColumnName("capacidad_max")
               .IsRequired();

        builder.Property(c => c.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.HasIndex(c => c.Nombre).IsUnique();
    }
}
