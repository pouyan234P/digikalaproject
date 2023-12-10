using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.Shoppingcart.Migrations
{
    public partial class changethetypeofheaderidincartdetailtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headerid",
                table: "cartdetails");

            migrationBuilder.AddColumn<int>(
                name: "Headeridid",
                table: "cartdetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartdetails_Headeridid",
                table: "cartdetails",
                column: "Headeridid");

            migrationBuilder.AddForeignKey(
                name: "FK_cartdetails_cartHeaders_Headeridid",
                table: "cartdetails",
                column: "Headeridid",
                principalTable: "cartHeaders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartdetails_cartHeaders_Headeridid",
                table: "cartdetails");

            migrationBuilder.DropIndex(
                name: "IX_cartdetails_Headeridid",
                table: "cartdetails");

            migrationBuilder.DropColumn(
                name: "Headeridid",
                table: "cartdetails");

            migrationBuilder.AddColumn<int>(
                name: "Headerid",
                table: "cartdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
