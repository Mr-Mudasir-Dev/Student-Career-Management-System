using Domain.Entities;
using Domain.Enums.Feedback;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> entity)
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.Message)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(f => f.Rating)
                .IsRequired(false);

            entity.Property(f => f.Category)
                .HasConversion<string>();

            entity.Property(f => f.Status)
                .HasConversion<string>()
                .HasDefaultValue(FeedbackStatus.Pending);

            entity.Property(f => f.AdminReply)
                .HasMaxLength(500);

            entity.Property(f => f.UserId)
                .IsRequired();

            entity.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
