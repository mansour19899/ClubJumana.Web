using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class smm2025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_Barcodes_BarcodeFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_Colours_ColourFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_Productw_ProductFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_ProductTypes_ProductTypeFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Barcodes_BarcodeFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Colours_ColourFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Productw_ProductFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_ProductTypes_ProductTypeFK",
                table: "Towels");

            migrationBuilder.DropIndex(
                name: "IX_Towels_BarcodeFK",
                table: "Towels");

            migrationBuilder.DropIndex(
                name: "IX_Bedings_BarcodeFK",
                table: "Bedings");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeFK",
                table: "Towels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductFK",
                table: "Towels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ColourFK",
                table: "Towels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BarcodeFK",
                table: "Towels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeFK",
                table: "Bedings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductFK",
                table: "Bedings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ColourFK",
                table: "Bedings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BarcodeFK",
                table: "Bedings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Towels_BarcodeFK",
                table: "Towels",
                column: "BarcodeFK",
                unique: true,
                filter: "[BarcodeFK] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bedings_BarcodeFK",
                table: "Bedings",
                column: "BarcodeFK",
                unique: true,
                filter: "[BarcodeFK] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_Barcodes_BarcodeFK",
                table: "Bedings",
                column: "BarcodeFK",
                principalTable: "Barcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_Colours_ColourFK",
                table: "Bedings",
                column: "ColourFK",
                principalTable: "Colours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_Productw_ProductFK",
                table: "Bedings",
                column: "ProductFK",
                principalTable: "Productw",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_ProductTypes_ProductTypeFK",
                table: "Bedings",
                column: "ProductTypeFK",
                principalTable: "ProductTypes",
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
                name: "FK_Towels_Productw_ProductFK",
                table: "Towels",
                column: "ProductFK",
                principalTable: "Productw",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_Barcodes_BarcodeFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_Colours_ColourFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_Productw_ProductFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedings_ProductTypes_ProductTypeFK",
                table: "Bedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Barcodes_BarcodeFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Colours_ColourFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Productw_ProductFK",
                table: "Towels");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_ProductTypes_ProductTypeFK",
                table: "Towels");

            migrationBuilder.DropIndex(
                name: "IX_Towels_BarcodeFK",
                table: "Towels");

            migrationBuilder.DropIndex(
                name: "IX_Bedings_BarcodeFK",
                table: "Bedings");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeFK",
                table: "Towels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductFK",
                table: "Towels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColourFK",
                table: "Towels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarcodeFK",
                table: "Towels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeFK",
                table: "Bedings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductFK",
                table: "Bedings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColourFK",
                table: "Bedings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarcodeFK",
                table: "Bedings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Towels_BarcodeFK",
                table: "Towels",
                column: "BarcodeFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bedings_BarcodeFK",
                table: "Bedings",
                column: "BarcodeFK",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_Barcodes_BarcodeFK",
                table: "Bedings",
                column: "BarcodeFK",
                principalTable: "Barcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_Colours_ColourFK",
                table: "Bedings",
                column: "ColourFK",
                principalTable: "Colours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_Productw_ProductFK",
                table: "Bedings",
                column: "ProductFK",
                principalTable: "Productw",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedings_ProductTypes_ProductTypeFK",
                table: "Bedings",
                column: "ProductTypeFK",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Barcodes_BarcodeFK",
                table: "Towels",
                column: "BarcodeFK",
                principalTable: "Barcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Colours_ColourFK",
                table: "Towels",
                column: "ColourFK",
                principalTable: "Colours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Productw_ProductFK",
                table: "Towels",
                column: "ProductFK",
                principalTable: "Productw",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_ProductTypes_ProductTypeFK",
                table: "Towels",
                column: "ProductTypeFK",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
