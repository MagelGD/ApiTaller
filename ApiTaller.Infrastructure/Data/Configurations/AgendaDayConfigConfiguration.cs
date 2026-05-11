using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class AgendaDayConfigConfiguration : IEntityTypeConfiguration<AgendaDayConfig>
    {
        public void Configure(EntityTypeBuilder<AgendaDayConfig> builder)
        {
            builder.ToTable("agenda_day_config");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Date)
                .IsRequired()
                .HasColumnName("date")
                .HasColumnType("date");

            builder.Property(e => e.CustomSlots)
                .HasColumnName("custom_slots");

            builder.Property(e => e.IsBlocked)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_blocked");

            builder.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(e => e.ResponsibleUserId)
                .HasColumnName("responsible_user_id");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId);
        }
    }
}
