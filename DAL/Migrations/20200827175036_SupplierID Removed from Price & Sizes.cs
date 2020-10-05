using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SupplierIDRemovedfromPriceSizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_Suppliers_SupplierID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizeAndPrices_SupplierID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "SupplierID",
                table: "ProductSizeAndPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierID",
                table: "ProductSizeAndPrices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeAndPrices_SupplierID",
                table: "ProductSizeAndPrices",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_Suppliers_SupplierID",
                table: "ProductSizeAndPrices",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
