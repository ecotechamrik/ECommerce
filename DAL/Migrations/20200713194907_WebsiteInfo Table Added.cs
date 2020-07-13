using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class WebsiteInfoTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebsiteInfo",
                columns: table => new
                {
                    WebsiteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpanelUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpanelPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTPUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTPPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainRenewDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostingRenewDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostingProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostingProviderDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostingProviderContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteBannerTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteBannerTagLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DevelopedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressMap = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteInfo", x => x.WebsiteID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebsiteInfo");
        }
    }
}
