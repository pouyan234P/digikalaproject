using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Shoppingcart.Migrations
{
    public partial class addcolorpropertiestoproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "products");
        }
    }
}
