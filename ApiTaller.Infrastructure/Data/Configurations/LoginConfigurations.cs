using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class LoginConfigurations : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("login");

            entity.HasIndex(e => e.UserId, "FK_LOGIN_USER");

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
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("message");

            entity.HasOne(d => d.UserIdNavigation).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGIN_USER");
        }
    }
}
