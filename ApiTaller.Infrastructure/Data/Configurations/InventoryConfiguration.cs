using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventory");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.StockQuantity).IsRequired();
            builder.Property(e => e.MinStock).IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.UpdatedAt).HasColumnType("datetime");
            builder.Property(e => e.LastUpdate).HasColumnType("datetime");

            builder.HasOne(d => d.ProductNavigation)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventory_product");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .HasConstraintName("FK_inventory_responsible_user");
        }
    }
}
