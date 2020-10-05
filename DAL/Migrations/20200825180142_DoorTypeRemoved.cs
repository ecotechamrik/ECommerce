using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DoorTypeRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_DoorTypeID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "DoorTypeID",
                table: "ProductAttributes");

            migrationBuilder.AddColumn<string>(
                name: "ProductAttributeName",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAttributeName",
                table: "ProductAttributes");

            migrationBuilder.AddColumn<int>(
                name: "DoorTypeID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_DoorTypeID",
                table: "ProductAttributes",
                column: "DoorTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_DoorType_DoorTypeID",
                table: "ProductAttributes",
                column: "DoorTypeID",
                principalTable: "DoorType",
                principalColumn: "DoorTypeID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
