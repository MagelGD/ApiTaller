using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    internal class UserRoleModuleConfigurations : IEntityTypeConfiguration<UserRoleModule>
    {
        public void Configure(EntityTypeBuilder<UserRoleModule> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("user_role_module");

            entity.HasIndex(e => e.ModulesRoleId, "FK_ROLE_MODULE_MODULE");

            entity.HasIndex(e => e.UserRoleId, "FK_ROLE_MODULE_USER_ROLE");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_USER_ROLE_MODULE_USER");

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
            entity.Property(e => e.ModulesRoleId)
                .HasColumnType("int(11)")
                .HasColumnName("module_role_id");
            entity.Property(e => e.UserRoleId)
                .HasColumnType("int(11)")
                .HasColumnName("user_role_id");
            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.ModuleIdNavigation).WithMany()
                .HasForeignKey(d => d.ModulesRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLE_MODULE_MODULE");

            entity.HasOne(d => d.UserRoleIdNavigation).WithMany()
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLE_MODULE_USER_ROLE");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_ROLE_MODULE_USER");
        }
    }
}
