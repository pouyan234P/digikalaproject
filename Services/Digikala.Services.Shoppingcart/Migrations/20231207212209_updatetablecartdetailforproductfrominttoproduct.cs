using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Shoppingcart.Migrations
{
    public partial class updatetablecartdetailforproductfrominttoproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productid",
                table: "cartdetails");

            migrationBuilder.AddColumn<int>(
                name: "productidid",
                table: "cartdetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartdetails_productidid",
                table: "cartdetails",
                column: "productidid");

            migrationBuilder.AddForeignKey(
                name: "FK_cartdetails_products_productidid",
                table: "cartdetails",
                column: "productidid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartdetails_products_productidid",
                table: "cartdetails");

            migrationBuilder.DropIndex(
                name: "IX_cartdetails_productidid",
                table: "cartdetails");

            migrationBuilder.DropColumn(
                name: "productidid",
                table: "cartdetails");

            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "cartdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
