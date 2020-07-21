using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SubCatGalleryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCategoryGalleries");

            migrationBuilder.CreateTable(
                name: "SubCatGalleries",
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
                    table.PrimaryKey("PK_SubCatGalleries", x => x.SubCatGalleryID);
                    table.ForeignKey(
                        name: "FK_SubCatGalleries_SubCategories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCatGalleries_SubCategoryID",
                table: "SubCatGalleries",
                column: "SubCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCatGalleries");

            migrationBuilder.CreateTable(
                name: "SubCategoryGalleries",
                columns: table => new
                {
                    SubCatGalleryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMainImage = table.Column<bool>(type: "bit", nullable: false),
                    LargeSizeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediumSizeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SubCategoryID = table.Column<int>(type: "int", nullable: true),
                    ThumbNailSizeImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
