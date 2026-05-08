using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class WorkOrderEvidenceConfigurations : IEntityTypeConfiguration<Domain.Models.WorkOrderEvidence>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.WorkOrderEvidence> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("work_order_evidence");

            entity.HasIndex(e => e.WorkOrderId, "FK_EVIDENCE_WORK_ORDER");
            entity.HasIndex(e => e.ResponsibleUserId, "FK_EVIDENCE_RESPONSIBLE_USER");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.WorkOrderId)
                .HasColumnType("int(11)")
                .HasColumnName("work_order_id");

            entity.Property(e => e.PhotoUrl)
                .HasColumnType("longtext")
                .HasColumnName("photo_url");

            entity.Property(e => e.EvidenceType)
                .HasMaxLength(100)
                .HasColumnName("evidence_type");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
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

            entity.Property(e => e.ResponsibleUserId)
                .HasColumnType("int(11)")
                .HasColumnName("responsible_user_id");

            entity.HasOne(d => d.WorkOrderNavigation).WithMany(p => p.Evidences)
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EVIDENCE_WORK_ORDER");

            entity.HasOne(d => d.ResponsibleUserIdNavigation).WithMany()
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVIDENCE_RESPONSIBLE_USER");
        }
    }
}
