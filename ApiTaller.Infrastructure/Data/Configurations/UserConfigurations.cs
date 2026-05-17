using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
           
                entity.HasKey(e => e.Id);

                entity.ToTable("user");

                entity.HasIndex(e => e.UserRoleId, "FK_USER_USER_ROLE");

                entity.HasIndex(e => e.IdentificationTypeId, "FK_USER_TYPE_IDENTIFICATION");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.MustChangePassword)
                    .HasColumnType("bit(1)")
                    .HasColumnName("must_change_password")
                    .HasDefaultValue(false);
                entity.Property(e => e.AssignmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("assignment_date");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration_date");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                entity.Property(e => e.UserRoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_role_id");
                entity.Property(e => e.IdentificationTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("identification_type_id");
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("full_name");
                entity.Property(e => e.IdentificationNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("identification_number");
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password");
                entity.Property(e => e.FirstSurname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("first_surname");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("first_name");
                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(255)
                    .HasColumnName("second_last_name");
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .HasColumnName("middle_name");
                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("token");
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.UserRoleIdNavigation).WithMany()
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_USER_ROLE");

                entity.HasOne(d => d.IdentificationTypeIdNavigation).WithMany()
                    .HasForeignKey(d => d.IdentificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_TYPE_IDENTIFICATION");
       
        }
    }
}
