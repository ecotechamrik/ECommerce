using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SubCategoryGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubCategoryGalleries",
                columns: table => new
                {
                    SubCatGalleryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThumbNailSizeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediumSizeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LargeSizeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsMainImage = table.Column<bool>(type: "bit", nullable: false),
                    SubCategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryGalleries", x => x.SubCatGalleryID);
                    table.ForeignKey(
                        name: "FK_SubCategoryGalleries_SubCategories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryGalleries_SubCategoryID",
                table: "SubCategoryGalleries",
                column: "SubCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCategoryGalleries");
        }
    }
}
