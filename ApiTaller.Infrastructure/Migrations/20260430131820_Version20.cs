using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int(11)", nullable: false),
                    identification_type_id = table.Column<int>(type: "int(11)", nullable: false),
                    identification_number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    first_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_IDENTIFICATION_TYPE",
                        column: x => x.identification_type_id,
                        principalTable: "identification_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CUSTOMER_RESPONSIBLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CUSTOMER_USER",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(type: "int(11)", nullable: false),
                    plate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    brand_id = table.Column<int>(type: "int(11)", nullable: false),
                    model_id = table.Column<int>(type: "int(11)", nullable: false),
                    version_id = table.Column<int>(type: "int(11)", nullable: true),
                    color = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cylinder_capacity = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.id);
                    table.ForeignKey(
                        name: "FK_VEHICLE_BRAND",
                        column: x => x.brand_id,
                        principalTable: "Brand",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VEHICLE_CUSTOMER",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VEHICLE_MODEL",
                        column: x => x.model_id,
                        principalTable: "BrandModels",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VEHICLE_RESPONSIBLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_VEHICLE_VERSION",
                        column: x => x.version_id,
                        principalTable: "BrandModelVersion",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "FK_CUSTOMER_IDENTIFICATION_TYPE",
                table: "customer",
                column: "identification_type_id");

            migrationBuilder.CreateIndex(
                name: "FK_CUSTOMER_RESPONSIBLE_USER",
                table: "customer",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_CUSTOMER_USER",
                table: "customer",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "FK_VEHICLE_BRAND",
                table: "vehicle",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "FK_VEHICLE_CUSTOMER",
                table: "vehicle",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "FK_VEHICLE_MODEL",
                table: "vehicle",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "FK_VEHICLE_RESPONSIBLE_USER",
                table: "vehicle",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_VEHICLE_VERSION",
                table: "vehicle",
                column: "version_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
