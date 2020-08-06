using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Supplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Currencies_CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductSizes_ProductSizeID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_DoorType_DoorTypeID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DoorTypeID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ProductSizeID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "DoorTypeID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "InvDate",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Markup",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "PriceDate",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "PriceVoid",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ProductSizeID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "RetailPriceDisc",
                table: "ProductAttributes");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InboundCost = table.Column<double>(type: "float", nullable: false),
                    LandedCost = table.Column<double>(type: "float", nullable: false),
                    SupplierCost = table.Column<double>(type: "float", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizeAndPrices",
                columns: table => new
                {
                    ProductSizeAndPricesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAttributeID = table.Column<int>(type: "int", nullable: true),
                    ProductSizeID = table.Column<int>(type: "int", nullable: true),
                    CurrencyID = table.Column<int>(type: "int", nullable: true),
                    PriceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetailPriceDisc = table.Column<double>(type: "float", nullable: false),
                    PriceVoid = table.Column<double>(type: "float", nullable: false),
                    Markup = table.Column<double>(type: "float", nullable: false),
                    SellingPrice = table.Column<double>(type: "float", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: true),
                    InboundCost = table.Column<double>(type: "float", nullable: false),
                    LandedCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizeAndPrices", x => x.ProductSizeAndPricesID);
                    table.ForeignKey(
                        name: "FK_ProductSizeAndPrices_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSizeAndPrices_ProductAttributes_ProductAttributeID",
                        column: x => x.ProductAttributeID,
                        principalTable: "ProductAttributes",
                        principalColumn: "ProductAttributeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductSizeAndPrices_ProductSizes_ProductSizeID",
                        column: x => x.ProductSizeID,
                        principalTable: "ProductSizes",
                        principalColumn: "ProductSizeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductSizeAndPrices_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeAndPrices_CurrencyID",
                table: "ProductSizeAndPrices",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeAndPrices_ProductAttributeID",
                table: "ProductSizeAndPrices",
                column: "ProductAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeAndPrices_ProductSizeID",
                table: "ProductSizeAndPrices",
                column: "ProductSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeAndPrices_SupplierID",
                table: "ProductSizeAndPrices",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes",
                column: "DoorTypeID",
                principalTable: "DoorType",
                principalColumn: "DoorTypeID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductSizeAndPrices");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.AddColumn<int>(
                name: "DoorTypeID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvDate",
                table: "ProductAttributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductAttributes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Markup",
                table: "ProductAttributes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PriceDate",
                table: "ProductAttributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "PriceVoid",
                table: "ProductAttributes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSizeID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RetailPriceDisc",
                table: "ProductAttributes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DoorTypeID",
                table: "Products",
                column: "DoorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_CurrencyID",
                table: "ProductAttributes",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductSizeID",
                table: "ProductAttributes",
                column: "ProductSizeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Currencies_CurrencyID",
                table: "ProductAttributes",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes",
                column: "DoorTypeID",
                principalTable: "DoorType",
                principalColumn: "DoorTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductSizes_ProductSizeID",
                table: "ProductAttributes",
                column: "ProductSizeID",
                principalTable: "ProductSizes",
                principalColumn: "ProductSizeID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DoorType_DoorTypeID",
                table: "Products",
                column: "DoorTypeID",
                principalTable: "DoorType",
                principalColumn: "DoorTypeID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
