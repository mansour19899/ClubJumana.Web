using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class InitialCreate30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAbaleToRefund",
                table: "soitems",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(short),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: (short)1);

            migrationBuilder.AddColumn<int>(
                name: "VariantFK",
                table: "productmasters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "Checked",
                table: "items",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(short),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: (short)0);

            migrationBuilder.AlterColumn<bool>(
                name: "Alert",
                table: "items",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(short),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 1,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 2,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 3,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 4,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 5,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 6,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 7,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 8,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 9,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 10,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 11,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 12,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 13,
                column: "NeedToUpdate",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_productmasters_VariantFK",
                table: "productmasters",
                column: "VariantFK",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productmasters_variants_VariantFK",
                table: "productmasters",
                column: "VariantFK",
                principalTable: "variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productmasters_variants_VariantFK",
                table: "productmasters");

            migrationBuilder.DropIndex(
                name: "IX_productmasters_VariantFK",
                table: "productmasters");

            migrationBuilder.DropColumn(
                name: "VariantFK",
                table: "productmasters");

            migrationBuilder.AlterColumn<short>(
                name: "IsAbaleToRefund",
                table: "soitems",
                type: "bit",
                nullable: true,
                defaultValue: (short)1,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<short>(
                name: "Checked",
                table: "items",
                type: "bit",
                nullable: true,
                defaultValue: (short)0,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<short>(
                name: "Alert",
                table: "items",
                type: "bit",
                nullable: true,
                defaultValue: (short)0,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 1,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 2,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 3,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 4,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 5,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 6,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 7,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 8,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 9,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 10,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 11,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 12,
                column: "NeedToUpdate",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "tablesversion",
                keyColumn: "Id",
                keyValue: 13,
                column: "NeedToUpdate",
                value: (short)0);
        }
    }
}
