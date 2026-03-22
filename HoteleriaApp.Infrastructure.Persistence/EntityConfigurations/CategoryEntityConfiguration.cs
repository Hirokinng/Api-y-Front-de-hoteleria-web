using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Infrastructure.Persistence.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.PricePerNight)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Characteristics)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .IsRequired();
        }
    }
}
