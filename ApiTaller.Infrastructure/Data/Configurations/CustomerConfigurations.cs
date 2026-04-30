using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Domain.Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Customer> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("customer");
            
            entity.HasIndex(e => e.UserId, "FK_CUSTOMER_USER");
            entity.HasIndex(e => e.IdentificationTypeId, "FK_CUSTOMER_IDENTIFICATION_TYPE");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_CUSTOMER_RESPONSIBLE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.IdentificationTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("identification_type_id");
            entity.Property(e => e.IdentificationNumber)
                .HasMaxLength(50)
                .HasColumnName("identification_number");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
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

            entity.HasOne(d => d.UserIdNavigation).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_USER");

            entity.HasOne(d => d.IdentificationTypeNavigation).WithMany()
                .HasForeignKey(d => d.IdentificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_IDENTIFICATION_TYPE");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_RESPONSIBLE_USER");
        }
    }
}
