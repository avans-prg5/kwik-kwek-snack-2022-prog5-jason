using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikKwekSnack.Domain.Migrations
{
    public partial class added_active_table_and_updated_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DrinkSizes",
                columns: new[] { "Id", "FullName", "PriceMultiplier", "ShortName" },
                values: new object[] { 4, "Extra Large", 1.75, "XL" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 1,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 2,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 3,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 4,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 5,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 4,
                column: "Active",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrinkSizes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 1,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 2,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 3,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 4,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 5,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "Snacks",
                keyColumn: "Id",
                keyValue: 4,
                column: "Active",
                value: false);
        }
    }
}
