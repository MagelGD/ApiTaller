using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class BrandModelVersionConfigurations : IEntityTypeConfiguration<Domain.Models.BrandModelVersion>
    {

        public void Configure(EntityTypeBuilder<Domain.Models.BrandModelVersion> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("BrandModelVersion");
            entity.HasIndex(e => e.BrandId, "FK_BRANDMODELVERSION_BRAND");
            entity.HasIndex(e => e.ModelId, "FK_BRANDMODELVERSION_MODELS");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_BRANDMODELVERSION_USER");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("version");
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
                .HasConstraintName("FK_BRANDMODELVERSION_USER");
            entity.HasOne(d => d.Brand).WithMany()
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BRAND_BRANDMODELVERSION");
            entity.HasOne(d => d.Model).WithMany()
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MODELS_BRANDMODELVERSION");
        }
    }
}