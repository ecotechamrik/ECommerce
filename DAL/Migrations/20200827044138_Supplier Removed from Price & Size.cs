using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SupplierRemovedfromPriceSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_Suppliers_SupplierID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "InboundCost",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "LandedCost",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "TransportationCost",
                table: "ProductSizeAndPrices");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_Suppliers_SupplierID",
                table: "ProductSizeAndPrices",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_Suppliers_SupplierID",
                table: "ProductSizeAndPrices");

            migrationBuilder.AddColumn<double>(
                name: "InboundCost",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LandedCost",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TransportationCost",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_Suppliers_SupplierID",
                table: "ProductSizeAndPrices",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
