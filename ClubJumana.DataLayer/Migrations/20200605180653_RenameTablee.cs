using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class RenameTablee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Towels_VariantId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Barcodes_BarcodeFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Colours_ColourFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Products_ProductFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_ProductTypes_ProductTypeFK",
                table: "Towels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Towels",
                table: "Towels");

            migrationBuilder.RenameTable(
                name: "Towels",
                newName: "Variants");

            migrationBuilder.RenameIndex(
                name: "IX_Towels_ProductTypeFK",
                table: "Variants",
                newName: "IX_Variants_ProductTypeFK");

            migrationBuilder.RenameIndex(
                name: "IX_Towels_ProductFK",
                table: "Variants",
                newName: "IX_Variants_ProductFK");

            migrationBuilder.RenameIndex(
                name: "IX_Towels_ColourFK",
                table: "Variants",
                newName: "IX_Variants_ColourFK");

            migrationBuilder.RenameIndex(
                name: "IX_Towels_BarcodeFK",
                table: "Variants",
                newName: "IX_Variants_BarcodeFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Variants",
                table: "Variants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Variants_VariantId",
                table: "Images",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Barcodes_BarcodeFK",
                table: "Variants",
                column: "BarcodeFK",
                principalTable: "Barcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Colours_ColourFK",
                table: "Variants",
                column: "ColourFK",
                principalTable: "Colours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Products_ProductFK",
                table: "Variants",
                column: "ProductFK",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_ProductTypes_ProductTypeFK",
                table: "Variants",
                column: "ProductTypeFK",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Variants_VariantId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Barcodes_BarcodeFK",
                table: "Variants");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Colours_ColourFK",
                table: "Variants");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Products_ProductFK",
                table: "Variants");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_ProductTypes_ProductTypeFK",
                table: "Variants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Variants",
                table: "Variants");

            migrationBuilder.RenameTable(
                name: "Variants",
                newName: "Towels");

            migrationBuilder.RenameIndex(
                name: "IX_Variants_ProductTypeFK",
                table: "Towels",
                newName: "IX_Towels_ProductTypeFK");

            migrationBuilder.RenameIndex(
                name: "IX_Variants_ProductFK",
                table: "Towels",
                newName: "IX_Towels_ProductFK");

            migrationBuilder.RenameIndex(
                name: "IX_Variants_ColourFK",
                table: "Towels",
                newName: "IX_Towels_ColourFK");

            migrationBuilder.RenameIndex(
                name: "IX_Variants_BarcodeFK",
                table: "Towels",
                newName: "IX_Towels_BarcodeFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Towels",
                table: "Towels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Towels_VariantId",
                table: "Images",
                column: "VariantId",
                principalTable: "Towels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Barcodes_BarcodeFK",
                table: "Towels",
                column: "BarcodeFK",
                principalTable: "Barcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Colours_ColourFK",
                table: "Towels",
                column: "ColourFK",
                principalTable: "Colours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Products_ProductFK",
                table: "Towels",
                column: "ProductFK",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_ProductTypes_ProductTypeFK",
                table: "Towels",
                column: "ProductTypeFK",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
