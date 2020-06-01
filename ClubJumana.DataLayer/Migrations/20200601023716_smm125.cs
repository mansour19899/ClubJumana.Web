using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class smm125 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productw_Materials_MaterialId",
                table: "Productw");

            migrationBuilder.DropIndex(
                name: "IX_Productw_MaterialId",
                table: "Productw");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Productw");

            migrationBuilder.CreateIndex(
                name: "IX_Productw_MaterialFK",
                table: "Productw",
                column: "MaterialFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Productw_Materials_MaterialFK",
                table: "Productw",
                column: "MaterialFK",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productw_Materials_MaterialFK",
                table: "Productw");

            migrationBuilder.DropIndex(
                name: "IX_Productw_MaterialFK",
                table: "Productw");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Productw",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productw_MaterialId",
                table: "Productw",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productw_Materials_MaterialId",
                table: "Productw",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
