using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductHeightTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductThicknesses_ProductThicknessID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_Currencies_CurrencyID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductAttributes_ProductAttributeID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductSizes_ProductSizeID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizeAndPrices_CurrencyID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "ProductSizeAndPrices");

            migrationBuilder.RenameColumn(
                name: "ProductSizeID",
                table: "ProductSizeAndPrices",
                newName: "ProductHeightID");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeID",
                table: "ProductSizeAndPrices",
                newName: "ProductAttributeThicknessID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeAndPrices_ProductSizeID",
                table: "ProductSizeAndPrices",
                newName: "IX_ProductSizeAndPrices_ProductHeightID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeAndPrices_ProductAttributeID",
                table: "ProductSizeAndPrices",
                newName: "IX_ProductSizeAndPrices_ProductAttributeThicknessID");

            migrationBuilder.RenameColumn(
                name: "ProductThicknessID",
                table: "ProductAttributes",
                newName: "SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_ProductThicknessID",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_SupplierID");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductAttributeThickness",
                columns: table => new
                {
                    ProductAttributeThicknessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeID = table.Column<int>(type: "int", nullable: true),
                    ProductThicknessID = table.Column<int>(type: "int", nullable: true),
                    ProductCodeInitials = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeThickness", x => x.ProductAttributeThicknessID);
                    table.ForeignKey(
                        name: "FK_ProductAttributeThickness_ProductAttributes_ProductAttributeID",
                        column: x => x.ProductAttributeID,
                        principalTable: "ProductAttributes",
                        principalColumn: "ProductAttributeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductAttributeThickness_ProductThicknesses_ProductThicknessID",
                        column: x => x.ProductThicknessID,
                        principalTable: "ProductThicknesses",
                        principalColumn: "ProductThicknessID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductHeights",
                columns: table => new
                {
                    ProductHeightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductHeightName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHeights", x => x.ProductHeightID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_CurrencyID",
                table: "ProductAttributes",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeThickness_ProductAttributeID",
                table: "ProductAttributeThickness",
                column: "ProductAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeThickness_ProductThicknessID",
                table: "ProductAttributeThickness",
                column: "ProductThicknessID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Currencies_CurrencyID",
                table: "ProductAttributes",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Suppliers_SupplierID",
                table: "ProductAttributes",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductAttributeThickness_ProductAttributeThicknessID",
                table: "ProductSizeAndPrices",
                column: "ProductAttributeThicknessID",
                principalTable: "ProductAttributeThickness",
                principalColumn: "ProductAttributeThicknessID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductHeights_ProductHeightID",
                table: "ProductSizeAndPrices",
                column: "ProductHeightID",
                principalTable: "ProductHeights",
                principalColumn: "ProductHeightID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Currencies_CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Suppliers_SupplierID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductAttributeThickness_ProductAttributeThicknessID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeAndPrices_ProductHeights_ProductHeightID",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropTable(
                name: "ProductAttributeThickness");

            migrationBuilder.DropTable(
                name: "ProductHeights");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.RenameColumn(
                name: "ProductHeightID",
                table: "ProductSizeAndPrices",
                newName: "ProductSizeID");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeThicknessID",
                table: "ProductSizeAndPrices",
                newName: "ProductAttributeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeAndPrices_ProductHeightID",
                table: "ProductSizeAndPrices",
                newName: "IX_ProductSizeAndPrices_ProductSizeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeAndPrices_ProductAttributeThicknessID",
                table: "ProductSizeAndPrices",
                newName: "IX_ProductSizeAndPrices_ProductAttributeID");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                table: "ProductAttributes",
                newName: "ProductThicknessID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_SupplierID",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_ProductThicknessID");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "ProductSizeAndPrices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    ProductSizeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSizeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.ProductSizeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeAndPrices_CurrencyID",
                table: "ProductSizeAndPrices",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductThicknesses_ProductThicknessID",
                table: "ProductAttributes",
                column: "ProductThicknessID",
                principalTable: "ProductThicknesses",
                principalColumn: "ProductThicknessID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_Currencies_CurrencyID",
                table: "ProductSizeAndPrices",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductAttributes_ProductAttributeID",
                table: "ProductSizeAndPrices",
                column: "ProductAttributeID",
                principalTable: "ProductAttributes",
                principalColumn: "ProductAttributeID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeAndPrices_ProductSizes_ProductSizeID",
                table: "ProductSizeAndPrices",
                column: "ProductSizeID",
                principalTable: "ProductSizes",
                principalColumn: "ProductSizeID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
