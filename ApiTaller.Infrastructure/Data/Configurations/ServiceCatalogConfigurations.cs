using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class ServiceCatalogConfigurations : IEntityTypeConfiguration<ServiceCatalog>
    {
        public void Configure(EntityTypeBuilder<ServiceCatalog> builder)
        {
            builder.ToTable("service_catalog");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ServiceTypeId).HasColumnName("service_type_id");
            builder.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(150);
            builder.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
            builder.Property(e => e.IsActive).HasColumnName("is_active");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");

            builder.HasOne(d => d.ServiceTypeIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ServiceTypeId)
                .HasConstraintName("FK_SERVICE_CATALOG_SERVICE_TYPE");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .HasConstraintName("FK_SERVICE_CATALOG_USER");
        }
    }
}
