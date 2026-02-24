using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    internal class InventoryConfigurations : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inventory");

            entity.HasIndex(e => e.ProductId, "FK_INVENTORY_PRODUCT");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_INVENTORY_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.ProductIdNavigation).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVENTORY_PRODUCT");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVENTORY_USER");
        }
    }
}
