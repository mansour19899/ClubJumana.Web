using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class InitialCreate30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelDate",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "OrderedDate",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "SalesOrderNumber",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ShipToAddressNam1",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ShipToAddressNam2",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ShipToAddressName",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ShipToPostalCode",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ShipToPostalPhone1",
                table: "saleorders");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAbaleToRefund",
                table: "soitems",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(short),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: (short)1);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpriationDate",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MessageOnInvoice",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MessageOnStatment",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PoNumber",
                table: "saleorders",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipVia",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoDate",
                table: "saleorders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNo",
                table: "saleorders",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ExpriationDate",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "MessageOnInvoice",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "MessageOnStatment",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "PoNumber",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ShipVia",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "SoDate",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "TrackingNo",
                table: "saleorders");

            migrationBuilder.AlterColumn<short>(
                name: "IsAbaleToRefund",
                table: "soitems",
                type: "bit",
                nullable: true,
                defaultValue: (short)1,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDate",
                table: "saleorders",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderedDate",
                table: "saleorders",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesOrderNumber",
                table: "saleorders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddressNam1",
                table: "saleorders",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddressNam2",
                table: "saleorders",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddressName",
                table: "saleorders",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToPostalCode",
                table: "saleorders",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToPostalPhone1",
                table: "saleorders",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

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
