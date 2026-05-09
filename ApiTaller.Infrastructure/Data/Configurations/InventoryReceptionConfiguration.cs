using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class InventoryReceptionConfiguration : IEntityTypeConfiguration<InventoryReception>
    {
        public void Configure(EntityTypeBuilder<InventoryReception> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("inventory_reception");

            entity.HasIndex(e => e.SupplierId, "FK_inventory_reception_supplier");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_inventory_reception_responsible_user");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.SupplierId)
                .HasColumnType("int(11)")
                .HasColumnName("supplier_id");

            entity.Property(e => e.ReceptionDate)
                .HasColumnType("datetime")
                .HasColumnName("reception_date");

            entity.Property(e => e.InvoiceImageBase64)
                .HasColumnType("longtext")
                .HasColumnName("invoice_image_base64");

            entity.Property(e => e.Observations)
                .HasMaxLength(1000)
                .HasColumnName("observations");

            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("total_amount");

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

            entity.HasOne(d => d.SupplierNavigation).WithMany()
                .HasForeignKey(d => d.SupplierId)
                .IsRequired(false)
                .HasConstraintName("FK_inventory_reception_supplier");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .IsRequired(false)
                .HasConstraintName("FK_inventory_reception_responsible_user");
        }
    }

    public class InventoryReceptionDetailConfiguration : IEntityTypeConfiguration<InventoryReceptionDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryReceptionDetail> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("inventory_reception_detail");

            entity.HasIndex(e => e.ReceptionId, "FK_reception_detail_reception");
            entity.HasIndex(e => e.ProductId, "FK_reception_detail_product");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.ReceptionId)
                .HasColumnType("int(11)")
                .HasColumnName("reception_id");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");

            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.Property(e => e.UnitCost)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("unit_cost");

          

            entity.HasOne(d => d.ReceptionNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.ReceptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_reception_detail_reception");

            entity.HasOne(d => d.ProductNavigation).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reception_detail_product");

        }
    }
}
