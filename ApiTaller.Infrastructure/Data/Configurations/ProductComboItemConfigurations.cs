using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class ProductComboItemConfigurations : IEntityTypeConfiguration<ProductComboItem>
    {
        public void Configure(EntityTypeBuilder<ProductComboItem> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("product_combo_item");

            entity.HasIndex(e => e.ParentProductId, "FK_COMBO_PARENT_PRODUCT");
            entity.HasIndex(e => e.ChildProductId, "FK_COMBO_CHILD_PRODUCT");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_COMBO_USER");

            entity.Property(e => e.Id).HasColumnType("int(11)").HasColumnName("id");
            entity.Property(e => e.ParentProductId).HasColumnType("int(11)").HasColumnName("parent_product_id");
            entity.Property(e => e.ChildProductId).HasColumnType("int(11)").HasColumnName("child_product_id");
            entity.Property(e => e.Quantity).HasColumnType("int(11)").HasColumnName("quantity").HasDefaultValue(1);
            entity.Property(e => e.WorkshopId).HasColumnType("int(11)").HasColumnName("workshop_id");
            entity.Property(e => e.IsActive).HasColumnType("bit(1)").HasColumnName("is_active");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("updated_at");
            entity.Property(e => e.ResponsibleUserId).HasColumnType("int(11)").HasColumnName("responsible_user_id");

            entity.HasOne(d => d.ParentProduct)
                .WithMany(p => p.ComboItems)
                .HasForeignKey(d => d.ParentProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_COMBO_PARENT_PRODUCT");

            entity.HasOne(d => d.ChildProduct)
                .WithMany()
                .HasForeignKey(d => d.ChildProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_COMBO_CHILD_PRODUCT");

            entity.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMBO_USER");
        }
    }
}
