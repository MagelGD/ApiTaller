using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    /// <summary>
    /// SAAS-0: Configuración de la tabla workshop (el tenant raíz del SaaS).
    /// </summary>
    public class WorkshopConfigurations : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("workshop");

            // Slug único a nivel de plataforma (para subdominios)
            entity.HasIndex(e => e.Slug, "UQ_WORKSHOP_SLUG").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("slug");

            entity.Property(e => e.OwnerEmail)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("owner_email");

            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");

            entity.Property(e => e.City)
                .HasMaxLength(150)
                .HasColumnName("city");

            entity.Property(e => e.WorkshopType)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("workshop_type")
                .HasDefaultValue("moto");

            entity.Property(e => e.Plan)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("plan")
                .HasDefaultValue("basic");

            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            entity.Property(e => e.TrialEndsAt)
                .HasColumnType("datetime")
                .HasColumnName("trial_ends_at");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            // Navegaciones
            entity.HasMany(w => w.Users)
                .WithOne(u => u.WorkshopNavigation)
                .HasForeignKey(u => u.WorkshopId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_USER_WORKSHOP");

            entity.HasMany(w => w.Settings)
                .WithOne(s => s.WorkshopNavigation)
                .HasForeignKey(s => s.WorkshopId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_WORKSHOP_SETTINGS_WORKSHOP");
        }
    }
}
