using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointment");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.CustomerId)
                .HasColumnName("customer_id");

            builder.Property(e => e.VehicleId)
                .HasColumnName("vehicle_id");

            builder.Property(e => e.ServiceTypeId)
                .HasColumnName("service_type_id");

            builder.Property(e => e.AppointmentDate)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("appointment_date");

            builder.Property(e => e.AppointmentTime)
                .HasColumnName("appointment_time");

            builder.Property(e => e.CustomerNotes)
                .HasColumnType("text")
                .HasColumnName("customer_notes");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status");

            builder.Property(e => e.BookingSource)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("booking_source");

            builder.Property(e => e.WorkOrderId)
                .HasColumnName("work_order_id");

            builder.Property(e => e.ContactName)
                .HasMaxLength(255)
                .HasColumnName("contact_name");

            builder.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .HasColumnName("contact_phone");

            builder.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .HasColumnName("contact_email");

            builder.Property(e => e.VehicleDescription)
                .HasMaxLength(500)
                .HasColumnName("vehicle_description");

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

            builder.HasOne(d => d.CustomerNavigation)
                .WithMany()
                .HasForeignKey(d => d.CustomerId);

            builder.HasOne(d => d.VehicleNavigation)
                .WithMany()
                .HasForeignKey(d => d.VehicleId);

            builder.HasOne(d => d.ServiceTypeNavigation)
                .WithMany()
                .HasForeignKey(d => d.ServiceTypeId);

            builder.HasOne(d => d.WorkOrderNavigation)
                .WithMany()
                .HasForeignKey(d => d.WorkOrderId);
        }
    }
}
