using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class InventoryHistoryConfiguration : IEntityTypeConfiguration<InventoryHistory>
    {
        public void Configure(EntityTypeBuilder<InventoryHistory> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("inventory_history");

            entity.HasIndex(e => e.ProductId, "FK_inventory_history_product");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_inventory_history_responsible_user");
            entity.HasIndex(e => e.SupplierId, "FK_inventory_history_supplier");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");

            entity.Property(e => e.MovementType)
                .HasMaxLength(50)
                .HasColumnName("movement_type");

            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.Property(e => e.Observations)
                .HasColumnType("longtext")
                .HasColumnName("observations");

            entity.Property(e => e.ReferenceId)
                .HasColumnType("int(11)")
                .HasColumnName("reference_id");

            entity.Property(e => e.SupplierId)
                .HasColumnType("int(11)")
                .HasColumnName("supplier_id");

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

            entity.HasOne(d => d.ProductNavigation).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventory_history_product");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventory_history_responsible_user");

            entity.HasOne(d => d.SupplierNavigation).WithMany()
                .HasForeignKey(d => d.SupplierId)
                .IsRequired(false)
                .HasConstraintName("FK_inventory_history_supplier");
        }
    }
}
