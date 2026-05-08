using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class SalePaymentConfigurations : IEntityTypeConfiguration<SalePayment>
    {
        public void Configure(EntityTypeBuilder<SalePayment> builder)
        {
            builder.ToTable("sale_payment");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.SaleId).HasColumnName("sale_id");
            builder.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            builder.Property(e => e.Amount).HasColumnName("amount").HasPrecision(18, 2);
            builder.Property(e => e.ReferenceCode).HasColumnName("reference_code").HasMaxLength(255);
            builder.Property(e => e.IsActive).HasColumnName("is_active");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");

            builder.HasOne(d => d.Sale)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.SaleId);

            builder.HasOne(d => d.PaymentMethod)
                .WithMany()
                .HasForeignKey(d => d.PaymentMethodId);

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
