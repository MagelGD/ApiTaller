using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class SaleDetailConfigurations : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("sale_detail");

            entity.HasIndex(e => e.SaleId, "FK_SALE_DETAIL_SALE");
            entity.HasIndex(e => e.ProductId, "FK_SALE_DETAIL_PRODUCT");
            entity.HasIndex(e => e.ServiceCatalogId, "FK_SALE_DETAIL_SERVICE");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_SALE_DETAIL_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.SaleId)
                .HasColumnType("int(11)")
                .HasColumnName("sale_id");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");

            entity.Property(e => e.ServiceCatalogId)
                .HasColumnType("int(11)")
                .HasColumnName("service_catalog_id");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");

            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("unit_price");

            entity.Property(e => e.Total)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("total");

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

            entity.HasOne(d => d.Sale).WithMany(p => p.Details)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SALE_DETAIL_SALE");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_DETAIL_PRODUCT");

            entity.HasOne(d => d.Service).WithMany()
                .HasForeignKey(d => d.ServiceCatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_DETAIL_SERVICE");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_DETAIL_USER");
        }
    }
}
