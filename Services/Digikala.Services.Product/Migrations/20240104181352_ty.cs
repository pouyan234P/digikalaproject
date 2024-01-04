using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Product.Migrations
{
    public partial class ty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "myCategoryId",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryidID",
                table: "products",
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

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryidID",
                table: "products",
                column: "CategoryidID",
                principalTable: "categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryidID",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoryidID",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CategoryidID",
                table: "products");

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
    }
}
