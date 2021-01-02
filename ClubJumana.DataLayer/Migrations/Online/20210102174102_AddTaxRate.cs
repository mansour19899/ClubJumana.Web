using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ClubJumana.DataLayer.Migrations.Online
{
    public partial class AddTaxRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tax_saleorders_SalesOrderFK",
                table: "Tax");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tax",
                table: "Tax");

            migrationBuilder.RenameTable(
                name: "Tax",
                newName: "taxes");

            migrationBuilder.RenameIndex(
                name: "IX_Tax_SalesOrderFK",
                table: "taxes",
                newName: "IX_taxes_SalesOrderFK");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAbaleToRefund",
                table: "soitems",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(short),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: (short)1);

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

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "taxes",
                type: "decimal(7,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taxes",
                table: "taxes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_taxes_saleorders_SalesOrderFK",
                table: "taxes",
                column: "SalesOrderFK",
                principalTable: "saleorders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taxes_saleorders_SalesOrderFK",
                table: "taxes");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taxes",
                table: "taxes");

            migrationBuilder.RenameTable(
                name: "taxes",
                newName: "Tax");

            migrationBuilder.RenameIndex(
                name: "IX_taxes_SalesOrderFK",
                table: "Tax",
                newName: "IX_Tax_SalesOrderFK");

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

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Tax",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,4)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tax",
                table: "Tax",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_saleorders_SalesOrderFK",
                table: "Tax",
                column: "SalesOrderFK",
                principalTable: "saleorders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
