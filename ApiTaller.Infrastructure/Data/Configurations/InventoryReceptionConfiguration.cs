using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class InventoryReceptionConfiguration : IEntityTypeConfiguration<InventoryReception>
    {
        public void Configure(EntityTypeBuilder<InventoryReception> builder)
        {
            builder.ToTable("inventory_reception");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ReceptionDate).HasColumnType("datetime");
            builder.Property(e => e.InvoiceImageBase64).HasColumnType("longtext");
            builder.Property(e => e.Observations).HasMaxLength(1000);
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.IsActive).HasColumnType("bit(1)");
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.UpdatedAt).HasColumnType("datetime");
            builder.Property(e => e.ResponsibleUserId).HasColumnType("int(11)");

            builder.HasOne(d => d.SupplierNavigation)
                .WithMany()
                .HasForeignKey(d => d.SupplierId)
                .IsRequired(false)
                .HasConstraintName("FK_inventory_reception_supplier");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .IsRequired(false)
                .HasConstraintName("FK_inventory_reception_responsible_user");
        }
    }

    public class InventoryReceptionDetailConfiguration : IEntityTypeConfiguration<InventoryReceptionDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryReceptionDetail> builder)
        {
            builder.ToTable("inventory_reception_detail");
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.ReceptionNavigation)
                .WithMany(p => p.Details)
                .HasForeignKey(d => d.ReceptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_reception_detail_reception");

            builder.HasOne(d => d.ProductNavigation)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reception_detail_product");
        }
    }
}
