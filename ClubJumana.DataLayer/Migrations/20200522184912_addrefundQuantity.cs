using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class addrefundQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefundQuantity",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefundQuantity",
                table: "ProductInventoryWarehouses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RefundQuantity",
                table: "ProductInventoryWarehouses");
        }
    }
}
