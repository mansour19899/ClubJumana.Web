using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ClubJumana.DataLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "barcodes",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 12, nullable: false),
                    BarcodeNumber = table.Column<string>(maxLength: 15, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barcodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sku_code = table.Column<string>(nullable: true),
                    StyleNum_code = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "colours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PantoneNumber = table.Column<string>(maxLength: 40, nullable: true),
                    Code = table.Column<string>(maxLength: 5, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Duty = table.Column<decimal>(type: "decimal(5, 2)", nullable: true),
                    ExChangeRate = table.Column<decimal>(type: "decimal(14,5)", nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    DigitalCode = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    HST = table.Column<decimal>(type: "decimal(7,4)", nullable: true),
                    GST = table.Column<decimal>(type: "decimal(7,4)", nullable: true),
                    QST = table.Column<decimal>(type: "decimal(7,4)", nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleTitle = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subcategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tablesversion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    RowVersion = table.Column<long>(nullable: false),
                    NeedToUpdate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tablesversion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    DueDateCalculation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
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
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    Address1 = table.Column<string>(maxLength: 100, nullable: true),
                    Address2 = table.Column<string>(maxLength: 100, nullable: true),
                    Address3 = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 20, nullable: true),
                    Country = table.Column<string>(maxLength: 20, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(maxLength: 40, nullable: true),
                    LastName = table.Column<string>(maxLength: 40, nullable: true),
                    Phone1 = table.Column<string>(maxLength: 20, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 40, nullable: true),
                    Acountsharp = table.Column<string>(maxLength: 20, nullable: true),
                    PaymentTerms = table.Column<string>(nullable: true),
                    TradeDiscountPercent = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    LeadTime = table.Column<string>(nullable: true),
                    Info1 = table.Column<string>(nullable: true),
                    Info2 = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 20, nullable: true),
                    Manufacture = table.Column<string>(maxLength: 100, nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 40, nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    StateProvinceRegion = table.Column<string>(nullable: true),
                    ZipPostlCode = table.Column<string>(maxLength: 20, nullable: true),
                    CountryFK = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 40, nullable: true),
                    FAX = table.Column<string>(maxLength: 40, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_companies_countries_CountryFK",
                        column: x => x.CountryFK,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categoriessubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CategoryFK = table.Column<int>(nullable: false),
                    SubCategoryFK = table.Column<int>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoriessubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categoriessubcategories_categories_CategoryFK",
                        column: x => x.CategoryFK,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_categoriessubcategories_subcategories_SubCategoryFK",
                        column: x => x.SubCategoryFK,
                        principalTable: "subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Gender = table.Column<bool>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactLastName = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(maxLength: 30, nullable: true),
                    Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    FaxNo = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(maxLength: 30, nullable: true),
                    Address2 = table.Column<string>(maxLength: 30, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayBillAddress = table.Column<string>(nullable: true),
                    DisplayShipAddress = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(maxLength: 15, nullable: true),
                    BalanceLCY = table.Column<decimal>(nullable: false),
                    BalanceDueLCY = table.Column<decimal>(nullable: false),
                    CreditLimitLCY = table.Column<decimal>(nullable: false),
                    TotalSales = table.Column<decimal>(nullable: false),
                    CostsLCY = table.Column<decimal>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CreatedBy_fk = table.Column<int>(nullable: false),
                    ProvinceFK = table.Column<int>(nullable: true),
                    CountryFK = table.Column<int>(nullable: true),
                    LastSaleDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_countries_CountryFK",
                        column: x => x.CountryFK,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customers_users_CreatedBy_fk",
                        column: x => x.CreatedBy_fk,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customers_provinces_ProvinceFK",
                        column: x => x.ProvinceFK,
                        principalTable: "provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "invitations",
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
                    table.PrimaryKey("PK_invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invitations_users_UserRegisterWithInvitation_fk",
                        column: x => x.UserRegisterWithInvitation_fk,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_invitations_users_UserSendInvitation_fk",
                        column: x => x.UserSendInvitation_fk,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userroles",
                columns: table => new
                {
                    UR_Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userroles", x => x.UR_Id);
                    table.ForeignKey(
                        name: "FK_userroles_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userroles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchaseorders",
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
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchaseorders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchaseorders_users_ApproveAsnUser_fk",
                        column: x => x.ApproveAsnUser_fk,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchaseorders_users_ApproveGrnUser_fk",
                        column: x => x.ApproveGrnUser_fk,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchaseorders_users_ApprovePoUser_fk",
                        column: x => x.ApprovePoUser_fk,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchaseorders_warehouses_FromWarehouse_fk",
                        column: x => x.FromWarehouse_fk,
                        principalTable: "warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchaseorders_warehouses_ToWarehouse_fk",
                        column: x => x.ToWarehouse_fk,
                        principalTable: "warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchaseorders_vendors_Vendor_fk",
                        column: x => x.Vendor_fk,
                        principalTable: "vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    StyleNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ProductTittle = table.Column<string>(maxLength: 100, nullable: true),
                    BrandFK = table.Column<int>(nullable: true),
                    MaterialFK = table.Column<int>(nullable: true),
                    CompanyFK = table.Column<int>(nullable: true),
                    CountryOfOrginFK = table.Column<int>(nullable: true),
                    DescribeMaterial = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_brands_BrandFK",
                        column: x => x.BrandFK,
                        principalTable: "brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_products_companies_CompanyFK",
                        column: x => x.CompanyFK,
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_products_countries_CountryOfOrginFK",
                        column: x => x.CountryOfOrginFK,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_products_materials_MaterialFK",
                        column: x => x.MaterialFK,
                        principalTable: "materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "producttypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CategorysubcategoreisFK = table.Column<int>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producttypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_producttypes_categoriessubcategories_CategorysubcategoreisFK",
                        column: x => x.CategorysubcategoreisFK,
                        principalTable: "categoriessubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saleorders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<bool>(nullable: false),
                    SoDate = table.Column<DateTime>(nullable: true),
                    ExpriationDate = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    ShipDate = table.Column<DateTime>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: true),
                    InvoiceNumber = table.Column<int>(nullable: true),
                    Cashier_fk = table.Column<int>(nullable: true),
                    Customer_fk = table.Column<int>(nullable: true),
                    Warehouse_fk = table.Column<int>(nullable: true),
                    term_fk = table.Column<int>(nullable: true),
                    ShipMethod_fk = table.Column<int>(nullable: true),
                    TermPercent = table.Column<decimal>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    SoTotalPrice = table.Column<decimal>(nullable: false),
                    TaxArea_fk = table.Column<int>(nullable: true),
                    Tax = table.Column<decimal>(nullable: false),
                    Handling = table.Column<decimal>(nullable: false),
                    Freight = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    PoNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ShipVia = table.Column<string>(nullable: true),
                    TrackingNo = table.Column<string>(nullable: true),
                    BillingAddress = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    MessageOnInvoice = table.Column<string>(nullable: true),
                    MessageOnStatment = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saleorders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_saleorders_users_Cashier_fk",
                        column: x => x.Cashier_fk,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_saleorders_customers_Customer_fk",
                        column: x => x.Customer_fk,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_saleorders_provinces_TaxArea_fk",
                        column: x => x.TaxArea_fk,
                        principalTable: "provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_saleorders_warehouses_Warehouse_fk",
                        column: x => x.Warehouse_fk,
                        principalTable: "warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_saleorders_terms_term_fk",
                        column: x => x.term_fk,
                        principalTable: "terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "variants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Sku = table.Column<string>(maxLength: 20, nullable: true),
                    ProductFK = table.Column<int>(nullable: true),
                    ColourFK = table.Column<int>(nullable: true),
                    BarcodeFK = table.Column<int>(nullable: true),
                    ProductTypeFK = table.Column<int>(nullable: true),
                    FobPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    WholesaleA = table.Column<decimal>(nullable: true),
                    WholesaleB = table.Column<decimal>(nullable: true),
                    RetailPrice = table.Column<decimal>(nullable: true),
                    Width = table.Column<decimal>(nullable: true),
                    length = table.Column<decimal>(nullable: true),
                    Size = table.Column<string>(maxLength: 20, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Data1 = table.Column<string>(maxLength: 100, nullable: true),
                    Data2 = table.Column<string>(maxLength: 100, nullable: true),
                    Data3 = table.Column<string>(maxLength: 100, nullable: true),
                    Data4 = table.Column<string>(maxLength: 100, nullable: true),
                    Data5 = table.Column<string>(maxLength: 100, nullable: true),
                    Data6 = table.Column<string>(maxLength: 100, nullable: true),
                    IsStar = table.Column<bool>(nullable: false),
                    LastDateEdited = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_variants_barcodes_BarcodeFK",
                        column: x => x.BarcodeFK,
                        principalTable: "barcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_variants_colours_ColourFK",
                        column: x => x.ColourFK,
                        principalTable: "colours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_variants_products_ProductFK",
                        column: x => x.ProductFK,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_variants_producttypes_ProductTypeFK",
                        column: x => x.ProductTypeFK,
                        principalTable: "producttypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "refunds",
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
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refunds_saleorders_SaleOrder_fk",
                        column: x => x.SaleOrder_fk,
                        principalTable: "saleorders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ImageName = table.Column<string>(maxLength: 20, nullable: true),
                    VariantFK = table.Column<int>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_images_variants_VariantFK",
                        column: x => x.VariantFK,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productmasters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    VendorCode = table.Column<int>(nullable: false),
                    StyleNumber = table.Column<string>(maxLength: 16, nullable: true),
                    VariantFK = table.Column<int>(nullable: false),
                    SKU = table.Column<string>(maxLength: 16, nullable: true),
                    UPC = table.Column<string>(maxLength: 12, nullable: true),
                    ITF14Inner = table.Column<string>(maxLength: 14, nullable: true),
                    ITF14Master = table.Column<string>(maxLength: 14, nullable: true),
                    Size = table.Column<string>(maxLength: 15, nullable: true),
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
                    PackCount = table.Column<int>(nullable: false),
                    StockOnHand = table.Column<int>(nullable: false),
                    GoodsReserved = table.Column<int>(nullable: false),
                    Transit = table.Column<int>(nullable: false),
                    InnersCount = table.Column<int>(nullable: false),
                    CartonsCount = table.Column<int>(nullable: false),
                    RefundQuantity = table.Column<int>(nullable: true),
                    LastUpdateInventory = table.Column<DateTime>(nullable: false),
                    Income = table.Column<int>(nullable: false),
                    Outcome = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productmasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productmasters_variants_VariantFK",
                        column: x => x.VariantFK,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "items",
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
                    GrnPrice = table.Column<decimal>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    PoItemsPrice = table.Column<decimal>(nullable: false),
                    AsnItemsPrice = table.Column<decimal>(nullable: false),
                    GrnItemsPrice = table.Column<decimal>(nullable: false),
                    Diffrent = table.Column<int>(nullable: false),
                    Alert = table.Column<bool>(nullable: true, defaultValue: false),
                    Note = table.Column<string>(nullable: true),
                    Checked = table.Column<bool>(nullable: true, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_items_purchaseorders_Po_fk",
                        column: x => x.Po_fk,
                        principalTable: "purchaseorders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_items_productmasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "productmasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productinventorywarehouses",
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
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productinventorywarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productinventorywarehouses_productmasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "productmasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productinventorywarehouses_warehouses_Warehouse_fk",
                        column: x => x.Warehouse_fk,
                        principalTable: "warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refunditems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Refund_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refunditems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refunditems_productmasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "productmasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_refunditems_refunds_Refund_fk",
                        column: x => x.Refund_fk,
                        principalTable: "refunds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "soitems",
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
                    PriceTerm = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_soitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_soitems_productmasters_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "productmasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_soitems_saleorders_So_fk",
                        column: x => x.So_fk,
                        principalTable: "saleorders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tablesversion",
                columns: new[] { "Id", "Name", "NeedToUpdate", "RowVersion" },
                values: new object[,]
                {
                    { 1, "Barcodes", false, 0L },
                    { 2, "Colours", false, 0L },
                    { 3, "Brands", false, 0L },
                    { 4, "Countries", false, 0L },
                    { 5, "Materials", false, 0L },
                    { 6, "Companies", false, 0L },
                    { 7, "Products", false, 0L },
                    { 8, "Categories", false, 0L },
                    { 9, "SubCategories", false, 0L },
                    { 10, "CategoriesSubCategories", false, 0L },
                    { 11, "ProductTypes", false, 0L },
                    { 12, "Varaints", false, 0L },
                    { 13, "Images", false, 0L }
                });

            migrationBuilder.InsertData(
                table: "vendors",
                columns: new[] { "ID", "Acountsharp", "Address1", "Address2", "Address3", "Country", "Currency", "Email", "FirstName", "Info1", "Info2", "LastName", "LeadTime", "Name", "Note", "PaymentTerms", "Phone1", "Phone2", "PostalCode", "Title", "TradeDiscountPercent" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, null, null, null, null, null, null, null, "ClubJummana", null, null, null, null, null, null, null },
                    { 2, null, null, null, null, null, null, null, null, null, null, null, null, "Anzir", null, null, null, null, null, null, null },
                    { 3, null, null, null, null, null, null, null, null, null, null, null, null, "Noman", null, null, null, null, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_categoriessubcategories_CategoryFK",
                table: "categoriessubcategories",
                column: "CategoryFK");

            migrationBuilder.CreateIndex(
                name: "IX_categoriessubcategories_SubCategoryFK",
                table: "categoriessubcategories",
                column: "SubCategoryFK");

            migrationBuilder.CreateIndex(
                name: "IX_companies_CountryFK",
                table: "companies",
                column: "CountryFK");

            migrationBuilder.CreateIndex(
                name: "IX_customers_CountryFK",
                table: "customers",
                column: "CountryFK");

            migrationBuilder.CreateIndex(
                name: "IX_customers_CreatedBy_fk",
                table: "customers",
                column: "CreatedBy_fk");

            migrationBuilder.CreateIndex(
                name: "IX_customers_ProvinceFK",
                table: "customers",
                column: "ProvinceFK");

            migrationBuilder.CreateIndex(
                name: "IX_images_VariantFK",
                table: "images",
                column: "VariantFK");

            migrationBuilder.CreateIndex(
                name: "IX_invitations_UserRegisterWithInvitation_fk",
                table: "invitations",
                column: "UserRegisterWithInvitation_fk",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invitations_UserSendInvitation_fk",
                table: "invitations",
                column: "UserSendInvitation_fk");

            migrationBuilder.CreateIndex(
                name: "IX_items_Po_fk",
                table: "items",
                column: "Po_fk");

            migrationBuilder.CreateIndex(
                name: "IX_items_ProductMaster_fk",
                table: "items",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_productinventorywarehouses_ProductMaster_fk",
                table: "productinventorywarehouses",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_productinventorywarehouses_Warehouse_fk",
                table: "productinventorywarehouses",
                column: "Warehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_productmasters_VariantFK",
                table: "productmasters",
                column: "VariantFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_BrandFK",
                table: "products",
                column: "BrandFK");

            migrationBuilder.CreateIndex(
                name: "IX_products_CompanyFK",
                table: "products",
                column: "CompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_products_CountryOfOrginFK",
                table: "products",
                column: "CountryOfOrginFK");

            migrationBuilder.CreateIndex(
                name: "IX_products_MaterialFK",
                table: "products",
                column: "MaterialFK");

            migrationBuilder.CreateIndex(
                name: "IX_producttypes_CategorysubcategoreisFK",
                table: "producttypes",
                column: "CategorysubcategoreisFK");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseorders_ApproveAsnUser_fk",
                table: "purchaseorders",
                column: "ApproveAsnUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseorders_ApproveGrnUser_fk",
                table: "purchaseorders",
                column: "ApproveGrnUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseorders_ApprovePoUser_fk",
                table: "purchaseorders",
                column: "ApprovePoUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseorders_FromWarehouse_fk",
                table: "purchaseorders",
                column: "FromWarehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseorders_ToWarehouse_fk",
                table: "purchaseorders",
                column: "ToWarehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseorders_Vendor_fk",
                table: "purchaseorders",
                column: "Vendor_fk");

            migrationBuilder.CreateIndex(
                name: "IX_refunditems_ProductMaster_fk",
                table: "refunditems",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_refunditems_Refund_fk",
                table: "refunditems",
                column: "Refund_fk");

            migrationBuilder.CreateIndex(
                name: "IX_refunds_SaleOrder_fk",
                table: "refunds",
                column: "SaleOrder_fk");

            migrationBuilder.CreateIndex(
                name: "IX_saleorders_Cashier_fk",
                table: "saleorders",
                column: "Cashier_fk");

            migrationBuilder.CreateIndex(
                name: "IX_saleorders_Customer_fk",
                table: "saleorders",
                column: "Customer_fk");

            migrationBuilder.CreateIndex(
                name: "IX_saleorders_TaxArea_fk",
                table: "saleorders",
                column: "TaxArea_fk");

            migrationBuilder.CreateIndex(
                name: "IX_saleorders_Warehouse_fk",
                table: "saleorders",
                column: "Warehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_saleorders_term_fk",
                table: "saleorders",
                column: "term_fk");

            migrationBuilder.CreateIndex(
                name: "IX_soitems_ProductMaster_fk",
                table: "soitems",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_soitems_So_fk",
                table: "soitems",
                column: "So_fk");

            migrationBuilder.CreateIndex(
                name: "IX_userroles_RoleId",
                table: "userroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_userroles_UserId",
                table: "userroles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_variants_BarcodeFK",
                table: "variants",
                column: "BarcodeFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_variants_ColourFK",
                table: "variants",
                column: "ColourFK");

            migrationBuilder.CreateIndex(
                name: "IX_variants_ProductFK",
                table: "variants",
                column: "ProductFK");

            migrationBuilder.CreateIndex(
                name: "IX_variants_ProductTypeFK",
                table: "variants",
                column: "ProductTypeFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "invitations");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "productinventorywarehouses");

            migrationBuilder.DropTable(
                name: "refunditems");

            migrationBuilder.DropTable(
                name: "soitems");

            migrationBuilder.DropTable(
                name: "tablesversion");

            migrationBuilder.DropTable(
                name: "userroles");

            migrationBuilder.DropTable(
                name: "purchaseorders");

            migrationBuilder.DropTable(
                name: "refunds");

            migrationBuilder.DropTable(
                name: "productmasters");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "vendors");

            migrationBuilder.DropTable(
                name: "saleorders");

            migrationBuilder.DropTable(
                name: "variants");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "warehouses");

            migrationBuilder.DropTable(
                name: "terms");

            migrationBuilder.DropTable(
                name: "barcodes");

            migrationBuilder.DropTable(
                name: "colours");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "producttypes");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "categoriessubcategories");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "subcategories");
        }
    }
}
