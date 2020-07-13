using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class WebsiteInfoTableAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteInfo",
                table: "WebsiteInfo");

            migrationBuilder.RenameTable(
                name: "WebsiteInfo",
                newName: "WebsiteInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteInfos",
                table: "WebsiteInfos",
                column: "WebsiteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteInfos",
                table: "WebsiteInfos");

            migrationBuilder.RenameTable(
                name: "WebsiteInfos",
                newName: "WebsiteInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteInfo",
                table: "WebsiteInfo",
                column: "WebsiteID");
        }
    }
}
