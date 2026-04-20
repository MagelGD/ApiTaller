using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class version12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roleaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<int>(type: "int(11)", nullable: false),
                    action_id = table.Column<int>(type: "int(11)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roleaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROLEACTION_ACTION",
                        column: x => x.action_id,
                        principalTable: "action",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ROLEACTION_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ROLEACTION_USERROLE",
                        column: x => x.role_id,
                        principalTable: "userrole",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "FK_ROLEACTION_ACTION",
                table: "roleaction",
                column: "action_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLEACTION_USER",
                table: "roleaction",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLEACTION_USERROLE",
                table: "roleaction",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roleaction");
        }
    }
}
