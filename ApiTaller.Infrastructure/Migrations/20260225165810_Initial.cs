using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    aplication_module_id = table.Column<int>(type: "int(11)", nullable: false),
                    operation_id = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserResponsibleId = table.Column<int>(type: "int(11)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aplication_module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aplication_module", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "history_inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    inventory_id = table.Column<int>(type: "int(11)", nullable: false),
                    amount = table.Column<int>(type: "int(11)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history_inventory", x => x.id);
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
                    responsabilidad_user_id = table.Column<int>(type: "int(11)", nullable: false)
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
                    amount = table.Column<int>(type: "int(11)", nullable: false),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.id);
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
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.id);
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
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operation", x => x.id);
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
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reference = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_path2 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_path3 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_path4 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
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
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_type", x => x.id);
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
                    token = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assignment_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_activa = table.Column<ulong>(type: "bit(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleUserIdNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_USER_TYPE_IDENTIFICATION",
                        column: x => x.identification_type_id,
                        principalTable: "identification_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_user_ResponsibleUserIdNavigationId",
                        column: x => x.ResponsibleUserIdNavigationId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrole", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROLE_USER",
                        column: x => x.responsible_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    responsible_user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_module", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROLE_MODULE_APLICATION_MODULE",
                        column: x => x.module_role_id,
                        principalTable: "aplication_module",
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

            migrationBuilder.CreateIndex(
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action",
                column: "aplication_module_id");

            migrationBuilder.CreateIndex(
                name: "FK_ACTION_OPERATION",
                table: "action",
                column: "operation_id");

            migrationBuilder.CreateIndex(
                name: "FK_ACTION_USER",
                table: "action",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_action_UserResponsibleId",
                table: "action",
                column: "UserResponsibleId");

            migrationBuilder.CreateIndex(
                name: "FK_APLICATION_MODULE_USER",
                table: "aplication_module",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_HISTORY_INVENTORY_INVENTORY",
                table: "history_inventory",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "FK_HISTORY_INVENTORY_USER",
                table: "history_inventory",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_TYPE_IDENTIFICATION_USER",
                table: "identification_type",
                column: "responsabilidad_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_INVENTORY_PRODUCT",
                table: "inventory",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "FK_INVENTORY_USER",
                table: "inventory",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_login_ResponsibleUserIdNavigationId",
                table: "login",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "FK_OPERATION_USER",
                table: "operation",
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
                name: "FK_USER_TYPE_IDENTIFICATION",
                table: "user",
                column: "identification_type_id");

            migrationBuilder.CreateIndex(
                name: "FK_USER_USER_ROLE",
                table: "user",
                column: "user_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_ResponsibleUserIdNavigationId",
                table: "user",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "FK_ROLE_MODULE_APLICATION_MODULE",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_APLICATIONMODULE",
                table: "action",
                column: "aplication_module_id",
                principalTable: "aplication_module",
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
                column: "UserResponsibleId",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_APLICATION_MODULE_USER",
                table: "aplication_module",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_HISTORY_INVENTORY_INVENTORY",
                table: "history_inventory",
                column: "inventory_id",
                principalTable: "inventory",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_HISTORY_INVENTORY_USER",
                table: "history_inventory",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TYPE_IDENTIFICATION_USER",
                table: "identification_type",
                column: "responsabilidad_user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_INVENTORY_PRODUCT",
                table: "inventory",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_INVENTORY_USER",
                table: "inventory",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_LOGIN_USER",
                table: "login",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_login_user_ResponsibleUserIdNavigationId",
                table: "login",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OPERACIONES_USUARIO",
                table: "operation",
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
                name: "action");

            migrationBuilder.DropTable(
                name: "history_inventory");

            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "user_role_module");

            migrationBuilder.DropTable(
                name: "operation");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "aplication_module");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "product_type");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "identification_type");

            migrationBuilder.DropTable(
                name: "userrole");
        }
    }
}
