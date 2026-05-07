using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "inventory_history",
                type: "int(11)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "inventory_reception",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SupplierId = table.Column<int>(type: "int(11)", nullable: true),
                    ReceptionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InvoiceImageBase64 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observations = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: true),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_reception", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventory_reception_supplier",
                        column: x => x.SupplierId,
                        principalTable: "supplier",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_inventory_reception_user_ResponsibleUserIdNavigationId",
                        column: x => x.ResponsibleUserIdNavigationId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory_reception_detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReceptionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int(11)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_reception_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reception_detail_product",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_reception_detail_reception",
                        column: x => x.ReceptionId,
                        principalTable: "inventory_reception",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_history_SupplierId",
                table: "inventory_history",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_reception_ResponsibleUserIdNavigationId",
                table: "inventory_reception",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_reception_SupplierId",
                table: "inventory_reception",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_reception_detail_ProductId",
                table: "inventory_reception_detail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_reception_detail_ReceptionId",
                table: "inventory_reception_detail",
                column: "ReceptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_supplier",
                table: "inventory_history",
                column: "SupplierId",
                principalTable: "supplier",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_history_supplier",
                table: "inventory_history");

            migrationBuilder.DropTable(
                name: "inventory_reception_detail");

            migrationBuilder.DropTable(
                name: "inventory_reception");

            migrationBuilder.DropIndex(
                name: "IX_inventory_history_SupplierId",
                table: "inventory_history");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "inventory_history");
        }
    }
}
