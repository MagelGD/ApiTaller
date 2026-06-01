using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class BrandConfigurations : IEntityTypeConfiguration<Domain.Models.Brand>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Brand> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Brand");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_BRAND_USER");
            entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.VehicleType)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("vehicle_type")
                .HasDefaultValue("moto");
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
            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BRAND_USER");
        }
    }
}
