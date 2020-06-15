using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class EditImage1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Variants_VariantId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VariantId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_VariantFK",
                table: "Images",
                column: "VariantFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Variants_VariantFK",
                table: "Images",
                column: "VariantFK",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Variants_VariantFK",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VariantFK",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_VariantId",
                table: "Images",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Variants_VariantId",
                table: "Images",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
