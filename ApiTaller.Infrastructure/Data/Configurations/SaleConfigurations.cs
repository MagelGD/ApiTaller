using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class SaleConfigurations : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("sale");

            entity.HasIndex(e => e.WorkOrderId, "FK_SALE_WORK_ORDER");
            entity.HasIndex(e => e.CustomerId, "FK_SALE_CUSTOMER");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_SALE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.WorkOrderId)
                .HasColumnType("int(11)")
                .HasColumnName("work_order_id");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");

            entity.Property(e => e.SaleDate)
                .HasColumnType("datetime")
                .HasColumnName("sale_date");

            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("subtotal");

            entity.Property(e => e.DiscountPercent)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("discount_percent");

            entity.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("discount_amount");

            entity.Property(e => e.Total)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("total");

            entity.Property(e => e.DownPayment)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("down_payment");

            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("balance");

            entity.Property(e => e.Observations)
                .HasMaxLength(1000)
                .HasColumnName("observations");

            entity.Property(e => e.WorkshopName)
                .HasMaxLength(255)
                .HasColumnName("workshop_name");

            entity.Property(e => e.WorkshopSlogan)
                .HasMaxLength(500)
                .HasColumnName("workshop_slogan");

            entity.Property(e => e.LogoBase64)
                .HasColumnType("longtext")
                .HasColumnName("logo_base64");

            entity.Property(e => e.LogoBrandsBase64)
                .HasColumnType("longtext")
                .HasColumnName("logo_brands_base64");

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

            entity.HasOne(d => d.Customer).WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_CUSTOMER");

            entity.HasOne(d => d.WorkOrder).WithMany()
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_WORK_ORDER");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_USER");
        }
    }
}
