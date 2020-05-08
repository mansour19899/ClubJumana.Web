using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                maxLength: 200,
                nullable: false,
                defaultValue: "0",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Gender = table.Column<bool>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CreatedBy_fk = table.Column<int>(nullable: false),
                    EditedDate = table.Column<DateTime>(nullable: true),
                    LastSaleDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CreatedBy_fk",
                        column: x => x.CreatedBy_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    VendorCode = table.Column<int>(nullable: false),
                    StyleNumber = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    UPC = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    MadeIn = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: true),
                    FobPrice = table.Column<decimal>(nullable: true),
                    RetailPrice = table.Column<decimal>(nullable: true),
                    WholesalePrice = table.Column<decimal>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: true),
                    SaleStartDate = table.Column<DateTime>(nullable: true),
                    SaleEndDate = table.Column<DateTime>(nullable: true),
                    Margin = table.Column<string>(nullable: true),
                    Inventory = table.Column<int>(nullable: false),
                    LastUpdateInventory = table.Column<DateTime>(nullable: false),
                    Income = table.Column<int>(nullable: false),
                    Outcome = table.Column<int>(nullable: false),
                    OnTheWayInventory = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    HST = table.Column<decimal>(nullable: true),
                    GST = table.Column<decimal>(nullable: true),
                    QST = table.Column<decimal>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Acountsharp = table.Column<string>(nullable: true),
                    PaymentTerms = table.Column<string>(nullable: true),
                    TradeDiscountPercent = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    LeadTime = table.Column<string>(nullable: true),
                    Info1 = table.Column<string>(nullable: true),
                    Info2 = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventoryWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: true),
                    Warehouse_fk = table.Column<int>(nullable: true),
                    Inventory = table.Column<int>(nullable: true, defaultValue: 0),
                    Income = table.Column<int>(nullable: true),
                    OutCome = table.Column<int>(nullable: true),
                    OnTheWayInventory = table.Column<int>(nullable: true, defaultValue: 0),
                    Aile = table.Column<string>(nullable: true),
                    Bin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventoryWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                        column: x => x.Warehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PoNumber = table.Column<int>(nullable: false),
                    Asnumber = table.Column<int>(nullable: false),
                    Grnumber = table.Column<int>(nullable: false),
                    Vendor_fk = table.Column<int>(nullable: true),
                    PoType = table.Column<string>(nullable: true),
                    Associate = table.Column<string>(nullable: true),
                    PoTerms = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    FormSO = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: true),
                    AsnDate = table.Column<DateTime>(nullable: true),
                    GrnDate = table.Column<DateTime>(nullable: true),
                    ShipDate = table.Column<DateTime>(nullable: true),
                    CancelDate = table.Column<DateTime>(nullable: true),
                    CreateOrder = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    LastEditDate = table.Column<DateTime>(nullable: true),
                    Freight = table.Column<decimal>(nullable: true),
                    DiscountPercent = table.Column<decimal>(nullable: true),
                    Percent = table.Column<string>(nullable: true),
                    DiscountDollers = table.Column<decimal>(nullable: true),
                    Insurance = table.Column<decimal>(nullable: true),
                    CustomsDuty = table.Column<decimal>(nullable: true),
                    Handling = table.Column<decimal>(nullable: true),
                    Forwarding = table.Column<decimal>(nullable: true),
                    LandTransport = table.Column<decimal>(nullable: true),
                    Others = table.Column<decimal>(nullable: true),
                    TotalCharges = table.Column<decimal>(nullable: true),
                    PoTotal = table.Column<decimal>(nullable: true),
                    AsnTotal = table.Column<decimal>(nullable: true),
                    GrnTotal = table.Column<decimal>(nullable: true),
                    CreatedPO = table.Column<bool>(nullable: true),
                    CreatedAsn = table.Column<bool>(nullable: true),
                    CreatedGrn = table.Column<bool>(nullable: true),
                    ItemsPoCount = table.Column<int>(nullable: true),
                    ItemsAsnCount = table.Column<int>(nullable: true),
                    ItemsGrnCount = table.Column<int>(nullable: true),
                    ToWarehouse_fk = table.Column<int>(nullable: true),
                    FromWarehouse_fk = table.Column<int>(nullable: true),
                    ApprovePoUser_fk = table.Column<int>(nullable: true),
                    ApproveAsnUser_fk = table.Column<int>(nullable: true),
                    ApproveGrnUser_fk = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_ApproveAsnUser_fk",
                        column: x => x.ApproveAsnUser_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_ApproveGrnUser_fk",
                        column: x => x.ApproveGrnUser_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_ApprovePoUser_fk",
                        column: x => x.ApprovePoUser_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_FromWarehouse_fk",
                        column: x => x.FromWarehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_ToWarehouse_fk",
                        column: x => x.ToWarehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Vendors_Vendor_fk",
                        column: x => x.Vendor_fk,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<bool>(nullable: false),
                    OrderedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ShipDate = table.Column<DateTime>(nullable: true),
                    CancelDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    SalesOrderNumber = table.Column<int>(nullable: true),
                    Cashier_fk = table.Column<int>(nullable: true),
                    Customer_fk = table.Column<int>(nullable: true),
                    Warehouse_fk = table.Column<int>(nullable: true),
                    ShipMethod_fk = table.Column<int>(nullable: true),
                    Subtotal = table.Column<decimal>(nullable: false),
                    SoTotalPrice = table.Column<decimal>(nullable: false),
                    TaxArea_fk = table.Column<int>(nullable: true),
                    Tax = table.Column<decimal>(nullable: false),
                    Handling = table.Column<decimal>(nullable: false),
                    Freight = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    ShipToAddressName = table.Column<string>(nullable: true),
                    ShipToAddressNam1 = table.Column<string>(nullable: true),
                    ShipToAddressNam2 = table.Column<string>(nullable: true),
                    ShipToPostalCode = table.Column<string>(nullable: true),
                    ShipToPostalPhone1 = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Users_Cashier_fk",
                        column: x => x.Cashier_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Customers_Customer_fk",
                        column: x => x.Customer_fk,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Provinces_TaxArea_fk",
                        column: x => x.TaxArea_fk,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Warehouses_Warehouse_fk",
                        column: x => x.Warehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Po_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    PoQuantity = table.Column<int>(nullable: false, defaultValue: 0),
                    AsnQuantity = table.Column<int>(nullable: false, defaultValue: 0),
                    GrnQuantity = table.Column<int>(nullable: false, defaultValue: 0),
                    PoPrice = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    AsnPrice = table.Column<decimal>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    PoItemsPrice = table.Column<decimal>(nullable: false),
                    AsnItemsPrice = table.Column<decimal>(nullable: false),
                    Diffrent = table.Column<int>(nullable: true),
                    Alert = table.Column<bool>(nullable: true, defaultValue: false),
                    Note = table.Column<string>(nullable: true),
                    Checked = table.Column<bool>(nullable: true, defaultValue: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_PurchaseOrders_Po_fk",
                        column: x => x.Po_fk,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Products_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoItem_Products_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoItem_SaleOrders_So_fk",
                        column: x => x.So_fk,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "ID", "Acountsharp", "Address1", "Address2", "Address3", "Country", "Currency", "Email", "FirstName", "Info1", "Info2", "LastName", "LeadTime", "Name", "Note", "PaymentTerms", "Phone1", "Phone2", "PostalCode", "Title", "TradeDiscountPercent" },
                values: new object[] { 1, null, null, null, null, null, null, null, null, null, null, null, null, "ClubJummana", null, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "ID", "Acountsharp", "Address1", "Address2", "Address3", "Country", "Currency", "Email", "FirstName", "Info1", "Info2", "LastName", "LeadTime", "Name", "Note", "PaymentTerms", "Phone1", "Phone2", "PostalCode", "Title", "TradeDiscountPercent" },
                values: new object[] { 2, null, null, null, null, null, null, null, null, null, null, null, null, "Anzir", null, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "ID", "Acountsharp", "Address1", "Address2", "Address3", "Country", "Currency", "Email", "FirstName", "Info1", "Info2", "LastName", "LeadTime", "Name", "Note", "PaymentTerms", "Phone1", "Phone2", "PostalCode", "Title", "TradeDiscountPercent" },
                values: new object[] { 3, null, null, null, null, null, null, null, null, null, null, null, null, "Noman", null, null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy_fk",
                table: "Customers",
                column: "CreatedBy_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Po_fk",
                table: "Items",
                column: "Po_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProductMaster_fk",
                table: "Items",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryWarehouses_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryWarehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses",
                column: "Warehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApproveAsnUser_fk",
                table: "PurchaseOrders",
                column: "ApproveAsnUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApproveGrnUser_fk",
                table: "PurchaseOrders",
                column: "ApproveGrnUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApprovePoUser_fk",
                table: "PurchaseOrders",
                column: "ApprovePoUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_FromWarehouse_fk",
                table: "PurchaseOrders",
                column: "FromWarehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ToWarehouse_fk",
                table: "PurchaseOrders",
                column: "ToWarehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Vendor_fk",
                table: "PurchaseOrders",
                column: "Vendor_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_Cashier_fk",
                table: "SaleOrders",
                column: "Cashier_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_Customer_fk",
                table: "SaleOrders",
                column: "Customer_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_TaxArea_fk",
                table: "SaleOrders",
                column: "TaxArea_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_Warehouse_fk",
                table: "SaleOrders",
                column: "Warehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SoItem_ProductMaster_fk",
                table: "SoItem",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SoItem_So_fk",
                table: "SoItem",
                column: "So_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ProductInventoryWarehouses");

            migrationBuilder.DropTable(
                name: "SoItem");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldDefaultValue: "0");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
