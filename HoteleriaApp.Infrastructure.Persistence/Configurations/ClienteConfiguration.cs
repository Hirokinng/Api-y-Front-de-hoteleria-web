using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HoteleriaApp.Core.Domain.Entities;
using System;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Cliente");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id_cliente");

        builder.Property(c => c.Nombre)
               .HasColumnName("nombre")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(c => c.Email)
               .HasColumnName("email")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(c => c.Telefono)
               .HasColumnName("telefono")
               .HasMaxLength(20);

        builder.Property(c => c.PasswordHash)
               .HasColumnName("password_hash")
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(c => c.FechaRegistro)
               .HasColumnName("fecha_registro");

        builder.Property(c => c.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.HasIndex(c => c.Email).IsUnique();
    }
}
