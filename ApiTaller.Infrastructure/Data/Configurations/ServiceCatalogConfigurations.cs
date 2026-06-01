using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class ServiceCatalogConfigurations : IEntityTypeConfiguration<ServiceCatalog>
    {
        public void Configure(EntityTypeBuilder<ServiceCatalog> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("service_catalog");

            entity.HasIndex(e => e.ServiceTypeId, "FK_SERVICE_CATALOG_SERVICE_TYPE");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_SERVICE_CATALOG_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.ServiceTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("service_type_id");

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.DefaultPrice)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("default_price");

            entity.Property(e => e.DefaultMinutes)
                .HasColumnType("int(11)")
                .HasColumnName("default_minutes");

            entity.Property(e => e.TimeUnit)
                .HasMaxLength(20)
                .HasColumnName("time_unit");

            entity.Property(e => e.VehicleType)
                .HasMaxLength(10)
                .HasColumnName("vehicle_type")
                .HasDefaultValue("both");

            // SAAS-1: ID del taller
            entity.Property(e => e.WorkshopId)
                .HasColumnType("int(11)")
                .HasColumnName("workshop_id");

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

            entity.HasOne(d => d.ServiceTypeIdNavigation).WithMany()
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SERVICE_CATALOG_SERVICE_TYPE");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SERVICE_CATALOG_USER");
        }
    }
}
