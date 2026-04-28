using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandModelVersion_user_ResponsibleUserIdNavigationId",
                table: "BrandModelVersion");

            migrationBuilder.DropIndex(
                name: "IX_BrandModelVersion_ResponsibleUserIdNavigationId",
                table: "BrandModelVersion");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserIdNavigationId",
                table: "BrandModelVersion");

            migrationBuilder.RenameColumn(
                name: "ResponsibleUserId",
                table: "BrandModelVersion",
                newName: "responsible_user_id");

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "BrandModelVersion",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "FK_BRANDMODELVERSION_USER",
                table: "BrandModelVersion",
                column: "responsible_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BRANDMODELVERSION_USER",
                table: "BrandModelVersion",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BRANDMODELVERSION_USER",
                table: "BrandModelVersion");

            migrationBuilder.DropIndex(
                name: "FK_BRANDMODELVERSION_USER",
                table: "BrandModelVersion");

            migrationBuilder.RenameColumn(
                name: "responsible_user_id",
                table: "BrandModelVersion",
                newName: "ResponsibleUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleUserId",
                table: "BrandModelVersion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserIdNavigationId",
                table: "BrandModelVersion",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BrandModelVersion_ResponsibleUserIdNavigationId",
                table: "BrandModelVersion",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandModelVersion_user_ResponsibleUserIdNavigationId",
                table: "BrandModelVersion",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
