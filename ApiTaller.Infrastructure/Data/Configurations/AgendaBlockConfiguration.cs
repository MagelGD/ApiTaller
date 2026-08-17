using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class AgendaBlockConfiguration : IEntityTypeConfiguration<AgendaBlock>
    {
        public void Configure(EntityTypeBuilder<AgendaBlock> builder)
        {
            builder.ToTable("agenda_block");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.BlockDate)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("block_date");

            builder.Property(e => e.Reason)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("reason");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(e => e.ResponsibleUserId)
                .HasColumnName("responsible_user_id");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId);

            // SAAS-2: Aislamiento por taller
            builder.Property(e => e.WorkshopId)
                .IsRequired()
                .HasColumnName("workshop_id");

            builder.HasOne(d => d.WorkshopNavigation).WithMany()
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("FK_AGENDA_BLOCK_WORKSHOP");
        }
    }
}
