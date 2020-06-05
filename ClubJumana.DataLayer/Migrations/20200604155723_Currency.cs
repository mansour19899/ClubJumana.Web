using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class Currency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DigitalCode",
                table: "Countries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DigitalCode",
                table: "Countries");
        }
    }
}
