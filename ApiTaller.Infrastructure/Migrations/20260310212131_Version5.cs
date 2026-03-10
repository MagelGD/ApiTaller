using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LOGIN_USER",
                table: "login");

            migrationBuilder.DropForeignKey(
                name: "FK_login_user_ResponsibleUserIdNavigationId",
                table: "login");

            migrationBuilder.DropIndex(
                name: "IX_login_ResponsibleUserIdNavigationId",
                table: "login");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserIdNavigationId",
                table: "login");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleUserId",
                table: "login",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_login_ResponsibleUserId",
                table: "login",
                column: "ResponsibleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "ResponsibleUserId",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LOGIN_USER",
                table: "login");

            migrationBuilder.DropIndex(
                name: "IX_login_ResponsibleUserId",
                table: "login");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleUserId",
                table: "login",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserIdNavigationId",
                table: "login",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_login_ResponsibleUserIdNavigationId",
                table: "login",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_login_user_ResponsibleUserIdNavigationId",
                table: "login",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
