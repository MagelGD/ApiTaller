using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version_Fix_1 : Migration
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
                name: "agenda_block",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    block_date = table.Column<DateTime>(type: "date", nullable: false),
                    reason = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agenda_block", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "agenda_day_config",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    custom_slots = table.Column<int>(type: "int", nullable: true),
                    is_blocked = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agenda_day_config", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "agenda_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    weeks_to_open = table.Column<int>(type: "int", nullable: false),
                    daily_slots = table.Column<int>(type: "int", nullable: false),
                    business_hours_start = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    business_hours_end = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    working_days = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agenda_settings", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(type: "int(11)", nullable: true),
                    vehicle_id = table.Column<int>(type: "int(11)", nullable: true),
                    service_type_id = table.Column<int>(type: "int(11)", nullable: true),
                    appointment_date = table.Column<DateTime>(type: "date", nullable: false),
                    appointment_time = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    customer_notes = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_source = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: true),
                    contact_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vehicle_description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.id);
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
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Host = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Port = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnableSsl = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SenderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SenderEmail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: true),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
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
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_id = table.Column<int>(type: "int(11)", nullable: false),
                    stock_quantity = table.Column<int>(type: "int(11)", nullable: false),
                    min_stock = table.Column<int>(type: "int(11)", nullable: false),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_id = table.Column<int>(type: "int(11)", nullable: false),
                    movement_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantity = table.Column<int>(type: "int(11)", nullable: false),
                    reference_id = table.Column<int>(type: "int(11)", nullable: true),
                    supplier_id = table.Column<int>(type: "int(11)", nullable: true),
                    observations = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitCost = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    SalePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_history", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory_reception",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    supplier_id = table.Column<int>(type: "int(11)", nullable: true),
                    reception_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    invoice_image_base64 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observations = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_reception", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory_reception_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reception_id = table.Column<int>(type: "int(11)", nullable: false),
                    product_id = table.Column<int>(type: "int(11)", nullable: false),
                    quantity = table.Column<int>(type: "int(11)", nullable: false),
                    unit_cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_reception_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_reception_detail_reception",
                        column: x => x.reception_id,
                        principalTable: "inventory_reception",
                        principalColumn: "id",
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
                name: "mechanic_payment_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    mechanic_id = table.Column<int>(type: "int(11)", nullable: false),
                    payment_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mechanic_payment_settings", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mechanic_payment_settlement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    mechanic_id = table.Column<int>(type: "int(11)", nullable: false),
                    settlement_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    services_count = table.Column<int>(type: "int(11)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mechanic_payment_settlement", x => x.id);
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
                name: "password_reset_token",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int(11)", nullable: false),
                    token = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_used = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValue: 0ul),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValue: 1ul),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_reset_token", x => x.id);
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
                name: "sale",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: true),
                    customer_id = table.Column<int>(type: "int(11)", nullable: false),
                    sale_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    discount_percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    discount_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    down_payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    observations = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale", x => x.id);
                    table.ForeignKey(
                        name: "FK_SALE_CUSTOMER",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sale_id = table.Column<int>(type: "int(11)", nullable: false),
                    product_id = table.Column<int>(type: "int(11)", nullable: true),
                    service_catalog_id = table.Column<int>(type: "int(11)", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantity = table.Column<int>(type: "int(11)", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_SALE_DETAIL_PRODUCT",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SALE_DETAIL_SALE",
                        column: x => x.sale_id,
                        principalTable: "sale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sale_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sale_id = table.Column<int>(type: "int(11)", nullable: false),
                    payment_method_id = table.Column<int>(type: "int(11)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    reference_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_SALE_PAYMENT_METHOD",
                        column: x => x.payment_method_id,
                        principalTable: "payment_method",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SALE_PAYMENT_SALE",
                        column: x => x.sale_id,
                        principalTable: "sale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service_catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    service_type_id = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    default_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    default_minutes = table.Column<int>(type: "int(11)", nullable: false),
                    time_unit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
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
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    service_catalog_id = table.Column<int>(type: "int(11)", nullable: false),
                    brand_model_version_id = table.Column<int>(type: "int(11)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estimated_minutes = table.Column<int>(type: "int(11)", nullable: false),
                    time_unit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_price_by_version", x => x.id);
                    table.ForeignKey(
                        name: "FK_SERVICE_PRICE_BRAND_MODEL_VERSION",
                        column: x => x.brand_model_version_id,
                        principalTable: "BrandModelVersion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SERVICE_PRICE_CATALOG",
                        column: x => x.service_catalog_id,
                        principalTable: "service_catalog",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
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
                    must_change_password = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValue: 0ul),
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
                name: "workshop_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    setting_key = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    setting_value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workshop_settings", x => x.id);
                    table.ForeignKey(
                        name: "FK_WORKSHOP_SETTINGS_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
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
                    down_payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    photo_url = table.Column<string>(type: "longtext", nullable: false)
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
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    work_order_id = table.Column<int>(type: "int(11)", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observations = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_order_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_HISTORY_RESPONSIBLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_HISTORY_WORK_ORDER",
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
                    quote_photo_url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_approved = table.Column<ulong>(type: "bit(1)", nullable: false),
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
                    EstimatedMinutes = table.Column<int>(type: "int", nullable: false),
                    TimeUnit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    warranty_end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_approved = table.Column<ulong>(type: "bit(1)", nullable: false),
                    is_paid_to_mechanic = table.Column<ulong>(type: "bit(1)", nullable: false),
                    paid_to_mechanic_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    mechanic_payment_settlement_id = table.Column<int>(type: "int(11)", nullable: true),
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
                        name: "FK_SERVICE_SETTLEMENT",
                        column: x => x.mechanic_payment_settlement_id,
                        principalTable: "mechanic_payment_settlement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "IX_agenda_block_responsible_user_id",
                table: "agenda_block",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_agenda_day_config_responsible_user_id",
                table: "agenda_day_config",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_agenda_settings_responsible_user_id",
                table: "agenda_settings",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_customer_id",
                table: "appointment",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_responsible_user_id",
                table: "appointment",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_service_type_id",
                table: "appointment",
                column: "service_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_vehicle_id",
                table: "appointment",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_work_order_id",
                table: "appointment",
                column: "work_order_id");

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
                name: "IX_EmailSettings_ResponsibleUserIdNavigationId",
                table: "EmailSettings",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "FK_TYPE_IDENTIFICATION_USER",
                table: "identification_type",
                column: "responsabilidad_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_inventory_product",
                table: "inventory",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "FK_inventory_responsible_user",
                table: "inventory",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_inventory_history_product",
                table: "inventory_history",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "FK_inventory_history_responsible_user",
                table: "inventory_history",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_inventory_history_supplier",
                table: "inventory_history",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "FK_inventory_reception_responsible_user",
                table: "inventory_reception",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_inventory_reception_supplier",
                table: "inventory_reception",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "FK_reception_detail_product",
                table: "inventory_reception_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "FK_reception_detail_reception",
                table: "inventory_reception_detail",
                column: "reception_id");

            migrationBuilder.CreateIndex(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_login_ResponsibleUserId",
                table: "login",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "FK_PAYMENT_SETTINGS_MECHANIC",
                table: "mechanic_payment_settings",
                column: "mechanic_id");

            migrationBuilder.CreateIndex(
                name: "IX_mechanic_payment_settings_responsible_user_id",
                table: "mechanic_payment_settings",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SETTLEMENT_MECHANIC",
                table: "mechanic_payment_settlement",
                column: "mechanic_id");

            migrationBuilder.CreateIndex(
                name: "IX_mechanic_payment_settlement_responsible_user_id",
                table: "mechanic_payment_settlement",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_MODULE_USER",
                table: "module",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_OPERATION_USER",
                table: "operation",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_PASSWORD_RESET_TOKEN_RESP_USER",
                table: "password_reset_token",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_PASSWORD_RESET_TOKEN_USER",
                table: "password_reset_token",
                column: "user_id");

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
                name: "FK_SALE_CUSTOMER",
                table: "sale",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_USER",
                table: "sale",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_WORK_ORDER",
                table: "sale",
                column: "work_order_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_DETAIL_PRODUCT",
                table: "sale_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_DETAIL_SALE",
                table: "sale_detail",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_DETAIL_SERVICE",
                table: "sale_detail",
                column: "service_catalog_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_DETAIL_USER",
                table: "sale_detail",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_PAYMENT_METHOD",
                table: "sale_payment",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_PAYMENT_SALE",
                table: "sale_payment",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "FK_SALE_PAYMENT_USER",
                table: "sale_payment",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_CATALOG_SERVICE_TYPE",
                table: "service_catalog",
                column: "service_type_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_CATALOG_USER",
                table: "service_catalog",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_PRICE_BRAND_MODEL_VERSION",
                table: "service_price_by_version",
                column: "brand_model_version_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_PRICE_CATALOG",
                table: "service_price_by_version",
                column: "service_catalog_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_PRICE_USER",
                table: "service_price_by_version",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_SERVICE_TYPE_USER",
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
                name: "FK_HISTORY_RESPONSIBLE_USER",
                table: "work_order_history",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_HISTORY_WORK_ORDER",
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

            migrationBuilder.CreateIndex(
                name: "IX_work_order_service_mechanic_payment_settlement_id",
                table: "work_order_service",
                column: "mechanic_payment_settlement_id");

            migrationBuilder.CreateIndex(
                name: "FK_WORKSHOP_SETTINGS_USER",
                table: "workshop_settings",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_WORKSHOP_SETTINGS_KEY",
                table: "workshop_settings",
                column: "setting_key",
                unique: true);

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
                name: "FK_agenda_block_user_responsible_user_id",
                table: "agenda_block",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_agenda_day_config_user_responsible_user_id",
                table: "agenda_day_config",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_agenda_settings_user_responsible_user_id",
                table: "agenda_settings",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_customer_customer_id",
                table: "appointment",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_service_type_service_type_id",
                table: "appointment",
                column: "service_type_id",
                principalTable: "service_type",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_user_responsible_user_id",
                table: "appointment",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_vehicle_vehicle_id",
                table: "appointment",
                column: "vehicle_id",
                principalTable: "vehicle",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_work_order_work_order_id",
                table: "appointment",
                column: "work_order_id",
                principalTable: "work_order",
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
                name: "FK_EmailSettings_user_ResponsibleUserIdNavigationId",
                table: "EmailSettings",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TYPE_IDENTIFICATION_USER",
                table: "identification_type",
                column: "responsabilidad_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_product",
                table: "inventory",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_responsible_user",
                table: "inventory",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_product",
                table: "inventory_history",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_responsible_user",
                table: "inventory_history",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_history_supplier",
                table: "inventory_history",
                column: "supplier_id",
                principalTable: "supplier",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_reception_responsible_user",
                table: "inventory_reception",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_reception_supplier",
                table: "inventory_reception",
                column: "supplier_id",
                principalTable: "supplier",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_reception_detail_product",
                table: "inventory_reception_detail",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "ResponsibleUserId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PAYMENT_SETTINGS_MECHANIC",
                table: "mechanic_payment_settings",
                column: "mechanic_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PAYMENT_SETTINGS_RESPONSIBLE_USER",
                table: "mechanic_payment_settings",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SETTLEMENT_MECHANIC",
                table: "mechanic_payment_settlement",
                column: "mechanic_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SETTLEMENT_RESPONSIBLE_USER",
                table: "mechanic_payment_settlement",
                column: "responsible_user_id",
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
                name: "FK_PASSWORD_RESET_TOKEN_RESP_USER",
                table: "password_reset_token",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PASSWORD_RESET_TOKEN_USER",
                table: "password_reset_token",
                column: "user_id",
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
                name: "FK_SALE_USER",
                table: "sale",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SALE_WORK_ORDER",
                table: "sale",
                column: "work_order_id",
                principalTable: "work_order",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SALE_DETAIL_SERVICE",
                table: "sale_detail",
                column: "service_catalog_id",
                principalTable: "service_catalog",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SALE_DETAIL_USER",
                table: "sale_detail",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SALE_PAYMENT_USER",
                table: "sale_payment",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SERVICE_CATALOG_SERVICE_TYPE",
                table: "service_catalog",
                column: "service_type_id",
                principalTable: "service_type",
                principalColumn: "id");

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
                name: "agenda_block");

            migrationBuilder.DropTable(
                name: "agenda_day_config");

            migrationBuilder.DropTable(
                name: "agenda_settings");

            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "inventory_history");

            migrationBuilder.DropTable(
                name: "inventory_reception_detail");

            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "mechanic_payment_settings");

            migrationBuilder.DropTable(
                name: "password_reset_token");

            migrationBuilder.DropTable(
                name: "roleaction");

            migrationBuilder.DropTable(
                name: "sale_detail");

            migrationBuilder.DropTable(
                name: "sale_payment");

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
                name: "workshop_settings");

            migrationBuilder.DropTable(
                name: "inventory_reception");

            migrationBuilder.DropTable(
                name: "action");

            migrationBuilder.DropTable(
                name: "payment_method");

            migrationBuilder.DropTable(
                name: "sale");

            migrationBuilder.DropTable(
                name: "service_catalog");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "mechanic_payment_settlement");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "operation");

            migrationBuilder.DropTable(
                name: "work_order");

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
