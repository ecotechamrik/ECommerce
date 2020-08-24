using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SupplierChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InboundCost",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "LandedCost",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TransportationCost",
                table: "Suppliers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InboundCost",
                table: "Suppliers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LandedCost",
                table: "Suppliers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TransportationCost",
                table: "Suppliers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
