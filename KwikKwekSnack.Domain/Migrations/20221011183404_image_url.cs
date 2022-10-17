using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwikKwekSnack.Domain.Migrations
{
    public partial class image_url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Drink");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Snacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Extras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Drink",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Drink");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Snacks",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Extras",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Drink",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
