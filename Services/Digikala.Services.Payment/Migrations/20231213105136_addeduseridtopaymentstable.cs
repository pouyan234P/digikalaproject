using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Payment.Migrations
{
    public partial class addeduseridtopaymentstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userid",
                table: "payments");
        }
    }
}
