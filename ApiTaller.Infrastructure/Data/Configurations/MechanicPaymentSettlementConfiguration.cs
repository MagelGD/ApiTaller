using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class MechanicPaymentSettlementConfiguration : IEntityTypeConfiguration<Domain.Models.MechanicPaymentSettlement>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.MechanicPaymentSettlement> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("mechanic_payment_settlement");

            entity.HasIndex(e => e.MechanicId, "FK_SETTLEMENT_MECHANIC");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.MechanicId)
                .HasColumnType("int(11)")
                .HasColumnName("mechanic_id");

            entity.Property(e => e.SettlementDate)
                .HasColumnType("datetime")
                .HasColumnName("settlement_date");

            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("total_amount");

            entity.Property(e => e.ServicesCount)
                .HasColumnType("int(11)")
                .HasColumnName("services_count");

            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");

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
                .HasConstraintName("FK_SETTLEMENT_MECHANIC");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SETTLEMENT_RESPONSIBLE_USER");
        }
    }
}
