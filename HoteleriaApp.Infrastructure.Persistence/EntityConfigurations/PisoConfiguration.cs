using HoteleriaApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HoteleriaApp.Infrastructure.Persistence.Configurations 
{
    public class PisoConfiguration : IEntityTypeConfiguration<Piso>
    {
        public void Configure(EntityTypeBuilder<Piso> builder)
        {

            builder.ToTable("Piso");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                   .IsRequired()
                   .HasMaxLength(100); 

            builder.Property(p => p.Descripcion)
                   .HasMaxLength(500)
                   .IsRequired(false); 

            builder.Property(p => p.NumeroPiso)
                   .IsRequired();

        }
    }
}