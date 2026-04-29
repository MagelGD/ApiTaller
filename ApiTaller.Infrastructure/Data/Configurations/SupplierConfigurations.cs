using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class SupplierConfigurations : IEntityTypeConfiguration<Domain.Models.Supplier>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Supplier> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("supplier");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_SUPPLIER_USER");
            entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
            entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("document_number");
            entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("business_name");
            entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("contact_name");
            entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");
            entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("phone_number");
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
            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SUPPLIER_USER");
        }
    }
}
