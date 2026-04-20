using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class version11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACTION_USER",
                table: "action");

            migrationBuilder.DropIndex(
                name: "IX_action_UserResponsibleId",
                table: "action");

            migrationBuilder.DropColumn(
                name: "UserResponsibleId",
                table: "action");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_USER",
                table: "action",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACTION_USER",
                table: "action");

            migrationBuilder.AddColumn<int>(
                name: "UserResponsibleId",
                table: "action",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_action_UserResponsibleId",
                table: "action",
                column: "UserResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_USER",
                table: "action",
                column: "UserResponsibleId",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
