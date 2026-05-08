using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "action",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    module_id = table.Column<int>(type: "int(11)", nullable: false),
                    operation_id = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    slug = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrandModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    models = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandModels", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrandModelVersion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrandId = table.Column<int>(type: "int(11)", nullable: false),
                    ModelId = table.Column<int>(type: "int(11)", nullable: false),
                    version = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandModelVersion", x => x.id);
                    table.ForeignKey(
                        name: "FK_BRAND_BRANDMODELVERSION",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_MODELS_BRANDMODELVERSION",
                        column: x => x.ModelId,
                        principalTable: "BrandModels",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identification_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    identification = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsabilidad_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identification_type", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int(11)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    MinStock = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory_history",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int(11)", nullable: false),
                    MovementType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int(11)", nullable: true),
                    Observations = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitCost = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    SalePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_history", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<ulong>(type: "bit(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_reception", x => x.Id);
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
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_reception_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reception_detail_reception",
                        column: x => x.ReceptionId,
                        principalTable: "inventory_reception",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int(11)", nullable: false),
                    message = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operation", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_method",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_method", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_type_id = table.Column<int>(type: "int(11)", nullable: false),
                    product_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<int>(type: "int(11)", nullable: false),
                    sale_price = table.Column<int>(type: "int(11)", nullable: false),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reference = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_type", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roleaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<int>(type: "int(11)", nullable: false),
                    action_id = table.Column<int>(type: "int(11)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roleaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROLEACTION_ACTION",
                        column: x => x.action_id,
                        principalTable: "action",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service_catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    service_type_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_catalog", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service_price_by_version",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    service_catalog_id = table.Column<int>(type: "int", nullable: false),
                    brand_model_version_id = table.Column<int>(type: "int(11)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estimated_minutes = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_price_by_version", x => x.id);
                    table.ForeignKey(
                        name: "FK_SERVICE_PRICE_BRAND_MODEL_VERSION",
                        column: x => x.brand_model_version_id,
                        principalTable: "BrandModelVersion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SERVICE_PRICE_CATALOG",
                        column: x => x.service_catalog_id,
                        principalTable: "service_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_type", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    document_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    business_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_role_id = table.Column<int>(type: "int(11)", nullable: false),
                    identification_type_id = table.Column<int>(type: "int(11)", nullable: false),
                    identification_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    first_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    middle_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    first_surname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    second_last_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    full_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    token = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assignment_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_USER_TYPE_IDENTIFICATION",
                        column: x => x.identification_type_id,
                        principalTable: "identification_type",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userrole",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrole", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROLE_USER",
                        column: x => x.responsible_user_id,
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

            migrationBuilder.CreateTable(
                name: "user_role_module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_role_id = table.Column<int>(type: "int(11)", nullable: false),
                    module_role_id = table.Column<int>(type: "int(11)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_module", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROLE_MODULE_MODULE",
                        column: x => x.module_role_id,
                        principalTable: "module",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ROLE_MODULE_USER_ROLE",
                        column: x => x.user_role_id,
                        principalTable: "userrole",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_USER_ROLE_MODULE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "work_order_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observations = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    responsible_user_id = table.Column<int>(type: "int", nullable: true),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_order_history_order",
                        column: x => x.work_order_id,
                        principalTable: "work_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_work_order_history_user_ResponsibleUserIdNavigationId",
                        column: x => x.ResponsibleUserIdNavigationId,
                        principalTable: "user",
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
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "FK_ACTION_OPERATION",
                table: "action",
                column: "operation_id");

            migrationBuilder.CreateIndex(
                name: "FK_ACTION_USER",
                table: "action",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_BRAND_USER",
                table: "Brand",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_BRANDMODELS_USER",
                table: "BrandModels",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "FK_BRANDMODELVERSION_BRAND",
                table: "BrandModelVersion",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "FK_BRANDMODELVERSION_MODELS",
                table: "BrandModelVersion",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "FK_BRANDMODELVERSION_USER",
                table: "BrandModelVersion",
                column: "responsible_user_id");

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
                name: "FK_TYPE_IDENTIFICATION_USER",
                table: "identification_type",
                column: "responsabilidad_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_ProductId",
                table: "inventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_ResponsibleUserId",
                table: "inventory",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_history_ProductId",
                table: "inventory_history",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_history_ResponsibleUserId",
                table: "inventory_history",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_history_SupplierId",
                table: "inventory_history",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_reception_ResponsibleUserId",
                table: "inventory_reception",
                column: "ResponsibleUserId");

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

            migrationBuilder.CreateIndex(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_login_ResponsibleUserId",
                table: "login",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "FK_MODULE_USER",
                table: "module",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_OPERATION_USER",
                table: "operation",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_PAYMENT_METHOD_USER",
                table: "payment_method",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_PRODUCT_TYPE_PRODUCT",
                table: "product",
                column: "product_type_id");

            migrationBuilder.CreateIndex(
                name: "FK_PRODUCT_USER",
                table: "product",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_TYPE_PRODUCT_USER",
                table: "product_type",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLEACTION_ACTION",
                table: "roleaction",
                column: "action_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLEACTION_USER",
                table: "roleaction",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLEACTION_USERROLE",
                table: "roleaction",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_catalog_responsible_user_id",
                table: "service_catalog",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_catalog_service_type_id",
                table: "service_catalog",
                column: "service_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_price_by_version_brand_model_version_id",
                table: "service_price_by_version",
                column: "brand_model_version_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_price_by_version_responsible_user_id",
                table: "service_price_by_version",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_price_by_version_service_catalog_id",
                table: "service_price_by_version",
                column: "service_catalog_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_type_responsible_user_id",
                table: "service_type",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SUPPLIER_USER",
                table: "supplier",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_USER_TYPE_IDENTIFICATION",
                table: "user",
                column: "identification_type_id");

            migrationBuilder.CreateIndex(
                name: "FK_USER_USER_ROLE",
                table: "user",
                column: "user_role_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLE_MODULE_MODULE",
                table: "user_role_module",
                column: "module_role_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLE_MODULE_USER_ROLE",
                table: "user_role_module",
                column: "user_role_id");

            migrationBuilder.CreateIndex(
                name: "FK_USER_ROLE_MODULE_USER",
                table: "user_role_module",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_ROLE_USER",
                table: "userrole",
                column: "responsible_user_id");

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
                name: "IX_work_order_history_ResponsibleUserIdNavigationId",
                table: "work_order_history",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_work_order_history_work_order_id",
                table: "work_order_history",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action",
                column: "module_id",
                principalTable: "module",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_OPERATION",
                table: "action",
                column: "operation_id",
                principalTable: "operation",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_USER",
                table: "action",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_BRAND_USER",
                table: "Brand",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_BRANDMODELS_USER",
                table: "BrandModels",
                column: "ResponsibleUserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_BRANDMODELVERSION_USER",
                table: "BrandModelVersion",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTOMER_IDENTIFICATION_TYPE",
                table: "customer",
                column: "identification_type_id",
                principalTable: "identification_type",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTOMER_RESPONSIBLE_USER",
                table: "customer",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTOMER_USER",
                table: "customer",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TYPE_IDENTIFICATION_USER",
                table: "identification_type",
                column: "responsabilidad_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_product",
                table: "inventory",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_responsible_user",
                table: "inventory",
                column: "ResponsibleUserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_product",
                table: "inventory_history",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_responsible_user",
                table: "inventory_history",
                column: "ResponsibleUserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_supplier",
                table: "inventory_history",
                column: "SupplierId",
                principalTable: "supplier",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_reception_responsible_user",
                table: "inventory_reception",
                column: "ResponsibleUserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_reception_supplier",
                table: "inventory_reception",
                column: "SupplierId",
                principalTable: "supplier",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_reception_detail_product",
                table: "inventory_reception_detail",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "ResponsibleUserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_MODULE_USER",
                table: "module",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OPERACIONES_USUARIO",
                table: "operation",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PAYMENT_METHOD_USER",
                table: "payment_method",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_TYPE_PRODUCT",
                table: "product",
                column: "product_type_id",
                principalTable: "product_type",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_USER",
                table: "product",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TYPE_PRODUCT_USER",
                table: "product_type",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLEACTION_USER",
                table: "roleaction",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLEACTION_USERROLE",
                table: "roleaction",
                column: "role_id",
                principalTable: "userrole",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SERVICE_CATALOG_SERVICE_TYPE",
                table: "service_catalog",
                column: "service_type_id",
                principalTable: "service_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SERVICE_CATALOG_USER",
                table: "service_catalog",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SERVICE_PRICE_USER",
                table: "service_price_by_version",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SERVICE_TYPE_USER",
                table: "service_type",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SUPPLIER_USER",
                table: "supplier",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_USER_ROLE",
                table: "user",
                column: "user_role_id",
                principalTable: "userrole",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TYPE_IDENTIFICATION_USER",
                table: "identification_type");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_USER",
                table: "userrole");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "inventory_history");

            migrationBuilder.DropTable(
                name: "inventory_reception_detail");

            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "payment_method");

            migrationBuilder.DropTable(
                name: "roleaction");

            migrationBuilder.DropTable(
                name: "service_price_by_version");

            migrationBuilder.DropTable(
                name: "user_role_module");

            migrationBuilder.DropTable(
                name: "work_order_evidence");

            migrationBuilder.DropTable(
                name: "work_order_history");

            migrationBuilder.DropTable(
                name: "work_order_part");

            migrationBuilder.DropTable(
                name: "work_order_service");

            migrationBuilder.DropTable(
                name: "inventory_reception");

            migrationBuilder.DropTable(
                name: "action");

            migrationBuilder.DropTable(
                name: "service_catalog");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "work_order");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "operation");

            migrationBuilder.DropTable(
                name: "service_type");

            migrationBuilder.DropTable(
                name: "product_type");

            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "BrandModelVersion");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "BrandModels");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "identification_type");

            migrationBuilder.DropTable(
                name: "userrole");
        }
    }
}
