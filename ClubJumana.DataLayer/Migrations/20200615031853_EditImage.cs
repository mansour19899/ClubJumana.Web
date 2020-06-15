using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class EditImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedingFK",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TowelFK",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "VariantFK",
                table: "Images",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariantFK",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "BedingFK",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TowelFK",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
