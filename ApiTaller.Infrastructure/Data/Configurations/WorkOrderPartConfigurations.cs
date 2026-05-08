using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class WorkOrderPartConfigurations : IEntityTypeConfiguration<Domain.Models.WorkOrderPart>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.WorkOrderPart> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("work_order_part");

            entity.HasIndex(e => e.WorkOrderId, "FK_PART_WORK_ORDER");
            entity.HasIndex(e => e.ProductId, "FK_PART_PRODUCT");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_PART_RESPONSIBLE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.WorkOrderId)
                .HasColumnType("int(11)")
                .HasColumnName("work_order_id");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");

            entity.Property(e => e.PartName)
                .HasMaxLength(255)
                .HasColumnName("part_name");

            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("unit_price");

            entity.Property(e => e.IsProvidedByCustomer)
                .HasColumnType("bit(1)")
                .HasColumnName("is_provided_by_customer");

            entity.Property(e => e.WarrantyEndDate)
                .HasColumnType("datetime")
                .HasColumnName("warranty_end_date");

            entity.Property(e => e.QuotePhotoUrl)
                .HasColumnType("longtext")
                .HasColumnName("quote_photo_url");

            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active");

            entity.Property(e => e.IsApproved)
                .HasColumnType("bit(1)")
                .HasColumnName("is_approved");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.WorkOrderNavigation).WithMany(p => p.Parts)
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PART_WORK_ORDER");

            entity.HasOne(d => d.ProductNavigation).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PART_PRODUCT");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PART_RESPONSIBLE_USER");
        }
    }
}
