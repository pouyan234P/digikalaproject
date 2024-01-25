using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Shoppingcart.Migrations
{
    public partial class addhashcolortoproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hashColor",
                table: "products",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hashColor",
                table: "products");
        }
    }
}
