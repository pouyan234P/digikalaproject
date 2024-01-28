using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Shoppingcart.Migrations
{
    public partial class changeinttostringforhashcolorpropertiesinproducttableforshoppingservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "hashColor",
                table: "products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "hashColor",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
