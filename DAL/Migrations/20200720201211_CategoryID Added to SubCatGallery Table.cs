using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CategoryIDAddedtoSubCatGalleryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "SubCatGalleries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCatGalleries_CategoryID",
                table: "SubCatGalleries",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCatGalleries_Categories_CategoryID",
                table: "SubCatGalleries",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCatGalleries_Categories_CategoryID",
                table: "SubCatGalleries");

            migrationBuilder.DropIndex(
                name: "IX_SubCatGalleries_CategoryID",
                table: "SubCatGalleries");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "SubCatGalleries");
        }
    }
}
