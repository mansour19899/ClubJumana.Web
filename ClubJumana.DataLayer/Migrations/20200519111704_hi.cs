using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class hi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoItem_Products_ProductMaster_fk",
                table: "SoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SoItem_SaleOrders_So_fk",
                table: "SoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoItem",
                table: "SoItem");

            migrationBuilder.RenameTable(
                name: "SoItem",
                newName: "SoItems");

            migrationBuilder.RenameIndex(
                name: "IX_SoItem_So_fk",
                table: "SoItems",
                newName: "IX_SoItems_So_fk");

            migrationBuilder.RenameIndex(
                name: "IX_SoItem_ProductMaster_fk",
                table: "SoItems",
                newName: "IX_SoItems_ProductMaster_fk");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoItems",
                table: "SoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoItems_Products_ProductMaster_fk",
                table: "SoItems",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoItems_SaleOrders_So_fk",
                table: "SoItems",
                column: "So_fk",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoItems_Products_ProductMaster_fk",
                table: "SoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SoItems_SaleOrders_So_fk",
                table: "SoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoItems",
                table: "SoItems");

            migrationBuilder.RenameTable(
                name: "SoItems",
                newName: "SoItem");

            migrationBuilder.RenameIndex(
                name: "IX_SoItems_So_fk",
                table: "SoItem",
                newName: "IX_SoItem_So_fk");

            migrationBuilder.RenameIndex(
                name: "IX_SoItems_ProductMaster_fk",
                table: "SoItem",
                newName: "IX_SoItem_ProductMaster_fk");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoItem",
                table: "SoItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoItem_Products_ProductMaster_fk",
                table: "SoItem",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoItem_SaleOrders_So_fk",
                table: "SoItem",
                column: "So_fk",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
