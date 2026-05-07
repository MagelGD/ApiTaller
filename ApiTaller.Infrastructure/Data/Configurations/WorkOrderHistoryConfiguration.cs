using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class WorkOrderHistoryConfiguration : IEntityTypeConfiguration<WorkOrderHistory>
    {
        public void Configure(EntityTypeBuilder<WorkOrderHistory> builder)
        {
            builder.ToTable("work_order_history");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.WorkOrderId).HasColumnName("work_order_id");
            builder.Property(e => e.Status).HasColumnName("status").HasMaxLength(50);
            builder.Property(e => e.Observations).HasColumnName("observations").HasMaxLength(500);
            builder.Property(e => e.ActionBy).HasColumnName("action_by").HasMaxLength(100);
            
            builder.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");

            builder.HasOne(d => d.WorkOrderNavigation)
                .WithMany()
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_work_order_history_order");
        }
    }
}
