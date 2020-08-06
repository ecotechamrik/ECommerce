using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProductFieldsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvDate",
                table: "ProductAttributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Markup",
                table: "ProductAttributes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PriceDate",
                table: "ProductAttributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "PriceVoid",
                table: "ProductAttributes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RetailPriceDisc",
                table: "ProductAttributes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_CurrencyID",
                table: "ProductAttributes",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Currencies_CurrencyID",
                table: "ProductAttributes",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Currencies_CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "BestQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CurrentQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "InvDate",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Markup",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "PriceDate",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "PriceVoid",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "RetailPriceDisc",
                table: "ProductAttributes");
        }
    }
}
