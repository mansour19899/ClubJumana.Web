using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class EditProductMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OnTheWayInventory",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "GoodsReserved",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockOnHand",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodsReserved",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockOnHand",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OnTheWayInventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
