using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sale",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: true),
                    customer_id = table.Column<int>(type: "int(11)", nullable: false),
                    sale_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    discount_percent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    discount_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    down_payment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    observations = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int", nullable: true),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale", x => x.id);
                    table.ForeignKey(
                        name: "FK_sale_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_sale_user_ResponsibleUserIdNavigationId",
                        column: x => x.ResponsibleUserIdNavigationId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_work_order_work_order_id",
                        column: x => x.work_order_id,
                        principalTable: "work_order",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sale_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int(11)", nullable: true),
                    service_catalog_id = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int", nullable: true),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_sale_detail_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_sale_detail_sale_sale_id",
                        column: x => x.sale_id,
                        principalTable: "sale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_detail_service_catalog_service_catalog_id",
                        column: x => x.service_catalog_id,
                        principalTable: "service_catalog",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_sale_detail_user_ResponsibleUserIdNavigationId",
                        column: x => x.ResponsibleUserIdNavigationId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sale_id = table.Column<int>(type: "int", nullable: false),
                    payment_method_id = table.Column<int>(type: "int(11)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    reference_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int", nullable: true),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_sale_payment_payment_method_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_method",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_payment_sale_sale_id",
                        column: x => x.sale_id,
                        principalTable: "sale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_payment_user_ResponsibleUserIdNavigationId",
                        column: x => x.ResponsibleUserIdNavigationId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_sale_customer_id",
                table: "sale",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_ResponsibleUserIdNavigationId",
                table: "sale",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_work_order_id",
                table: "sale",
                column: "work_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_product_id",
                table: "sale_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_ResponsibleUserIdNavigationId",
                table: "sale_detail",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_sale_id",
                table: "sale_detail",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_service_catalog_id",
                table: "sale_detail",
                column: "service_catalog_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_payment_payment_method_id",
                table: "sale_payment",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_payment_ResponsibleUserIdNavigationId",
                table: "sale_payment",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_payment_sale_id",
                table: "sale_payment",
                column: "sale_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sale_detail");

            migrationBuilder.DropTable(
                name: "sale_payment");

            migrationBuilder.DropTable(
                name: "sale");
        }
    }
}
