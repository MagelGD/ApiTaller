using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class InventoryHistoryConfiguration : IEntityTypeConfiguration<InventoryHistory>
    {
        public void Configure(EntityTypeBuilder<InventoryHistory> builder)
        {
            builder.ToTable("inventory_history");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.MovementType).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.UpdatedAt).HasColumnType("datetime");

            builder.HasOne(d => d.ProductNavigation)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventory_history_product");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .HasConstraintName("FK_inventory_history_responsible_user");

            builder.HasOne(d => d.SupplierNavigation)
                .WithMany()
                .HasForeignKey(d => d.SupplierId)
                .IsRequired(false)
                .HasConstraintName("FK_inventory_history_supplier");
        }
    }
}
