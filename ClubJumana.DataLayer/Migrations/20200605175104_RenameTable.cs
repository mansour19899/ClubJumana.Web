using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Products_ProductMaster_fk",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_RefundItems_Products_ProductMaster_fk",
                table: "RefundItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SoItems_Products_ProductMaster_fk",
                table: "SoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Productw_ProductFK",
                table: "Towels");

            migrationBuilder.DropTable(
                name: "Productw");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FobPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GoodsReserved",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Income",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastUpdateInventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MadeIn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Margin",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RefundQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RetailPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleEndDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleStartDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockOnHand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UPC",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VendorCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WholesalePrice",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BrandFK",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyFK",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryOfOrginFK",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescribeMaterial",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialFK",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTittle",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductMasters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    VendorCode = table.Column<int>(nullable: false),
                    StyleNumber = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    UPC = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    MadeIn = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: true),
                    FobPrice = table.Column<decimal>(nullable: true),
                    RetailPrice = table.Column<decimal>(nullable: true),
                    WholesalePrice = table.Column<decimal>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: true),
                    SaleStartDate = table.Column<DateTime>(nullable: true),
                    SaleEndDate = table.Column<DateTime>(nullable: true),
                    Margin = table.Column<string>(nullable: true),
                    StockOnHand = table.Column<int>(nullable: false),
                    GoodsReserved = table.Column<int>(nullable: false),
                    RefundQuantity = table.Column<int>(nullable: true),
                    LastUpdateInventory = table.Column<DateTime>(nullable: false),
                    Income = table.Column<int>(nullable: false),
                    Outcome = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMasters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandFK",
                table: "Products",
                column: "BrandFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyFK",
                table: "Products",
                column: "CompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CountryOfOrginFK",
                table: "Products",
                column: "CountryOfOrginFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialFK",
                table: "Products",
                column: "MaterialFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ProductMasters_ProductMaster_fk",
                table: "Items",
                column: "ProductMaster_fk",
                principalTable: "ProductMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_ProductMasters_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk",
                principalTable: "ProductMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandFK",
                table: "Products",
                column: "BrandFK",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Companies_CompanyFK",
                table: "Products",
                column: "CompanyFK",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Countries_CountryOfOrginFK",
                table: "Products",
                column: "CountryOfOrginFK",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Materials_MaterialFK",
                table: "Products",
                column: "MaterialFK",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefundItems_ProductMasters_ProductMaster_fk",
                table: "RefundItems",
                column: "ProductMaster_fk",
                principalTable: "ProductMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoItems_ProductMasters_ProductMaster_fk",
                table: "SoItems",
                column: "ProductMaster_fk",
                principalTable: "ProductMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Products_ProductFK",
                table: "Towels",
                column: "ProductFK",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ProductMasters_ProductMaster_fk",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_ProductMasters_ProductMaster_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandFK",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Companies_CompanyFK",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Countries_CountryOfOrginFK",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Materials_MaterialFK",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RefundItems_ProductMasters_ProductMaster_fk",
                table: "RefundItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SoItems_ProductMasters_ProductMaster_fk",
                table: "SoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Towels_Products_ProductFK",
                table: "Towels");

            migrationBuilder.DropTable(
                name: "ProductMasters");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandFK",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CompanyFK",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CountryOfOrginFK",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MaterialFK",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandFK",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CompanyFK",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CountryOfOrginFK",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DescribeMaterial",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MaterialFK",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTittle",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FobPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoodsReserved",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Income",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateInventory",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MadeIn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Margin",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Outcome",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RefundQuantity",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RetailPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleEndDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleStartDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockOnHand",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UPC",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorCode",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "WholesalePrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Productw",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BrandFK = table.Column<int>(type: "int", nullable: true),
                    CompanyFK = table.Column<int>(type: "int", nullable: true),
                    CountryOfOrginFK = table.Column<int>(type: "int", nullable: true),
                    DescribeMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialFK = table.Column<int>(type: "int", nullable: true),
                    ProductTittle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    StyleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productw", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productw_Brands_BrandFK",
                        column: x => x.BrandFK,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productw_Companies_CompanyFK",
                        column: x => x.CompanyFK,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productw_Countries_CountryOfOrginFK",
                        column: x => x.CountryOfOrginFK,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productw_Materials_MaterialFK",
                        column: x => x.MaterialFK,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productw_BrandFK",
                table: "Productw",
                column: "BrandFK");

            migrationBuilder.CreateIndex(
                name: "IX_Productw_CompanyFK",
                table: "Productw",
                column: "CompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_Productw_CountryOfOrginFK",
                table: "Productw",
                column: "CountryOfOrginFK");

            migrationBuilder.CreateIndex(
                name: "IX_Productw_MaterialFK",
                table: "Productw",
                column: "MaterialFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Products_ProductMaster_fk",
                table: "Items",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefundItems_Products_ProductMaster_fk",
                table: "RefundItems",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoItems_Products_ProductMaster_fk",
                table: "SoItems",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towels_Productw_ProductFK",
                table: "Towels",
                column: "ProductFK",
                principalTable: "Productw",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
