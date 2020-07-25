using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SubCatGalleryTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeSizeImage",
                table: "SubCatGalleries");

            migrationBuilder.RenameColumn(
                name: "MediumSizeImage",
                table: "SubCatGalleries",
                newName: "OriginalImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginalImage",
                table: "SubCatGalleries",
                newName: "MediumSizeImage");

            migrationBuilder.AddColumn<string>(
                name: "LargeSizeImage",
                table: "SubCatGalleries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
