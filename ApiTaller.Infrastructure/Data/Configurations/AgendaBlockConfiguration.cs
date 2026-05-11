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
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(e => e.ResponsibleUserId)
                .HasColumnName("responsible_user_id");

            builder.HasOne(d => d.ResponsibleUserIdNavigation)
                .WithMany()
                .HasForeignKey(d => d.ResponsibleUserId);
        }
    }
}
