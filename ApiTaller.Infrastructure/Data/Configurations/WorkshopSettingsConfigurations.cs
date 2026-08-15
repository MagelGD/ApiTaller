using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class WorkshopSettingsConfigurations : IEntityTypeConfiguration<WorkshopSettings>
    {
        public void Configure(EntityTypeBuilder<WorkshopSettings> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("workshop_settings");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_WORKSHOP_SETTINGS_USER");
            // SAAS-1: Índice único por tenant (workshop_id, setting_key) — cada taller tiene su propio scope
            entity.HasIndex(new[] { "WorkshopId", "SettingKey" }, "UQ_WORKSHOP_SETTINGS_TENANT_KEY").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.SettingKey)
                .HasMaxLength(100)
                .HasColumnName("setting_key");

            // ⚠️ CRÍTICO: LONGTEXT para soportar logos Base64 (~100-500 KB)
            entity.Property(e => e.SettingValue)
                .HasColumnType("longtext")
                .HasColumnName("setting_value");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");

            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORKSHOP_SETTINGS_USER");

            // SAAS-1: La relación al Workshop padre se configura en WorkshopConfigurations
            entity.Property(e => e.WorkshopId)
                .HasColumnType("int(11)")
                .HasColumnName("workshop_id");
        }
    }
}
