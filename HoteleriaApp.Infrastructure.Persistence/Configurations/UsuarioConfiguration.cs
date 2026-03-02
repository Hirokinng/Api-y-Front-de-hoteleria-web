using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id_usuario");

        builder.Property(u => u.Nombre)
               .HasColumnName("nombre")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(u => u.Email)
               .HasColumnName("email")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(u => u.PasswordHash)
               .HasColumnName("password_hash")
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(u => u.Rol)
               .HasColumnName("rol")
               .HasMaxLength(20)
               .HasConversion<string>();

        builder.Property(u => u.Activo)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.Property(u => u.FechaCreacion)
               .HasColumnName("fecha_creacion");

        builder.HasIndex(u => u.Email).IsUnique();
    }
}

