using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
                table: "user_role_module");

            migrationBuilder.DropTable(
                name: "aplication_module");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action",
                column: "aplication_module_id",
                principalTable: "module",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
                table: "user_role_module",
                column: "module_role_id",
                principalTable: "module",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
                table: "user_role_module");

            migrationBuilder.CreateTable(
                name: "aplication_module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aplication_module", x => x.id);
                    table.ForeignKey(
                        name: "FK_APLICATION_MODULE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "FK_APLICATION_MODULE_USER",
                table: "aplication_module",
                column: "responsible_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action",
                column: "aplication_module_id",
                principalTable: "aplication_module",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
                table: "user_role_module",
                column: "module_role_id",
                principalTable: "aplication_module",
                principalColumn: "id");
        }
    }
}
