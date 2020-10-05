using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LivePriceDisc",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PracticalCost",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PracticalMarkup",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RetailMarkupDisc",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RetailPrice",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LivePriceDisc",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "PracticalCost",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "PracticalMarkup",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "RetailMarkupDisc",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "RetailPrice",
                table: "ProductSizeAndPrices");
        }
    }
}
