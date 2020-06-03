using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class addWholesaleprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Bedings");

            migrationBuilder.AddColumn<float>(
                name: "FobPrice",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RetailPrice",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WholesaleA",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WholesaleB",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FobPrice",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RetailPrice",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WholesaleA",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WholesaleB",
                table: "Bedings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FobPrice",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "RetailPrice",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "WholesaleA",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "WholesaleB",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "FobPrice",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "RetailPrice",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "WholesaleA",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "WholesaleB",
                table: "Bedings");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Towels",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Bedings",
                type: "real",
                nullable: true);
        }
    }
}
