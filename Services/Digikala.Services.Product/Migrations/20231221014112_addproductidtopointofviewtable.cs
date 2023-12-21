using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Product.Migrations
{
    public partial class addproductidtopointofviewtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Productidid",
                table: "pointofviews",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pointofviews_products_Productidid",
                table: "pointofviews");

            migrationBuilder.DropIndex(
                name: "IX_pointofviews_Productidid",
                table: "pointofviews");

            migrationBuilder.DropColumn(
                name: "Productidid",
                table: "pointofviews");
        }
    }
}
