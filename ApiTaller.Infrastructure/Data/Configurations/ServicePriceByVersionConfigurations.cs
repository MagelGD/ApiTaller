using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class ServicePriceByVersionConfigurations : IEntityTypeConfiguration<ServicePriceByVersion>
    {
        public void Configure(EntityTypeBuilder<ServicePriceByVersion> builder)
        {
            builder.ToTable("service_price_by_version");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ServiceCatalogId).HasColumnName("service_catalog_id");
            builder.Property(e => e.BrandModelVersionId).HasColumnName("brand_model_version_id");
            builder.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
            builder.Property(e => e.EstimatedMinutes).HasColumnName("estimated_minutes");
            builder.Property(e => e.IsActive).HasColumnName("is_active");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");

            builder.HasOne(d => d.ServiceCatalogIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ServiceCatalogId)
                .HasConstraintName("FK_SERVICE_PRICE_CATALOG");

            builder.HasOne(d => d.BrandModelVersionIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.BrandModelVersionId)
                .HasConstraintName("FK_SERVICE_PRICE_BRAND_MODEL_VERSION");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .HasConstraintName("FK_SERVICE_PRICE_USER");
        }
    }
}
