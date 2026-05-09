using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class WorkOrderHistoryConfiguration : IEntityTypeConfiguration<WorkOrderHistory>
    {
        public void Configure(EntityTypeBuilder<WorkOrderHistory> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("work_order_history");

            entity.HasIndex(e => e.WorkOrderId, "FK_HISTORY_WORK_ORDER");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_HISTORY_RESPONSIBLE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.WorkOrderId)
                .HasColumnType("int(11)")
                .HasColumnName("work_order_id");

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.Property(e => e.Observations)
                .HasMaxLength(500)
                .HasColumnName("observations");

            entity.Property(e => e.ActionBy)
                .HasMaxLength(100)
                .HasColumnName("action_by");

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

            entity.HasOne(d => d.WorkOrderNavigation).WithMany()
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_HISTORY_WORK_ORDER");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HISTORY_RESPONSIBLE_USER");
        }
    }
}
