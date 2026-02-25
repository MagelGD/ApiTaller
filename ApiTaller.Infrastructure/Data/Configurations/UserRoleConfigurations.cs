using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class UserRoleConfigurations : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("userrole");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_ROLE_USER");

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
                .HasColumnName("update_at");
            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .IsRequired(false)
                .HasColumnName("responsible_user_id");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("role");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .IsRequired(false)
                .HasConstraintName("FK_ROLE_USER");
        }
    }
}
