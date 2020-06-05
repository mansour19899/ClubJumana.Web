using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class EditCurrency2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ExChangeRate",
                table: "Countries",
                type: "decimal(14,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,10)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ExChangeRate",
                table: "Countries",
                type: "decimal(14,10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldNullable: true);
        }
    }
}
