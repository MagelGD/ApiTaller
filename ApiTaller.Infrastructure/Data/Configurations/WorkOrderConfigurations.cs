using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class WorkOrderConfigurations : IEntityTypeConfiguration<Domain.Models.WorkOrder>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.WorkOrder> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("work_order");

            entity.HasIndex(e => e.VehicleId, "FK_WORK_ORDER_VEHICLE");
            entity.HasIndex(e => e.CustomerId, "FK_WORK_ORDER_CUSTOMER");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_WORK_ORDER_RESPONSIBLE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.VehicleId)
                .HasColumnType("int(11)")
                .HasColumnName("vehicle_id");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");

            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");

            entity.Property(e => e.EstimatedDeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("estimated_delivery_date");

            entity.Property(e => e.Mileage)
                .HasColumnType("int(11)")
                .HasColumnName("mileage");

            entity.Property(e => e.FuelLevel)
                .HasMaxLength(50)
                .HasColumnName("fuel_level");

            entity.Property(e => e.Observations)
                .HasColumnType("text")
                .HasColumnName("observations");

            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");

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

            entity.HasOne(d => d.VehicleNavigation).WithMany()
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_VEHICLE");

            entity.HasOne(d => d.CustomerNavigation).WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_CUSTOMER");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WORK_ORDER_RESPONSIBLE_USER");
        }
    }
}
