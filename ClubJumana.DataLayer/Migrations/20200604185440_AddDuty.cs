using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class AddDuty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LandedCostRate",
                table: "Countries",
                newName: "Duty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duty",
                table: "Countries",
                newName: "LandedCostRate");
        }
    }
}
