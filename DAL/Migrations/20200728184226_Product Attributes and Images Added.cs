using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductAttributesandImagesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoorTypeID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "DoorType",
                columns: table => new
                {
                    DoorTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoorTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorType", x => x.DoorTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageID);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    ProductPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeID = table.Column<int>(type: "int", nullable: true),
                    CostingPrice = table.Column<double>(type: "float", nullable: false),
                    MarkupPrice = table.Column<double>(type: "float", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: true),
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
                name: "IX_ProductAttributes_DoorTypeID",
                table: "ProductAttributes",
                column: "DoorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductID",
                table: "ProductImages",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_CurrencyID",
                table: "ProductPrices",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductAttributesProductAttributeID",
                table: "ProductPrices",
                column: "ProductAttributesProductAttributeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes",
                column: "DoorTypeID",
                principalTable: "DoorType",
                principalColumn: "DoorTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "DoorType");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_DoorTypeID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "DoorTypeID",
                table: "ProductAttributes");
        }
    }
}
