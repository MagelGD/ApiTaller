using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class ServicePriceByVersionConfigurations : IEntityTypeConfiguration<ServicePriceByVersion>
    {
        public void Configure(EntityTypeBuilder<ServicePriceByVersion> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("service_price_by_version");

            entity.HasIndex(e => e.ServiceCatalogId, "FK_SERVICE_PRICE_CATALOG");
            entity.HasIndex(e => e.BrandModelVersionId, "FK_SERVICE_PRICE_BRAND_MODEL_VERSION");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_SERVICE_PRICE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.ServiceCatalogId)
                .HasColumnType("int(11)")
                .HasColumnName("service_catalog_id");

            entity.Property(e => e.BrandModelVersionId)
                .HasColumnType("int(11)")
                .HasColumnName("brand_model_version_id");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            entity.Property(e => e.EstimatedMinutes)
                .HasColumnType("int(11)")
                .HasColumnName("estimated_minutes");

            entity.Property(e => e.TimeUnit)
                .HasColumnName("time_unit")
                .HasMaxLength(20);

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

            entity.HasOne(d => d.ServiceCatalogIdNavigation).WithMany()
                .HasForeignKey(d => d.ServiceCatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SERVICE_PRICE_CATALOG");

            entity.HasOne(d => d.BrandModelVersionIdNavigation).WithMany()
                .HasForeignKey(d => d.BrandModelVersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SERVICE_PRICE_BRAND_MODEL_VERSION");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SERVICE_PRICE_USER");
        }
    }
}
