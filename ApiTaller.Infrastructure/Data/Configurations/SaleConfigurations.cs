using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class SaleConfigurations : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sale");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.WorkOrderId).HasColumnName("work_order_id");
            builder.Property(e => e.CustomerId).HasColumnName("customer_id");
            builder.Property(e => e.SaleDate).HasColumnName("sale_date");
            builder.Property(e => e.Subtotal).HasColumnName("subtotal").HasPrecision(18, 2);
            builder.Property(e => e.DiscountPercent).HasColumnName("discount_percent").HasPrecision(18, 2);
            builder.Property(e => e.DiscountAmount).HasColumnName("discount_amount").HasPrecision(18, 2);
            builder.Property(e => e.Total).HasColumnName("total").HasPrecision(18, 2);
            builder.Property(e => e.DownPayment).HasColumnName("down_payment").HasPrecision(18, 2);
            builder.Property(e => e.Balance).HasColumnName("balance").HasPrecision(18, 2);
            builder.Property(e => e.Observations).HasColumnName("observations").HasMaxLength(1000);
            builder.Property(e => e.IsActive).HasColumnName("is_active");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");

            builder.HasOne(d => d.Customer)
                .WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.WorkOrder)
                .WithMany()
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
