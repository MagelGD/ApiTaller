using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class RoleActionConfigurations : IEntityTypeConfiguration<Domain.Models.RoleAction>
    {
        public void Configure(EntityTypeBuilder<RoleAction> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("roleaction");

            entity.HasIndex(e => e.RoleId, "FK_ROLEACTION_USERROLE");

            entity.HasIndex(e => e.ActionId, "FK_ROLEACTION_ACTION");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_ROLEACTION_USER");
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
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.ActionId)
                .HasColumnType("int(11)")
                .HasColumnName("action_id");
            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.RoleIdNavigation).WithMany()
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLEACTION_USERROLE");

            entity.HasOne(d => d.ActionIdNavigation).WithMany()
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLEACTION_ACTION");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLEACTION_USER");
        }
    }
}
