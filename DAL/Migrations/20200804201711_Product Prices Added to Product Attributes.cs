using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductPricesAddedtoProductAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    ProductPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostingPrice = table.Column<double>(type: "float", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: true),
                    MarkupPrice = table.Column<double>(type: "float", nullable: false),
                    ProductAttributeID = table.Column<int>(type: "int", nullable: true),
                    ProductAttributesProductAttributeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.ProductPriceID);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPrices_ProductAttributes_ProductAttributesProductAttributeID",
                        column: x => x.ProductAttributesProductAttributeID,
                        principalTable: "ProductAttributes",
                        principalColumn: "ProductAttributeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_CurrencyID",
                table: "ProductPrices",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductAttributesProductAttributeID",
                table: "ProductPrices",
                column: "ProductAttributesProductAttributeID");
        }
    }
}
