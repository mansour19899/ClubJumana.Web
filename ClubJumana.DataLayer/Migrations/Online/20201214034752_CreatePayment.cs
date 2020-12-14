using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations.Online
{
    public partial class CreatePayment : Migration
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

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "saleorders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OpenBalance",
                table: "saleorders",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.CreateTable(
                name: "deposittos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposittos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentmethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentmethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    ReferenceNo = table.Column<string>(nullable: true),
                    PaymentMethodFK = table.Column<int>(nullable: false),
                    DepositToFK = table.Column<int>(nullable: false),
                    AmountReceived = table.Column<decimal>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Attachments = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payments_deposittos_DepositToFK",
                        column: x => x.DepositToFK,
                        principalTable: "deposittos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payments_paymentmethods_PaymentMethodFK",
                        column: x => x.PaymentMethodFK,
                        principalTable: "paymentmethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "paymentinvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    InvoiceFK = table.Column<int>(nullable: false),
                    PaymenteFK = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentinvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paymentinvoices_saleorders_InvoiceFK",
                        column: x => x.InvoiceFK,
                        principalTable: "saleorders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_paymentinvoices_payments_PaymenteFK",
                        column: x => x.PaymenteFK,
                        principalTable: "payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_paymentinvoices_InvoiceFK",
                table: "paymentinvoices",
                column: "InvoiceFK");

            migrationBuilder.CreateIndex(
                name: "IX_paymentinvoices_PaymenteFK",
                table: "paymentinvoices",
                column: "PaymenteFK");

            migrationBuilder.CreateIndex(
                name: "IX_payments_DepositToFK",
                table: "payments",
                column: "DepositToFK");

            migrationBuilder.CreateIndex(
                name: "IX_payments_PaymentMethodFK",
                table: "payments",
                column: "PaymentMethodFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paymentinvoices");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "deposittos");

            migrationBuilder.DropTable(
                name: "paymentmethods");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "saleorders");

            migrationBuilder.DropColumn(
                name: "OpenBalance",
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
