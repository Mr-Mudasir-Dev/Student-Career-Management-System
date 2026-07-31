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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)");

            entity.Property(x => x.Quantity)
                .IsRequired();

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
