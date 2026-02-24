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
           
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.IdRol, "FK_USUARIO_ROL");

                entity.HasIndex(e => e.IdTipoIdentificacion, "FK_USUARIO_TIPO_IDENTIFICACION");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_USUARIO");
                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("CORREO");
                entity.Property(e => e.Estado)
                    .HasColumnType("bit(1)")
                    .HasColumnName("ESTADO");
                entity.Property(e => e.FechaAsignacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ASIGNACION");
                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");
                entity.Property(e => e.FechaExpiracion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_EXPIRACION");
                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");
                entity.Property(e => e.IdRol)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_ROL");
                entity.Property(e => e.IdTipoIdentificacion)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_TIPO_IDENTIFICACION");
                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("NOMBRE_COMPLETO");
                entity.Property(e => e.NumeroIdentificacion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("NUMERO_IDENTIFICACION");
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("PASSWORD");
                entity.Property(e => e.PrimerApellido)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("PRIMER_APELLIDO");
                entity.Property(e => e.PrimerNombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("PRIMER_NOMBRE");
                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(255)
                    .HasColumnName("SEGUNDO_APELLIDO");
                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(255)
                    .HasColumnName("SEGUNDO_NOMBRE");
                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("TOKEN");
                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("USUARIO");

                entity.HasOne(d => d.IdRolNavigation).WithMany()
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_ROL");

                entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany()
                    .HasForeignKey(d => d.IdTipoIdentificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_TIPO_IDENTIFICACION");
       
        }
    }
}
