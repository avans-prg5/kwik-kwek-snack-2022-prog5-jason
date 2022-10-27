using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikKwekSnack.Domain.Migrations
{
    public partial class seed_data_and_bug_fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkExtras_Drink_DrinkId",
                table: "DrinkExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrder_Drink_DrinkId",
                table: "DrinkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrder_DrinkSizes_DrinkSizeId",
                table: "DrinkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrder_Orders_OrderId",
                table: "DrinkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrderExtras_DrinkOrder_DrinkOrderId",
                table: "DrinkOrderExtras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkOrder",
                table: "DrinkOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drink",
                table: "Drink");

            migrationBuilder.RenameTable(
                name: "DrinkOrder",
                newName: "DrinkOrders");

            migrationBuilder.RenameTable(
                name: "Drink",
                newName: "Drinks");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkOrder_OrderId",
                table: "DrinkOrders",
                newName: "IX_DrinkOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkOrder_DrinkSizeId",
                table: "DrinkOrders",
                newName: "IX_DrinkOrders_DrinkSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkOrder_DrinkId",
                table: "DrinkOrders",
                newName: "IX_DrinkOrders_DrinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkOrders",
                table: "DrinkOrders",
                column: "DrinkOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks",
                column: "Id");

            migrationBuilder.InsertData(
                table: "DrinkSizes",
                columns: new[] { "Id", "FullName", "PriceMultiplier", "ShortName" },
                values: new object[,]
                {
                    { 1, "Small", 1.0, "S" },
                    { 2, "Medium", 1.25, "M" },
                    { 3, "Large", 1.5, "L" }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "Description", "ImageURL", "MinimalPrice", "Name" },
                values: new object[,]
                {
                    { 1, "Bruine frisdrank met koolzuur", "https://imgs.search.brave.com/s1tlzTSdN6odOOFO1fLwEPOUyq4gnuw6DfxckSH0ylM/rs:fit:1000:1000:1/g:ce/aHR0cHM6Ly9henRl/Y21leGljYW5wcm9k/dWN0c2FuZGxpcXVv/ci5jb20uYXUvd3At/Y29udGVudC91cGxv/YWRzLzIwMjAvMDUv/UmVkLUNvbGEtY2Fu/cy1henRlYy1tZXhp/Y2FuLmpwZw", 2.5, "Cola" },
                    { 2, "Water zonder koolzuur", null, 1.5, "Spa Blauw" },
                    { 3, "Water met koolzuur", null, 1.5, "Spa Rood" },
                    { 4, "Chocolademelk", null, 3.0, "Chocomel" }
                });

            migrationBuilder.InsertData(
                table: "Extras",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "IJsklontjes", 0.10000000000000001 },
                    { 2, "Rietje", 0.14999999999999999 },
                    { 3, "Slagroom", 0.20000000000000001 },
                    { 4, "Sla", 0.25 },
                    { 5, "Mayonnaise", 0.25 }
                });

            migrationBuilder.InsertData(
                table: "Snacks",
                columns: new[] { "Id", "Description", "ImageURL", "Name", "StandardPrice" },
                values: new object[,]
                {
                    { 1, "Rundvlees hamburger", "https://imgs.search.brave.com/DyyLM6KzO1StGQnV_w3sPjLgSZyelGWt7GQcDkUDXqA/rs:fit:612:539:1/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vcGhvdG9z/L2ZyZXNoLWJ1cmdl/ci1pc29sYXRlZC1w/aWN0dXJlLWlkMTEy/NTE0OTE4Mz9rPTYm/bT0xMTI1MTQ5MTgz/JnM9NjEyeDYxMiZ3/PTAmaD1LeFNmVlVr/M0tQM0JnSFZZYm95/TDlhUkxIcC1mUlly/ZlBjRmVhMHc2OE93/PQ", "Beef Burger", 2.5 },
                    { 2, "Gefrituurde aardappelen", null, "Friet", 1.5 },
                    { 3, "Gefrituurde vleesrol", "https://imgs.search.brave.com/zjRHgiREOLZtvFh-TiPRqiUPLR_1JGBTiErIRUY89UI/rs:fit:800:568:1/g:ce/aHR0cHM6Ly90aHVt/YnMuZHJlYW1zdGlt/ZS5jb20vYi9mcmlr/YW5kZWwtdW0tcGV0/aXNjby1ob2xhbmQl/QzMlQUFzLXRyYWRp/Y2lvbmFsLW1laW8t/Y2FjaG9ycm8tcXVl/bnRlLXRyaXR1cmFk/by1kYS1jYXJuZS1w/ciVDMyVCM3hpbW8t/YWNpbWEtZG8tMTUy/MTc2MzMwLmpwZw", "Frikandel", 1.0 },
                    { 4, "Pizza met plakjes salami", "http://www.clker.com/cliparts/3/9/1/d/1451508004467611065wallpaper-sliced-pizza.jpg", "Pizza Salami", 5.0 }
                });

            migrationBuilder.InsertData(
                table: "DrinkExtras",
                columns: new[] { "DrinkId", "ExtraId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "SnackExtras",
                columns: new[] { "ExtraId", "SnackId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkExtras_Drinks_DrinkId",
                table: "DrinkExtras",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrderExtras_DrinkOrders_DrinkOrderId",
                table: "DrinkOrderExtras",
                column: "DrinkOrderId",
                principalTable: "DrinkOrders",
                principalColumn: "DrinkOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrders_Drinks_DrinkId",
                table: "DrinkOrders",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrders_DrinkSizes_DrinkSizeId",
                table: "DrinkOrders",
                column: "DrinkSizeId",
                principalTable: "DrinkSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrders_Orders_OrderId",
                table: "DrinkOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkExtras_Drinks_DrinkId",
                table: "DrinkExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrderExtras_DrinkOrders_DrinkOrderId",
                table: "DrinkOrderExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrders_Drinks_DrinkId",
                table: "DrinkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrders_DrinkSizes_DrinkSizeId",
                table: "DrinkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkOrders_Orders_OrderId",
                table: "DrinkOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkOrders",
                table: "DrinkOrders");

            migrationBuilder.DeleteData(
                table: "DrinkExtras",
                keyColumns: new[] { "DrinkId", "ExtraId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DrinkExtras",
                keyColumns: new[] { "DrinkId", "ExtraId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "DrinkExtras",
                keyColumns: new[] { "DrinkId", "ExtraId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "DrinkExtras",
                keyColumns: new[] { "DrinkId", "ExtraId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DrinkExtras",
                keyColumns: new[] { "DrinkId", "ExtraId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "DrinkSizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DrinkSizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DrinkSizes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SnackExtras",
                keyColumns: new[] { "ExtraId", "SnackId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "SnackExtras",
                keyColumns: new[] { "ExtraId", "SnackId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "SnackExtras",
                keyColumns: new[] { "ExtraId", "SnackId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "SnackExtras",
                keyColumns: new[] { "ExtraId", "SnackId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Drinks",
                newName: "Drink");

            migrationBuilder.RenameTable(
                name: "DrinkOrders",
                newName: "DrinkOrder");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkOrders_OrderId",
                table: "DrinkOrder",
                newName: "IX_DrinkOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkOrders_DrinkSizeId",
                table: "DrinkOrder",
                newName: "IX_DrinkOrder_DrinkSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkOrders_DrinkId",
                table: "DrinkOrder",
                newName: "IX_DrinkOrder_DrinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drink",
                table: "Drink",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkOrder",
                table: "DrinkOrder",
                column: "DrinkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkExtras_Drink_DrinkId",
                table: "DrinkExtras",
                column: "DrinkId",
                principalTable: "Drink",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrder_Drink_DrinkId",
                table: "DrinkOrder",
                column: "DrinkId",
                principalTable: "Drink",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrder_DrinkSizes_DrinkSizeId",
                table: "DrinkOrder",
                column: "DrinkSizeId",
                principalTable: "DrinkSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrder_Orders_OrderId",
                table: "DrinkOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkOrderExtras_DrinkOrder_DrinkOrderId",
                table: "DrinkOrderExtras",
                column: "DrinkOrderId",
                principalTable: "DrinkOrder",
                principalColumn: "DrinkOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
