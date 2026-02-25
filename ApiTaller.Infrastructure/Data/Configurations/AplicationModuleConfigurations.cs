using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    internal class AplicationModuleConfigurations : IEntityTypeConfiguration<AplicationModule>
    {
        public void Configure(EntityTypeBuilder<AplicationModule> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("aplication_module");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_APLICATION_MODULE_USER");

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
                .HasColumnName("responsible_user_id");
            entity.Property(e => e.name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APLICATION_MODULE_USER");
        }
    }
}
