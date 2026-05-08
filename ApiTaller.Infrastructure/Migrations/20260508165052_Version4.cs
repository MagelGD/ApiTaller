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
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "work_order_service",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "work_order_part",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "work_order_service");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "work_order_part");
        }
    }
}
