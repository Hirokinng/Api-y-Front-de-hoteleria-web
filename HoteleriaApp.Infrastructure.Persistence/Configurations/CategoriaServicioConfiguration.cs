using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HoteleriaApp.Core.Domain.Entities;
using System;

public class CategoriaServicioConfiguration : IEntityTypeConfiguration<CategoriaServicio>
{
    public void Configure(EntityTypeBuilder<CategoriaServicio> builder)
    {
        builder.ToTable("Categoria_Servicio");

        builder.HasKey(cs => new { cs.IdCategoria, cs.IdServicio });

        builder.Property(cs => cs.IdCategoria).HasColumnName("id_categoria");
        builder.Property(cs => cs.IdServicio).HasColumnName("id_servicio");

        builder.Property(cs => cs.Precio)
               .HasColumnName("precio")
               .HasColumnType("decimal(10,2)")
               .IsRequired();

        builder.Property(cs => cs.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.HasOne(cs => cs.Categoria)
               .WithMany(c => c.CategoriaServicios)
               .HasForeignKey(cs => cs.IdCategoria)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cs => cs.Servicio)
               .WithMany(s => s.CategoriaServicios)
               .HasForeignKey(cs => cs.IdServicio)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

