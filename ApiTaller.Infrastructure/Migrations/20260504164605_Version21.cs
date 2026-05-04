using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "work_order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vehicle_id = table.Column<int>(type: "int(11)", nullable: false),
                    customer_id = table.Column<int>(type: "int(11)", nullable: false),
                    entry_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    estimated_delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    mileage = table.Column<int>(type: "int(11)", nullable: false),
                    fuel_level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observations = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_WORK_ORDER_CUSTOMER",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_WORK_ORDER_RESPONSIBLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_WORK_ORDER_VEHICLE",
                        column: x => x.vehicle_id,
                        principalTable: "vehicle",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "work_order_evidence",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: false),
                    photo_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    evidence_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_order_evidence", x => x.id);
                    table.ForeignKey(
                        name: "FK_EVIDENCE_RESPONSIBLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_EVIDENCE_WORK_ORDER",
                        column: x => x.work_order_id,
                        principalTable: "work_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "work_order_part",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: false),
                    product_id = table.Column<int>(type: "int(11)", nullable: true),
                    part_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantity = table.Column<int>(type: "int(11)", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_provided_by_customer = table.Column<ulong>(type: "bit(1)", nullable: false),
                    warranty_end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_order_part", x => x.id);
                    table.ForeignKey(
                        name: "FK_PART_PRODUCT",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PART_RESPONSIBLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PART_WORK_ORDER",
                        column: x => x.work_order_id,
                        principalTable: "work_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "work_order_service",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mechanic_id = table.Column<int>(type: "int(11)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    warranty_end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_order_service", x => x.id);
                    table.ForeignKey(
                        name: "FK_SERVICE_MECHANIC",
                        column: x => x.mechanic_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SERVICE_RESPONSIBLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SERVICE_WORK_ORDER",
                        column: x => x.work_order_id,
                        principalTable: "work_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "FK_WORK_ORDER_CUSTOMER",
                table: "work_order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "FK_WORK_ORDER_RESPONSIBLE_USER",
                table: "work_order",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_WORK_ORDER_VEHICLE",
                table: "work_order",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "FK_EVIDENCE_RESPONSIBLE_USER",
                table: "work_order_evidence",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_EVIDENCE_WORK_ORDER",
                table: "work_order_evidence",
                column: "work_order_id");

            migrationBuilder.CreateIndex(
                name: "FK_PART_PRODUCT",
                table: "work_order_part",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "FK_PART_RESPONSIBLE_USER",
                table: "work_order_part",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_PART_WORK_ORDER",
                table: "work_order_part",
                column: "work_order_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_MECHANIC",
                table: "work_order_service",
                column: "mechanic_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_RESPONSIBLE_USER",
                table: "work_order_service",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_WORK_ORDER",
                table: "work_order_service",
                column: "work_order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "work_order_evidence");

            migrationBuilder.DropTable(
                name: "work_order_part");

            migrationBuilder.DropTable(
                name: "work_order_service");

            migrationBuilder.DropTable(
                name: "work_order");
        }
    }
}
