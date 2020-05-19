using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class addSubtotalForPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AsnSubtotal",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrnSubtotal",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PoSubtotal",
                table: "PurchaseOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsnSubtotal",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "GrnSubtotal",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PoSubtotal",
                table: "PurchaseOrders");
        }
    }
}
