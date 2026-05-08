using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sale_user_ResponsibleUserIdNavigationId",
                table: "sale");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_detail_user_ResponsibleUserIdNavigationId",
                table: "sale_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_payment_user_ResponsibleUserIdNavigationId",
                table: "sale_payment");

            migrationBuilder.DropIndex(
                name: "IX_sale_payment_ResponsibleUserIdNavigationId",
                table: "sale_payment");

            migrationBuilder.DropIndex(
                name: "IX_sale_detail_ResponsibleUserIdNavigationId",
                table: "sale_detail");

            migrationBuilder.DropIndex(
                name: "IX_sale_ResponsibleUserIdNavigationId",
                table: "sale");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserIdNavigationId",
                table: "sale_payment");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserIdNavigationId",
                table: "sale_detail");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserIdNavigationId",
                table: "sale");

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "sale_payment",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "sale_detail",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "sale",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sale_payment_responsible_user_id",
                table: "sale_payment",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_responsible_user_id",
                table: "sale_detail",
                column: "responsible_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_responsible_user_id",
                table: "sale",
                column: "responsible_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_user_responsible_user_id",
                table: "sale",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_detail_user_responsible_user_id",
                table: "sale_detail",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_payment_user_responsible_user_id",
                table: "sale_payment",
                column: "responsible_user_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sale_user_responsible_user_id",
                table: "sale");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_detail_user_responsible_user_id",
                table: "sale_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_payment_user_responsible_user_id",
                table: "sale_payment");

            migrationBuilder.DropIndex(
                name: "IX_sale_payment_responsible_user_id",
                table: "sale_payment");

            migrationBuilder.DropIndex(
                name: "IX_sale_detail_responsible_user_id",
                table: "sale_detail");

            migrationBuilder.DropIndex(
                name: "IX_sale_responsible_user_id",
                table: "sale");

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "sale_payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserIdNavigationId",
                table: "sale_payment",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "sale_detail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserIdNavigationId",
                table: "sale_detail",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "responsible_user_id",
                table: "sale",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserIdNavigationId",
                table: "sale",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_sale_payment_ResponsibleUserIdNavigationId",
                table: "sale_payment",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_detail_ResponsibleUserIdNavigationId",
                table: "sale_detail",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_ResponsibleUserIdNavigationId",
                table: "sale",
                column: "ResponsibleUserIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_user_ResponsibleUserIdNavigationId",
                table: "sale",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sale_detail_user_ResponsibleUserIdNavigationId",
                table: "sale_detail",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sale_payment_user_ResponsibleUserIdNavigationId",
                table: "sale_payment",
                column: "ResponsibleUserIdNavigationId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
