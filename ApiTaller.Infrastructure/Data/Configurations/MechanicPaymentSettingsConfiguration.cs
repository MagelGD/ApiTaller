using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class MechanicPaymentSettingsConfiguration : IEntityTypeConfiguration<Domain.Models.MechanicPaymentSettings>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.MechanicPaymentSettings> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("mechanic_payment_settings");

            entity.HasIndex(e => e.MechanicId, "FK_PAYMENT_SETTINGS_MECHANIC");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.MechanicId)
                .HasColumnType("int(11)")
                .HasColumnName("mechanic_id");

            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .HasColumnName("payment_type");

            entity.Property(e => e.Value)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("value");

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

            entity.HasOne(d => d.MechanicNavigation).WithMany()
                .HasForeignKey(d => d.MechanicId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PAYMENT_SETTINGS_MECHANIC");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAYMENT_SETTINGS_RESPONSIBLE_USER");
        }
    }
}
