using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    internal class InventoryHistoryConfigurations : IEntityTypeConfiguration<InventoryHistory>
    {
        public void Configure(EntityTypeBuilder<InventoryHistory> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("history_inventory");

            entity.HasIndex(e => e.InventoryId, "FK_HISTORY_INVENTORY_INVENTORY");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_HISTORY_INVENTORY_USER");

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
            entity.Property(e => e.InventoryId)
                .HasColumnType("int(11)")
                .HasColumnName("inventory_id");
            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.InventoryIdNavigation).WithMany()
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HISTORY_INVENTORY_INVENTORY");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HISTORY_INVENTORY_USER");
        }
    }
}
