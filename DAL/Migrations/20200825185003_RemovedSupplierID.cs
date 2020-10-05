using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RemovedSupplierID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Suppliers_SupplierID",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_SupplierID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "SupplierID",
                table: "ProductAttributes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_SupplierID",
                table: "ProductAttributes",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Suppliers_SupplierID",
                table: "ProductAttributes",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
