using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CascadeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeThickness_ProductAttributes_ProductAttributeID",
                table: "ProductAttributeThickness");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductID",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDesigns_ProductDesignID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGrades_ProductGradeID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductHeights_ProductHeightID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductWidths_ProductWidthID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_ProductSizeAndPrices_ProductSizeAndPriceID",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierID",
                table: "ProductSuppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeThickness_ProductAttributes_ProductAttributeID",
                table: "ProductAttributeThickness",
                column: "ProductAttributeID",
                principalTable: "ProductAttributes",
                principalColumn: "ProductAttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductID",
                table: "ProductImages",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDesigns_ProductDesignID",
                table: "Products",
                column: "ProductDesignID",
                principalTable: "ProductDesigns",
                principalColumn: "ProductDesignID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGrades_ProductGradeID",
                table: "Products",
                column: "ProductGradeID",
                principalTable: "ProductGrades",
                principalColumn: "ProductGradeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductHeights_ProductHeightID",
                table: "ProductSizeAndPrices",
                column: "ProductHeightID",
                principalTable: "ProductHeights",
                principalColumn: "ProductHeightID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductWidths_ProductWidthID",
                table: "ProductSizeAndPrices",
                column: "ProductWidthID",
                principalTable: "ProductWidths",
                principalColumn: "ProductWidthID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_ProductSizeAndPrices_ProductSizeAndPriceID",
                table: "ProductSuppliers",
                column: "ProductSizeAndPriceID",
                principalTable: "ProductSizeAndPrices",
                principalColumn: "ProductSizeAndPriceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierID",
                table: "ProductSuppliers",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeThickness_ProductAttributes_ProductAttributeID",
                table: "ProductAttributeThickness");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductID",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDesigns_ProductDesignID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGrades_ProductGradeID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductHeights_ProductHeightID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductWidths_ProductWidthID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_ProductSizeAndPrices_ProductSizeAndPriceID",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierID",
                table: "ProductSuppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeThickness_ProductAttributes_ProductAttributeID",
                table: "ProductAttributeThickness",
                column: "ProductAttributeID",
                principalTable: "ProductAttributes",
                principalColumn: "ProductAttributeID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductID",
                table: "ProductImages",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDesigns_ProductDesignID",
                table: "Products",
                column: "ProductDesignID",
                principalTable: "ProductDesigns",
                principalColumn: "ProductDesignID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGrades_ProductGradeID",
                table: "Products",
                column: "ProductGradeID",
                principalTable: "ProductGrades",
                principalColumn: "ProductGradeID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductHeights_ProductHeightID",
                table: "ProductSizeAndPrices",
                column: "ProductHeightID",
                principalTable: "ProductHeights",
                principalColumn: "ProductHeightID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductWidths_ProductWidthID",
                table: "ProductSizeAndPrices",
                column: "ProductWidthID",
                principalTable: "ProductWidths",
                principalColumn: "ProductWidthID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_ProductSizeAndPrices_ProductSizeAndPriceID",
                table: "ProductSuppliers",
                column: "ProductSizeAndPriceID",
                principalTable: "ProductSizeAndPrices",
                principalColumn: "ProductSizeAndPriceID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierID",
                table: "ProductSuppliers",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
