using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height80",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Height84",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Height90",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Height96",
                table: "ProductAttributes");

            migrationBuilder.AddColumn<int>(
                name: "ProductSizeID",
                table: "ProductAttributes",
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
                name: "IX_ProductAttributes_ProductSizeID",
                table: "ProductAttributes",
                column: "ProductSizeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductSizes_ProductSizeID",
                table: "ProductAttributes",
                column: "ProductSizeID",
                principalTable: "ProductSizes",
                principalColumn: "ProductSizeID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductSizes_ProductSizeID",
                table: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ProductSizeID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ProductSizeID",
                table: "ProductAttributes");

            migrationBuilder.AddColumn<string>(
                name: "Height80",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height84",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height90",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height96",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
