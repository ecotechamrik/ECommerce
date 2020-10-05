using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BuildingCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CurrentQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IndexNumber",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LocationCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriorityNumber",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RetailBin",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WholeSaleBin",
                table: "Products");

            migrationBuilder.AlterColumn<double>(
                name: "SellingPrice",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "RetailPriceDisc",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "PriceVoid",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Markup",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "BestQuantityNo",
                table: "ProductSizeAndPrices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingCode",
                table: "ProductSizeAndPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GroupNumber",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndexNumber",
                table: "ProductSizeAndPrices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryLevel",
                table: "ProductSizeAndPrices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryNumber",
                table: "ProductSizeAndPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadTime",
                table: "ProductSizeAndPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationCode",
                table: "ProductSizeAndPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ProductSizeAndPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderNowNo",
                table: "ProductSizeAndPrices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriorityNumber",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RetailBin",
                table: "ProductSizeAndPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WholeSaleBin",
                table: "ProductSizeAndPrices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestQuantityNo",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "BuildingCode",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "IndexNumber",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "InventoryLevel",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "InventoryNumber",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "LeadTime",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "LocationCode",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "OrderNowNo",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "PriorityNumber",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "RetailBin",
                table: "ProductSizeAndPrices");

            migrationBuilder.DropColumn(
                name: "WholeSaleBin",
                table: "ProductSizeAndPrices");

            migrationBuilder.AlterColumn<double>(
                name: "SellingPrice",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "RetailPriceDisc",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PriceVoid",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Markup",
                table: "ProductSizeAndPrices",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BestQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BuildingCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroupNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndexNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriorityNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RetailBin",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WholeSaleBin",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
