using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class VehicleConfigurations : IEntityTypeConfiguration<Domain.Models.Vehicle>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Vehicle> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("vehicle");
            
            entity.HasIndex(e => e.CustomerId, "FK_VEHICLE_CUSTOMER");
            entity.HasIndex(e => e.BrandId, "FK_VEHICLE_BRAND");
            entity.HasIndex(e => e.ModelId, "FK_VEHICLE_MODEL");
            entity.HasIndex(e => e.VersionId, "FK_VEHICLE_VERSION");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_VEHICLE_RESPONSIBLE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.Plate)
                .HasMaxLength(20)
                .HasColumnName("plate");
            entity.Property(e => e.BrandId)
                .HasColumnType("int(11)")
                .HasColumnName("brand_id");
            entity.Property(e => e.ModelId)
                .HasColumnType("int(11)")
                .HasColumnName("model_id");
            entity.Property(e => e.VersionId)
                .HasColumnType("int(11)")
                .HasColumnName("version_id");
            entity.Property(e => e.Color)
                .HasMaxLength(100)
                .HasColumnName("color");
            entity.Property(e => e.CylinderCapacity)
                .HasMaxLength(50)
                .HasColumnName("cylinder_capacity");
            entity.Property(e => e.VehicleType)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("vehicle_type")
                .HasDefaultValue("moto");
            // SAAS-1 + CAR sub-type: sedan | suv | bus | truck (null para motos)
            entity.Property(e => e.VehicleSubType)
                .HasMaxLength(20)
                .HasColumnName("vehicle_sub_type");
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

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICLE_CUSTOMER");

            entity.HasOne(d => d.BrandNavigation).WithMany()
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICLE_BRAND");

            entity.HasOne(d => d.ModelNavigation).WithMany()
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICLE_MODEL");

            entity.HasOne(d => d.VersionNavigation).WithMany()
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICLE_VERSION");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICLE_RESPONSIBLE_USER");
        }
    }
}
