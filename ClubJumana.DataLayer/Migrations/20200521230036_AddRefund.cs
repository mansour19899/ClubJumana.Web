using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class AddRefund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAbaleToRefund",
                table: "SoItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityRefunded",
                table: "SoItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefundDate = table.Column<DateTime>(nullable: true),
                    RefundNumber = table.Column<int>(nullable: true),
                    SaleOrder_fk = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: true),
                    RefundTotalPrice = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refunds_SaleOrders_SaleOrder_fk",
                        column: x => x.SaleOrder_fk,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Refund_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundItems_Products_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundItems_Refunds_Refund_fk",
                        column: x => x.Refund_fk,
                        principalTable: "Refunds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefundItems_ProductMaster_fk",
                table: "RefundItems",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_RefundItems_Refund_fk",
                table: "RefundItems",
                column: "Refund_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_SaleOrder_fk",
                table: "Refunds",
                column: "SaleOrder_fk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefundItems");

            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropColumn(
                name: "IsAbaleToRefund",
                table: "SoItems");

            migrationBuilder.DropColumn(
                name: "QuantityRefunded",
                table: "SoItems");
        }
    }
}
