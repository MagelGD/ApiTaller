using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class SaleDetailConfigurations : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.ToTable("sale_detail");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.SaleId).HasColumnName("sale_id");
            builder.Property(e => e.ProductId).HasColumnName("product_id");
            builder.Property(e => e.ServiceCatalogId).HasColumnName("service_catalog_id");
            builder.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
            builder.Property(e => e.Quantity).HasColumnName("quantity");
            builder.Property(e => e.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 2);
            builder.Property(e => e.Total).HasColumnName("total").HasPrecision(18, 2);
            builder.Property(e => e.IsActive).HasColumnName("is_active");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");

            builder.HasOne(d => d.Sale)
                .WithMany(p => p.Details)
                .HasForeignKey(d => d.SaleId);

            builder.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);

            builder.HasOne(d => d.Service)
                .WithMany()
                .HasForeignKey(d => d.ServiceCatalogId);

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
