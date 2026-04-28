using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("product");

            entity.HasIndex(e => e.ProducTypeId, "FK_PRODUCT_TYPE_PRODUCT");

            entity.HasIndex(e => e.ResponsibleUserId, "FK_PRODUCT_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.ProducTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("product_type_id");
            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");
            entity.Property(e => e.SalePrice)
                .HasColumnType("int(11)")
                .HasColumnName("sale_price");
            entity.Property(e => e.Reference)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("reference");
            

            entity.HasOne(d => d.ProductTypeIdNavigation).WithMany()
                .HasForeignKey(d => d.ProducTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_TYPE_PRODUCT");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_USER");
        }
    }
}
