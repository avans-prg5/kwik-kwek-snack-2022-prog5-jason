using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikKwekSnack.Domain.Migrations
{
    public partial class bugfix_order_drinkorders_wrong_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drink_Orders_OrderId",
                table: "Drink");

            migrationBuilder.DropIndex(
                name: "IX_Drink_OrderId",
                table: "Drink");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Drink");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "DrinkOrder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrinkOrder_OrderId",
                table: "DrinkOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrder_Orders_OrderId",
                table: "DrinkOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrder_Orders_OrderId",
                table: "DrinkOrder");

            migrationBuilder.DropIndex(
                name: "IX_DrinkOrder_OrderId",
                table: "DrinkOrder");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "DrinkOrder");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Drink",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drink_OrderId",
                table: "Drink",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drink_Orders_OrderId",
                table: "Drink",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
