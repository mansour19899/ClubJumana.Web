using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations.Online
{
    public partial class _23Jan : Migration
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
                name: "IX_refunds_SaleOrderFK",
                table: "refunds",
                column: "SaleOrderFK");

            migrationBuilder.CreateIndex(
                name: "IX_taxrefunds_RefundFK",
                table: "taxrefunds",
                column: "RefundFK");

           
            migrationBuilder.AddForeignKey(
                name: "FK_refunds_saleorders_SaleOrderFK",
                table: "refunds",
                column: "SaleOrderFK",
                principalTable: "saleorders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refunds_paymentmethods_PaymentMethodFK",
                table: "refunds");

            migrationBuilder.DropForeignKey(
                name: "FK_refunds_deposittos_RefundFromFK",
                table: "refunds");

            migrationBuilder.DropForeignKey(
                name: "FK_refunds_saleorders_SaleOrderFK",
                table: "refunds");

            migrationBuilder.DropTable(
                name: "taxrefunds");

            migrationBuilder.DropIndex(
                name: "IX_refunds_PaymentMethodFK",
                table: "refunds");

            migrationBuilder.DropIndex(
                name: "IX_refunds_RefundFromFK",
                table: "refunds");

            migrationBuilder.DropIndex(
                name: "IX_refunds_SaleOrderFK",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "ChequeNo",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "MessageOnRefund",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "MessageOnStatement",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "PaymentMethodFK",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "RefundFromFK",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "SaleOrderFK",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "ShippingTaxCode",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "TypeOfDiscount",
                table: "refunds");

            migrationBuilder.DropColumn(
                name: "TaxCodeName",
                table: "refunditems");

            migrationBuilder.AlterColumn<short>(
                name: "IsAbaleToRefund",
                table: "soitems",
                type: "bit",
                nullable: true,
                defaultValue: (short)1,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleOrder_fk",
                table: "refunds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "refunds",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.CreateIndex(
                name: "IX_refunds_SaleOrder_fk",
                table: "refunds",
                column: "SaleOrder_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_refunds_saleorders_SaleOrder_fk",
                table: "refunds",
                column: "SaleOrder_fk",
                principalTable: "saleorders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
