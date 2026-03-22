using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HoteleriaApp.Core.Domain.Entities;

public class CategoriaConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id_categoria");

        builder.Property(c => c.Name)
               .HasColumnName("nombre")
               .HasMaxLength(80)
               .IsRequired();

        builder.Property(c => c.Description)
               .HasColumnName("descripcion")
               .HasMaxLength(500);

        

        builder.Property(c => c.IsActive)
               .HasColumnName("activo")
               .HasDefaultValue(true);

        builder.HasIndex(c => c.Name).IsUnique();
    }
}