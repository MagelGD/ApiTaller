using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    internal class IdentificacionTypeConfigurations : IEntityTypeConfiguration<IdentificationType>
    {
        public void Configure(EntityTypeBuilder<IdentificationType> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("identification_type");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_TYPE_IDENTIFICATION_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
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
                .IsRequired(false)
                .HasColumnName("responsabilidad_user_id");
            entity.Property(e => e.Identification)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("identification");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .IsRequired(false)
                .HasConstraintName("FK_TYPE_IDENTIFICATION_USER");

            // SAAS-2: Aislamiento por taller
            entity.Property(e => e.WorkshopId)
                .HasColumnType("int(11)")
                .IsRequired(false)
                .HasColumnName("workshop_id");

            entity.HasOne(d => d.WorkshopNavigation).WithMany()
                .HasForeignKey(d => d.WorkshopId)
                .IsRequired(false)
                .HasConstraintName("FK_IDENTIFICATION_TYPE_WORKSHOP");
        }
    }
}
