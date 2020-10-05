using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CodeFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategoryCode",
                table: "SubCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoorTypeCode",
                table: "DoorType",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryCode",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "DoorTypeCode",
                table: "DoorType");
        }
    }
}
