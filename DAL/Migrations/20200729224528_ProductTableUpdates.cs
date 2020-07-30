using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductTableUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoorTypeID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductDesignID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductGradeID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DoorTypeID",
                table: "Products",
                column: "DoorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductDesignID",
                table: "Products",
                column: "ProductDesignID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGradeID",
                table: "Products",
                column: "ProductGradeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DoorType_DoorTypeID",
                table: "Products",
                column: "DoorTypeID",
                principalTable: "DoorType",
                principalColumn: "DoorTypeID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDesigns_ProductDesignID",
                table: "Products",
                column: "ProductDesignID",
                principalTable: "ProductDesigns",
                principalColumn: "ProductDesignID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGrades_ProductGradeID",
                table: "Products",
                column: "ProductGradeID",
                principalTable: "ProductGrades",
                principalColumn: "ProductGradeID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_DoorType_DoorTypeID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDesigns_ProductDesignID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGrades_ProductGradeID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DoorTypeID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductDesignID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductGradeID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DoorTypeID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDesignID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductGradeID",
                table: "Products");
        }
    }
}
