using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
                table: "user_role_module");

            migrationBuilder.RenameIndex(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
                table: "user_role_module",
                newName: "FK_ROLE_MODULE_MODULE");

            migrationBuilder.RenameColumn(
                name: "aplication_module_id",
                table: "action",
                newName: "module_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_MODULE_MODULE",
                table: "user_role_module",
                column: "module_role_id",
                principalTable: "module",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_MODULE_MODULE",
                table: "user_role_module");

            migrationBuilder.RenameIndex(
                name: "FK_ROLE_MODULE_MODULE",
                table: "user_role_module",
                newName: "FK_ROLE_MODULE_APLICATION_MODULE");

            migrationBuilder.RenameColumn(
                name: "module_id",
                table: "action",
                newName: "aplication_module_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
                table: "user_role_module",
                column: "module_role_id",
                principalTable: "module",
                principalColumn: "id");
        }
    }
}
