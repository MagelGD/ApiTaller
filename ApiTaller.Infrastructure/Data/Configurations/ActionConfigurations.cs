using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class ActionConfigurations : IEntityTypeConfiguration<Domain.Models.Action>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Action> entity)
        {

                entity.HasKey(e => e.Id);   
                
                entity.ToTable("action");

                entity.HasIndex(e => e.ModuleId, "FK_ACTION_APLICATIONMODULE");

                entity.HasIndex(e => e.OperationId, "FK_ACTION_OPERATION");

                entity.HasIndex(e => e.ResponsibleUserId, "FK_ACTION_USER");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("slug");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                entity.Property(e => e.ModuleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("module_id");
                entity.Property(e => e.OperationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("operation_id");
                entity.Property(e => e.ResponsibleUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("responsible_user_id");

                entity.HasOne(d => d.ModuleIdNavigation).WithMany()
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTION_APLICATIONMODULE");

                entity.HasOne(d => d.OperationIdNavigation).WithMany()
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTION_OPERATION");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACTION_USER");
        }
    }
}
