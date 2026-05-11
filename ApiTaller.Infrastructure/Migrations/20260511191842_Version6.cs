using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK_agenda_block_user_responsible_user_id",
                table: "agenda_block");

            migrationBuilder.DropForeignKey(
                name: "FK_AgendaDayConfig_user_ResponsibleUserIdNavigationId",
                table: "AgendaDayConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaDayConfig",
                table: "AgendaDayConfig");

            migrationBuilder.DropIndex(
                name: "IX_AgendaDayConfig_ResponsibleUserIdNavigationId",
                table: "AgendaDayConfig");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserIdNavigationId",
                table: "AgendaDayConfig");

            migrationBuilder.RenameTable(
                name: "AgendaDayConfig",
                newName: "agenda_day_config");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "agenda_settings",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "agenda_day_config",
                newName: "reason");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "agenda_day_config",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "agenda_day_config",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "agenda_day_config",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ResponsibleUserId",
                table: "agenda_day_config",
                newName: "responsible_user_id");

            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "agenda_day_config",
                newName: "is_blocked");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "agenda_day_config",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "CustomSlots",
                table: "agenda_day_config",
                newName: "custom_slots");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "agenda_day_config",
                newName: "created_at");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                table: "agenda_settings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "agenda_block",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "agenda_day_config",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "agenda_day_config",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "agenda_day_config",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_blocked",
                table: "agenda_day_config",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "agenda_day_config",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "agenda_day_config",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_agenda_day_config",
                table: "agenda_day_config",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_agenda_day_config_responsible_user_id",
                table: "agenda_day_config",
                column: "responsible_user_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agenda_block_user_responsible_user_id",
                table: "agenda_block");

            migrationBuilder.DropForeignKey(
                name: "FK_agenda_day_config_user_responsible_user_id",
                table: "agenda_day_config");

            migrationBuilder.DropPrimaryKey(
                name: "PK_agenda_day_config",
                table: "agenda_day_config");

            migrationBuilder.DropIndex(
                name: "IX_agenda_day_config_responsible_user_id",
                table: "agenda_day_config");

            migrationBuilder.RenameTable(
                name: "agenda_day_config",
                newName: "AgendaDayConfig");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "agenda_settings",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "reason",
                table: "AgendaDayConfig",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "AgendaDayConfig",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AgendaDayConfig",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "AgendaDayConfig",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "responsible_user_id",
                table: "AgendaDayConfig",
                newName: "ResponsibleUserId");

            migrationBuilder.RenameColumn(
                name: "is_blocked",
                table: "AgendaDayConfig",
                newName: "IsBlocked");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "AgendaDayConfig",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "custom_slots",
                table: "AgendaDayConfig",
                newName: "CustomSlots");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "AgendaDayConfig",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "agenda_settings",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "agenda_block",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "AgendaDayConfig",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "AgendaDayConfig",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleUserId",
                table: "AgendaDayConfig",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlocked",
                table: "AgendaDayConfig",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AgendaDayConfig",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AgendaDayConfig",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserIdNavigationId",
                table: "AgendaDayConfig",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaDayConfig",
                table: "AgendaDayConfig",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaDayConfig_ResponsibleUserIdNavigationId",
                table: "AgendaDayConfig",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_agenda_block_user_responsible_user_id",
                table: "agenda_block",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaDayConfig_user_ResponsibleUserIdNavigationId",
                table: "AgendaDayConfig",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
