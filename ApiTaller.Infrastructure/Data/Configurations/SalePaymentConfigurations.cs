using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class SalePaymentConfigurations : IEntityTypeConfiguration<SalePayment>
    {
        public void Configure(EntityTypeBuilder<SalePayment> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("sale_payment");

            entity.HasIndex(e => e.SaleId, "FK_SALE_PAYMENT_SALE");
            entity.HasIndex(e => e.PaymentMethodId, "FK_SALE_PAYMENT_METHOD");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_SALE_PAYMENT_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.SaleId)
                .HasColumnType("int(11)")
                .HasColumnName("sale_id");

            entity.Property(e => e.PaymentMethodId)
                .HasColumnType("int(11)")
                .HasColumnName("payment_method_id");

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("amount");

            entity.Property(e => e.ReferenceCode)
                .HasMaxLength(255)
                .HasColumnName("reference_code");

            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");

            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");

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

            entity.HasOne(d => d.Sale).WithMany(p => p.Payments)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SALE_PAYMENT_SALE");

            entity.HasOne(d => d.PaymentMethod).WithMany()
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_PAYMENT_METHOD");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_PAYMENT_USER");
        }
    }
}
