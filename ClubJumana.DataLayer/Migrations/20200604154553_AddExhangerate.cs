using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class AddExhangerate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExChangeRate",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LandedCostRate",
                table: "Countries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExChangeRate",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LandedCostRate",
                table: "Countries");
        }
    }
}
