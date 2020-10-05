using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DescriptonAddedtoProductAttributesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductAttributeThickness");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductAttributes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductAttributeThickness",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
