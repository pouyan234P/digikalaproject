using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Product.Migrations
{
    public partial class addupdadecategoryandproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pointofviews_products_Productidid",
                table: "pointofviews");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryidID",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoryidID",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_pointofviews_Productidid",
                table: "pointofviews");

            migrationBuilder.DropColumn(
                name: "CategoryidID",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Productidid",
                table: "pointofviews");

            migrationBuilder.AlterColumn<string>(
                name: "mainpicture",
                table: "products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<byte[]>(
                name: "PictureUrlID",
                table: "products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mainpictureUrlID",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "myCategoryId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryParent",
                table: "categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_products_myCategoryId",
                table: "products",
                column: "myCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_CategoryParent",
                table: "categories",
                column: "CategoryParent");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_CategoryParent",
                table: "categories",
                column: "CategoryParent",
                principalTable: "categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_myCategoryId",
                table: "products",
                column: "myCategoryId",
                principalTable: "categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_CategoryParent",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_myCategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_myCategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_categories_CategoryParent",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "PictureUrlID",
                table: "products");

            migrationBuilder.DropColumn(
                name: "mainpictureUrlID",
                table: "products");

            migrationBuilder.DropColumn(
                name: "myCategoryId",
                table: "products");

            migrationBuilder.AlterColumn<byte>(
                name: "mainpicture",
                table: "products",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryidID",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Productidid",
                table: "pointofviews",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryParent",
                table: "categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryidID",
                table: "products",
                column: "CategoryidID");

            migrationBuilder.CreateIndex(
                name: "IX_pointofviews_Productidid",
                table: "pointofviews",
                column: "Productidid");

            migrationBuilder.AddForeignKey(
                name: "FK_pointofviews_products_Productidid",
                table: "pointofviews",
                column: "Productidid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryidID",
                table: "products",
                column: "CategoryidID",
                principalTable: "categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
