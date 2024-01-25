using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Product.Migrations
{
    public partial class addaverageandhashcolortoproductstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageScore",
                table: "products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "HashColor",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageScore",
                table: "products");

            migrationBuilder.DropColumn(
                name: "HashColor",
                table: "products");
        }
    }
}
