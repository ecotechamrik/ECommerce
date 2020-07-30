using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductDesignTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDesign",
                table: "ProductDesign");

            migrationBuilder.RenameTable(
                name: "ProductDesign",
                newName: "ProductDesigns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDesigns",
                table: "ProductDesigns",
                column: "ProductDesignID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDesigns",
                table: "ProductDesigns");

            migrationBuilder.RenameTable(
                name: "ProductDesigns",
                newName: "ProductDesign");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDesign",
                table: "ProductDesign",
                column: "ProductDesignID");
        }
    }
}
