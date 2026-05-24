using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class WorkOrderServiceConfigurations : IEntityTypeConfiguration<Domain.Models.WorkOrderService>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.WorkOrderService> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("work_order_service");

            entity.HasIndex(e => e.WorkOrderId, "FK_SERVICE_WORK_ORDER");
            entity.HasIndex(e => e.MechanicId, "FK_SERVICE_MECHANIC");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_SERVICE_RESPONSIBLE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.WorkOrderId)
                .HasColumnType("int(11)")
                .HasColumnName("work_order_id");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");

            entity.Property(e => e.MechanicId)
                .HasColumnType("int(11)")
                .HasColumnName("mechanic_id");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            entity.Property(e => e.WarrantyEndDate)
                .HasColumnType("datetime")
                .HasColumnName("warranty_end_date");

            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active");

            entity.Property(e => e.IsApproved)
                .HasColumnType("bit(1)")
                .HasColumnName("is_approved");

            entity.Property(e => e.IsPaidToMechanic)
                .HasColumnType("bit(1)")
                .HasColumnName("is_paid_to_mechanic");

            entity.Property(e => e.PaidToMechanicAt)
                .HasColumnType("datetime")
                .HasColumnName("paid_to_mechanic_at");

            entity.Property(e => e.MechanicPaymentSettlementId)
                .HasColumnType("int(11)")
                .HasColumnName("mechanic_payment_settlement_id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.WorkOrderNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SERVICE_WORK_ORDER");

            entity.HasOne(d => d.MechanicNavigation).WithMany()
                .HasForeignKey(d => d.MechanicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SERVICE_MECHANIC");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SERVICE_RESPONSIBLE_USER");

            entity.HasOne(d => d.MechanicPaymentSettlementNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.MechanicPaymentSettlementId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SERVICE_SETTLEMENT");
        }
    }
}
