using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_user_ResponsibleUserIdNavigationId",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_ResponsibleUserIdNavigationId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserIdNavigationId",
                table: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserId",
                table: "user",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserIdNavigationId",
                table: "user",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_user_ResponsibleUserIdNavigationId",
                table: "user",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_ResponsibleUserIdNavigationId",
                table: "user",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
