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
            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "work_order_service",
                newName: "is_approved");

            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "work_order_part",
                newName: "is_approved");

            migrationBuilder.AlterColumn<ulong>(
                name: "is_approved",
                table: "work_order_service",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<ulong>(
                name: "is_approved",
                table: "work_order_part",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_approved",
                table: "work_order_service",
                newName: "IsApproved");

            migrationBuilder.RenameColumn(
                name: "is_approved",
                table: "work_order_part",
                newName: "IsApproved");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "work_order_service",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bit(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "work_order_part",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bit(1)");
        }
    }
}
