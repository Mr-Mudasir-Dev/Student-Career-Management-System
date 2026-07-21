using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.HasKey(x => x.Id);

            entity.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(300);

            entity.Property(b => b.Description)
                .HasMaxLength(2000);

            entity.Property(b => b.CoverImage)
                .HasMaxLength(500);

            entity.Property(b => b.Price)
                .HasColumnType("decimal(18,2)"); 

            entity.Property(b => b.Language)
                .HasConversion<string>();

            entity.Property(b => b.IsBestseller)
                .HasDefaultValue(false);

            entity.Property(b => b.IsNewArrival)
                .HasDefaultValue(false);

            entity.Property(b => b.IsAvailable)
                .HasDefaultValue(true);
        }
    }
}
