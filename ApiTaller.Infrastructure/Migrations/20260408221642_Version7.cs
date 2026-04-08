using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- BLOQUE 1: OPERACIONES DE LIMPIEZA (YA EJECUTADAS) ---
            /*
            migrationBuilder.DropForeignKey(name: "FK_Module_user_ResponsibleUserIdNavigationId", table: "Module");
            migrationBuilder.DropPrimaryKey(name: "PK_Module", table: "Module");
            migrationBuilder.DropIndex(name: "IX_Module_ResponsibleUserIdNavigationId", table: "Module");
            migrationBuilder.DropColumn(name: "ResponsibleUserIdNavigationId", table: "Module");
            */

            // --- BLOQUE 2: RENOMBRES (YA EJECUTADOS) ---
            // Comentamos estos porque MySQL ya cambió los nombres de la tabla y columnas
            /*
            migrationBuilder.RenameTable(name: "Module", newName: "module");
            migrationBuilder.RenameColumn(name: "Name", table: "module", newName: "name");
            migrationBuilder.RenameColumn(name: "Id", table: "module", newName: "id");
            migrationBuilder.RenameColumn(name: "UpdatedAt", table: "module", newName: "update_at");
            migrationBuilder.RenameColumn(name: "ResponsibleUserId", table: "module", newName: "responsible_user_id");
            migrationBuilder.RenameColumn(name: "IsActive", table: "module", newName: "is_active");
            migrationBuilder.RenameColumn(name: "CreatedAt", table: "module", newName: "created_at");
            */

            // --- BLOQUE 3: REESTABLECER LLAVE PRIMARIA ---
            // Si esto falla diciendo que la PK ya existe, comenta estas 4 líneas también.
            migrationBuilder.AddPrimaryKey(
                name: "PK_module",
                table: "module",
                column: "id");

            // --- BLOQUE 4: ALTERACIONES DE TIPO (LO QUE FALTA) ---
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "module",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            // Configurar AUTO_INCREMENT (Aquí fue donde falló originalmente)
            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "module",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_at",
                table: "module",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "module",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<ulong>(
                name: "is_active",
                table: "module",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "module",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            // --- BLOQUE 5: NUEVAS RELACIONES ---
            migrationBuilder.CreateIndex(
                name: "FK_MODULE_USER",
                table: "module",
                column: "responsible_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_MODULE_USER",
                table: "module",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Sin cambios en el Down
        }
    }
}