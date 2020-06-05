using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class RemoveTowel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Bedings_BedingId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Towels_TowelId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Bedings");

            migrationBuilder.DropIndex(
                name: "IX_Images_BedingId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TowelId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Gsm",
                table: "Towels");

            migrationBuilder.DropColumn(
                name: "BedingId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TowelId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_VariantId",
                table: "Images",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Towels_VariantId",
                table: "Images",
                column: "VariantId",
                principalTable: "Towels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Towels_VariantId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VariantId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "Images");

            migrationBuilder.AddColumn<short>(
                name: "Gsm",
                table: "Towels",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BedingId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TowelId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bedings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BarcodeFK = table.Column<int>(type: "int", nullable: true),
                    ColourFK = table.Column<int>(type: "int", nullable: true),
                    Data1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FobPrice = table.Column<float>(type: "real", nullable: true),
                    IsKing = table.Column<bool>(type: "bit", nullable: false),
                    LastDateEdited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductFK = table.Column<int>(type: "int", nullable: true),
                    ProductTypeFK = table.Column<int>(type: "int", nullable: true),
                    RetailPrice = table.Column<float>(type: "real", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WholesaleA = table.Column<float>(type: "real", nullable: true),
                    WholesaleB = table.Column<float>(type: "real", nullable: true),
                    Width = table.Column<float>(type: "real", nullable: true),
                    length = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bedings_Barcodes_BarcodeFK",
                        column: x => x.BarcodeFK,
                        principalTable: "Barcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bedings_Colours_ColourFK",
                        column: x => x.ColourFK,
                        principalTable: "Colours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bedings_Productw_ProductFK",
                        column: x => x.ProductFK,
                        principalTable: "Productw",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bedings_ProductTypes_ProductTypeFK",
                        column: x => x.ProductTypeFK,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_BedingId",
                table: "Images",
                column: "BedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TowelId",
                table: "Images",
                column: "TowelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bedings_BarcodeFK",
                table: "Bedings",
                column: "BarcodeFK",
                unique: true,
                filter: "[BarcodeFK] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bedings_ColourFK",
                table: "Bedings",
                column: "ColourFK");

            migrationBuilder.CreateIndex(
                name: "IX_Bedings_ProductFK",
                table: "Bedings",
                column: "ProductFK");

            migrationBuilder.CreateIndex(
                name: "IX_Bedings_ProductTypeFK",
                table: "Bedings",
                column: "ProductTypeFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Bedings_BedingId",
                table: "Images",
                column: "BedingId",
                principalTable: "Bedings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Towels_TowelId",
                table: "Images",
                column: "TowelId",
                principalTable: "Towels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
