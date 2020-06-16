using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class mysqlstartqq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barcodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BarcodeNumber = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sku_code = table.Column<string>(nullable: true),
                    StyleNum_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PantoneNumber = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Duty = table.Column<decimal>(type: "decimal(5, 2)", nullable: true),
                    ExChangeRate = table.Column<decimal>(type: "decimal(14,5)", nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    DigitalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMasters",
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
                    StockOnHand = table.Column<int>(nullable: false),
                    GoodsReserved = table.Column<int>(nullable: false),
                    RefundQuantity = table.Column<int>(nullable: true),
                    LastUpdateInventory = table.Column<DateTime>(nullable: false),
                    Income = table.Column<int>(nullable: false),
                    Outcome = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMasters", x => x.Id);
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
                    Active = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleTitle = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    ActiveCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UserAvatar = table.Column<string>(maxLength: 200, nullable: true),
                    RegisterDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
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
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Manufacture = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    StateProvinceRegion = table.Column<string>(nullable: true),
                    ZipPostlCode = table.Column<string>(nullable: true),
                    CountryFK = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    FAX = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_CountryFK",
                        column: x => x.CountryFK,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CategoryFK = table.Column<int>(nullable: false),
                    SubCategoryFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriesSubCategories_Categories_CategoryFK",
                        column: x => x.CategoryFK,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriesSubCategories_SubCategories_SubCategoryFK",
                        column: x => x.SubCategoryFK,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Active = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
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
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    ActiveCode = table.Column<string>(maxLength: 50, nullable: true),
                    UserSendInvitation_fk = table.Column<int>(nullable: false),
                    UserRegisterWithInvitation_fk = table.Column<int>(nullable: true),
                    ExpireInvitationDate = table.Column<DateTime>(nullable: false),
                    SendInvitationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_UserRegisterWithInvitation_fk",
                        column: x => x.UserRegisterWithInvitation_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_UserSendInvitation_fk",
                        column: x => x.UserSendInvitation_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UR_Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UR_Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventoryWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Warehouse_fk = table.Column<int>(nullable: false),
                    Inventory = table.Column<int>(nullable: true, defaultValue: 0),
                    Income = table.Column<int>(nullable: true),
                    OutCome = table.Column<int>(nullable: true),
                    OnTheWayInventory = table.Column<int>(nullable: true, defaultValue: 0),
                    RefundQuantity = table.Column<int>(nullable: true),
                    Aile = table.Column<string>(nullable: true),
                    Bin = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventoryWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInventoryWarehouses_ProductMasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "ProductMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                        column: x => x.Warehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CreateOrder = table.Column<DateTime>(nullable: true),
                    LastEditDate = table.Column<DateTime>(nullable: true),
                    Freight = table.Column<decimal>(nullable: false),
                    DiscountPercent = table.Column<decimal>(nullable: false),
                    Percent = table.Column<string>(nullable: true),
                    DiscountDollers = table.Column<decimal>(nullable: false),
                    Insurance = table.Column<decimal>(nullable: false),
                    CustomsDuty = table.Column<decimal>(nullable: false),
                    Handling = table.Column<decimal>(nullable: false),
                    Forwarding = table.Column<decimal>(nullable: false),
                    LandTransport = table.Column<decimal>(nullable: false),
                    Others = table.Column<decimal>(nullable: false),
                    TotalCharges = table.Column<decimal>(nullable: false),
                    PoSubtotal = table.Column<decimal>(nullable: true),
                    AsnSubtotal = table.Column<decimal>(nullable: true),
                    GrnSubtotal = table.Column<decimal>(nullable: true),
                    PoTotal = table.Column<decimal>(nullable: true),
                    AsnTotal = table.Column<decimal>(nullable: true),
                    GrnTotal = table.Column<decimal>(nullable: true),
                    CreatedPO = table.Column<bool>(nullable: false),
                    CreatedAsn = table.Column<bool>(nullable: false),
                    CreatedGrn = table.Column<bool>(nullable: false),
                    ItemsPoCount = table.Column<int>(nullable: true),
                    ItemsAsnCount = table.Column<int>(nullable: true),
                    ItemsGrnCount = table.Column<int>(nullable: true),
                    ToWarehouse_fk = table.Column<int>(nullable: true),
                    FromWarehouse_fk = table.Column<int>(nullable: true),
                    ApprovePoUser_fk = table.Column<int>(nullable: true),
                    ApproveAsnUser_fk = table.Column<int>(nullable: true),
                    ApproveGrnUser_fk = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    StyleNumber = table.Column<string>(nullable: true),
                    ProductTittle = table.Column<string>(nullable: true),
                    BrandFK = table.Column<int>(nullable: true),
                    MaterialFK = table.Column<int>(nullable: true),
                    CompanyFK = table.Column<int>(nullable: true),
                    CountryOfOrginFK = table.Column<int>(nullable: true),
                    DescribeMaterial = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandFK",
                        column: x => x.BrandFK,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyFK",
                        column: x => x.CompanyFK,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Countries_CountryOfOrginFK",
                        column: x => x.CountryOfOrginFK,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Materials_MaterialFK",
                        column: x => x.MaterialFK,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CategorysubcategoreisFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypes_CategoriesSubCategories_CategorysubcategoreisFK",
                        column: x => x.CategorysubcategoreisFK,
                        principalTable: "CategoriesSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<bool>(nullable: false),
                    OrderedDate = table.Column<DateTime>(nullable: true),
                    ShipDate = table.Column<DateTime>(nullable: true),
                    CancelDate = table.Column<DateTime>(nullable: true),
                    SalesOrderNumber = table.Column<int>(nullable: true),
                    InvoiceNumber = table.Column<int>(nullable: true),
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
                    Quantity = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
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
                    Diffrent = table.Column<int>(nullable: false),
                    Alert = table.Column<bool>(nullable: true, defaultValue: false),
                    Note = table.Column<string>(nullable: true),
                    Checked = table.Column<bool>(nullable: true, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
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
                        name: "FK_Items_ProductMasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "ProductMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Sku = table.Column<string>(nullable: true),
                    ProductFK = table.Column<int>(nullable: true),
                    ColourFK = table.Column<int>(nullable: true),
                    BarcodeFK = table.Column<int>(nullable: true),
                    ProductTypeFK = table.Column<int>(nullable: true),
                    FobPrice = table.Column<float>(nullable: true),
                    WholesaleA = table.Column<float>(nullable: true),
                    WholesaleB = table.Column<float>(nullable: true),
                    RetailPrice = table.Column<float>(nullable: true),
                    Width = table.Column<float>(nullable: true),
                    length = table.Column<float>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Data1 = table.Column<string>(nullable: true),
                    Data2 = table.Column<string>(nullable: true),
                    Data3 = table.Column<string>(nullable: true),
                    Data4 = table.Column<string>(nullable: true),
                    Data5 = table.Column<string>(nullable: true),
                    Data6 = table.Column<string>(nullable: true),
                    LastDateEdited = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Barcodes_BarcodeFK",
                        column: x => x.BarcodeFK,
                        principalTable: "Barcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_Colours_ColourFK",
                        column: x => x.ColourFK,
                        principalTable: "Colours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductFK",
                        column: x => x.ProductFK,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_ProductTypes_ProductTypeFK",
                        column: x => x.ProductTypeFK,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RefundDate = table.Column<DateTime>(nullable: true),
                    RefundNumber = table.Column<int>(nullable: true),
                    SaleOrder_fk = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: true),
                    RefundTotalPrice = table.Column<decimal>(nullable: false),
                    SubtotalPrice = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refunds_SaleOrders_SaleOrder_fk",
                        column: x => x.SaleOrder_fk,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    So_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    QuantityRefunded = table.Column<int>(nullable: true, defaultValue: 0),
                    IsAbaleToRefund = table.Column<bool>(nullable: true, defaultValue: true),
                    Price = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoItems_ProductMasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "ProductMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoItems_SaleOrders_So_fk",
                        column: x => x.So_fk,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    VariantFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Variants_VariantFK",
                        column: x => x.VariantFK,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Refund_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundItems_ProductMasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "ProductMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundItems_Refunds_Refund_fk",
                        column: x => x.Refund_fk,
                        principalTable: "Refunds",
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
                name: "IX_CategoriesSubCategories_CategoryFK",
                table: "CategoriesSubCategories",
                column: "CategoryFK");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesSubCategories_SubCategoryFK",
                table: "CategoriesSubCategories",
                column: "SubCategoryFK");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryFK",
                table: "Companies",
                column: "CountryFK");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy_fk",
                table: "Customers",
                column: "CreatedBy_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Images_VariantFK",
                table: "Images",
                column: "VariantFK");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserRegisterWithInvitation_fk",
                table: "Invitations",
                column: "UserRegisterWithInvitation_fk",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserSendInvitation_fk",
                table: "Invitations",
                column: "UserSendInvitation_fk");

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
                name: "IX_Products_BrandFK",
                table: "Products",
                column: "BrandFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyFK",
                table: "Products",
                column: "CompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CountryOfOrginFK",
                table: "Products",
                column: "CountryOfOrginFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialFK",
                table: "Products",
                column: "MaterialFK");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_CategorysubcategoreisFK",
                table: "ProductTypes",
                column: "CategorysubcategoreisFK");

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
                name: "IX_RefundItems_ProductMaster_fk",
                table: "RefundItems",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_RefundItems_Refund_fk",
                table: "RefundItems",
                column: "Refund_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_SaleOrder_fk",
                table: "Refunds",
                column: "SaleOrder_fk");

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
                name: "IX_SoItems_ProductMaster_fk",
                table: "SoItems",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SoItems_So_fk",
                table: "SoItems",
                column: "So_fk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_BarcodeFK",
                table: "Variants",
                column: "BarcodeFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ColourFK",
                table: "Variants",
                column: "ColourFK");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductFK",
                table: "Variants",
                column: "ProductFK");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductTypeFK",
                table: "Variants",
                column: "ProductTypeFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ProductInventoryWarehouses");

            migrationBuilder.DropTable(
                name: "RefundItems");

            migrationBuilder.DropTable(
                name: "SoItems");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropTable(
                name: "ProductMasters");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Barcodes");

            migrationBuilder.DropTable(
                name: "Colours");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "CategoriesSubCategories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
