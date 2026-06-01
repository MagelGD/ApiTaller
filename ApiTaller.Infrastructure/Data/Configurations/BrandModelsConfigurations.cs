using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class BrandModelsConfigurations : IEntityTypeConfiguration<Domain.Models.BrandModels>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.BrandModels> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("BrandModels");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_BRANDMODELS_USER");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Models)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("models");
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
            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BRANDMODELS_USER");

        }
    }
}
