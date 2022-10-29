using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikKwekSnack.Domain.Migrations
{
    public partial class readded_fk_drinkorder_snackorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrders_Orders_OrderId",
                table: "DrinkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SnackOrders_Orders_OrderId",
                table: "SnackOrders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "SnackOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "DrinkOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrders_Orders_OrderId",
                table: "DrinkOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SnackOrders_Orders_OrderId",
                table: "SnackOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrders_Orders_OrderId",
                table: "DrinkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SnackOrders_Orders_OrderId",
                table: "SnackOrders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "SnackOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "DrinkOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrders_Orders_OrderId",
                table: "DrinkOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SnackOrders_Orders_OrderId",
                table: "SnackOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
