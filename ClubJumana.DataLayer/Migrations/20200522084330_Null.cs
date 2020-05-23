using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class Null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.AlterColumn<int>(
                name: "Warehouse_fk",
                table: "ProductInventoryWarehouses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses",
                column: "Warehouse_fk",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.AlterColumn<int>(
                name: "Warehouse_fk",
                table: "ProductInventoryWarehouses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses",
                column: "Warehouse_fk",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
