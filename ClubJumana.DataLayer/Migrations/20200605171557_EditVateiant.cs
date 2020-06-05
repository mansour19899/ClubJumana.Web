using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class EditVateiant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data1",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data2",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data3",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data4",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data5",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data6",
                table: "Towels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data1",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data2",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data3",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data4",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data5",
                table: "Bedings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data6",
                table: "Bedings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data1",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "Data2",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "Data3",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "Data4",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "Data5",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "Data6",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "Data1",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "Data2",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "Data3",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "Data4",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "Data5",
                table: "Bedings");

            migrationBuilder.DropColumn(
                name: "Data6",
                table: "Bedings");
        }
    }
}
