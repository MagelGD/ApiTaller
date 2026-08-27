using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    internal class WorkshopModuleConfigurations : IEntityTypeConfiguration<WorkshopModule>
    {
        public void Configure(EntityTypeBuilder<WorkshopModule> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("workshop_module");

            entity.HasIndex(e => e.WorkshopId, "FK_workshop_module_workshop");
            entity.HasIndex(e => e.ModuleId, "FK_workshop_module_module");
            entity.HasIndex(e => new { e.WorkshopId, e.ModuleId }, "UQ_workshop_module").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.WorkshopId)
                .HasColumnType("int(11)")
                .HasColumnName("workshop_id");
            entity.Property(e => e.ModuleId)
                .HasColumnType("int(11)")
                .HasColumnName("module_id");
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

            entity.HasOne(d => d.WorkshopNavigation)
                .WithMany(p => p.WorkshopModules)
                .HasForeignKey(d => d.WorkshopId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_workshop_module_workshop");

            entity.HasOne(d => d.ModuleNavigation)
                .WithMany()
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_workshop_module_module");

            entity.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_workshop_module_responsible_user");
        }
    }
}
