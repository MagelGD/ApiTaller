using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class PaymentMethodConfigurations : IEntityTypeConfiguration<Domain.Models.PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.PaymentMethod> entity) 
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("payment_method");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_PAYMENT_METHOD_USER");
            entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
            entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("icon");
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
                .HasConstraintName("FK_PAYMENT_METHOD_USER");
        }
    }
}
