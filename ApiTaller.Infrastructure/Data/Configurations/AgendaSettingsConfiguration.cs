using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class AgendaSettingsConfiguration : IEntityTypeConfiguration<AgendaSettings>
    {
        public void Configure(EntityTypeBuilder<AgendaSettings> builder)
        {
            builder.ToTable("agenda_settings");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.WeeksToOpen)
                .IsRequired()
                .HasColumnName("weeks_to_open");

            builder.Property(e => e.DailySlots)
                .IsRequired()
                .HasColumnName("daily_slots");

            builder.Property(e => e.BusinessHoursStart)
                .IsRequired()
                .HasColumnName("business_hours_start");

            builder.Property(e => e.BusinessHoursEnd)
                .IsRequired()
                .HasColumnName("business_hours_end");

            builder.Property(e => e.StartDate)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("start_date");

            builder.Property(e => e.WorkingDays)
                .HasMaxLength(50)
                .HasColumnName("working_days");

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

            // SAAS-2: Aislamiento por taller
            builder.Property(e => e.WorkshopId)
                .IsRequired()
                .HasColumnName("workshop_id");

            builder.HasOne(d => d.WorkshopNavigation).WithMany()
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_AGENDA_SETTINGS_WORKSHOP");
        }
    }
}
