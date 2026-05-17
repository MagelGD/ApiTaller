using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "must_change_password",
                table: "user",
                type: "bit(1)",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateTable(
                name: "password_reset_token",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int(11)", nullable: false),
                    token = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_used = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValue: 0ul),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValue: 1ul),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_reset_token", x => x.id);
                    table.ForeignKey(
                        name: "FK_PASSWORD_RESET_TOKEN_RESP_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PASSWORD_RESET_TOKEN_USER",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "FK_PASSWORD_RESET_TOKEN_RESP_USER",
                table: "password_reset_token",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_PASSWORD_RESET_TOKEN_USER",
                table: "password_reset_token",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "password_reset_token");

            migrationBuilder.DropColumn(
                name: "must_change_password",
                table: "user");
        }
    }
}
