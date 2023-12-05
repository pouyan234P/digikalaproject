using Microsoft.EntityFrameworkCore.Migrations;

namespace Digikala.Services.CouponApi.Migrations
{
    public partial class intital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "digicoupons",
                columns: table => new
                {
                    DigicouponID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_digicoupons", x => x.DigicouponID);
                });

            migrationBuilder.CreateTable(
                name: "giftcards",
                columns: table => new
                {
                    GiftcardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftcartCode = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giftcards", x => x.GiftcardID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "digicoupons");

            migrationBuilder.DropTable(
                name: "giftcards");
        }
    }
}
