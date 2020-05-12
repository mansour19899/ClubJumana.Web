﻿// <auto-generated />
using System;
using ClubJumana.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClubJumana.DataLayer.Migrations
{
    [DbContext(typeof(JummanaContext))]
    partial class JummanaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy_fk")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EditedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastSaleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy_fk");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool?>("Alert")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("AsnItemsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AsnPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AsnQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool?>("Checked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Diffrent")
                        .HasColumnType("int");

                    b.Property<int>("GrnQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PoItemsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PoPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("PoQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Po_fk")
                        .HasColumnType("int");

                    b.Property<int>("ProductMaster_fk")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Po_fk");

                    b.HasIndex("ProductMaster_fk");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.ProductInventoryWarehouse", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Aile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Income")
                        .HasColumnType("int");

                    b.Property<int?>("Inventory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("OnTheWayInventory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("OutCome")
                        .HasColumnType("int");

                    b.Property<int?>("ProductMaster_fk")
                        .HasColumnType("int");

                    b.Property<int?>("Warehouse_fk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductMaster_fk");

                    b.HasIndex("Warehouse_fk");

                    b.ToTable("ProductInventoryWarehouses");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.ProductMaster", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("FobPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Income")
                        .HasColumnType("int");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateInventory")
                        .HasColumnType("datetime2");

                    b.Property<string>("MadeIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Margin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OnTheWayInventory")
                        .HasColumnType("int");

                    b.Property<int>("Outcome")
                        .HasColumnType("int");

                    b.Property<decimal?>("RetailPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SaleEndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("SaleStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VendorCode")
                        .HasColumnType("int");

                    b.Property<decimal?>("WholesalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<decimal?>("GST")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("HST")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("QST")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ApproveAsnUser_fk")
                        .HasColumnType("int");

                    b.Property<int?>("ApproveGrnUser_fk")
                        .HasColumnType("int");

                    b.Property<int?>("ApprovePoUser_fk")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AsnDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("AsnTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Asnumber")
                        .HasColumnType("int");

                    b.Property<string>("Associate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool?>("CreatedAsn")
                        .HasColumnType("bit");

                    b.Property<bool?>("CreatedGrn")
                        .HasColumnType("bit");

                    b.Property<bool?>("CreatedPO")
                        .HasColumnType("bit");

                    b.Property<decimal?>("CustomsDuty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountDollers")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FormSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Forwarding")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("FromWarehouse_fk")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GrnDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("GrnTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Grnumber")
                        .HasColumnType("int");

                    b.Property<decimal?>("Handling")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Insurance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ItemsAsnCount")
                        .HasColumnType("int");

                    b.Property<int?>("ItemsGrnCount")
                        .HasColumnType("int");

                    b.Property<int?>("ItemsPoCount")
                        .HasColumnType("int");

                    b.Property<decimal?>("LandTransport")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Others")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PoNumber")
                        .HasColumnType("int");

                    b.Property<string>("PoTerms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PoType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime?>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ToWarehouse_fk")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalCharges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Vendor_fk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApproveAsnUser_fk");

                    b.HasIndex("ApproveGrnUser_fk");

                    b.HasIndex("ApprovePoUser_fk");

                    b.HasIndex("FromWarehouse_fk");

                    b.HasIndex("ToWarehouse_fk");

                    b.HasIndex("Vendor_fk");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.SaleOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("Cashier_fk")
                        .HasColumnType("int");

                    b.Property<int?>("Customer_fk")
                        .HasColumnType("int");

                    b.Property<decimal>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Handling")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("OrderedDate")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SalesOrderNumber")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ShipMethod_fk")
                        .HasColumnType("int");

                    b.Property<string>("ShipToAddressNam1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipToAddressNam2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipToAddressName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipToPostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipToPostalPhone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SoTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TaxArea_fk")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.Property<int?>("Warehouse_fk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Cashier_fk");

                    b.HasIndex("Customer_fk");

                    b.HasIndex("TaxArea_fk");

                    b.HasIndex("Warehouse_fk");

                    b.ToTable("SaleOrders");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.SoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductMaster_fk")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("So_fk")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductMaster_fk");

                    b.HasIndex("So_fk");

                    b.ToTable("SoItem");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.User.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("ExpireInvitationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("SendInvitationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserRegisterWithInvitation_fk")
                        .HasColumnType("int");

                    b.Property<int>("UserSendInvitation_fk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRegisterWithInvitation_fk")
                        .IsUnique()
                        .HasFilter("[UserRegisterWithInvitation_fk] IS NOT NULL");

                    b.HasIndex("UserSendInvitation_fk");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Users.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAvatar")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Users.UserRole", b =>
                {
                    b.Property<int>("UR_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UR_Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<string>("Acountsharp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Info1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Info2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LeadTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnName("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentTerms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TradeDiscountPercent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ClubJummana"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Anzir"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Noman"
                        });
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Customer", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "User")
                        .WithMany("Customers")
                        .HasForeignKey("CreatedBy_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Item", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.PurchaseOrder", "PurchaseOrder")
                        .WithMany("Items")
                        .HasForeignKey("Po_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClubJumana.DataLayer.Entities.ProductMaster", "ProductMaster")
                        .WithMany("Items")
                        .HasForeignKey("ProductMaster_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.ProductInventoryWarehouse", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.ProductMaster", "ProductMaster")
                        .WithMany("ProductInventoryWarehouses")
                        .HasForeignKey("ProductMaster_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Warehouse", "Warehouse")
                        .WithMany("ProductInventoryWarehouses")
                        .HasForeignKey("Warehouse_fk");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.PurchaseOrder", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "UserCreateAsn")
                        .WithMany("CreateAsn")
                        .HasForeignKey("ApproveAsnUser_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "UserCreateGrn")
                        .WithMany("CreateGrn")
                        .HasForeignKey("ApproveGrnUser_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "UserCreatePo")
                        .WithMany("CreatePo")
                        .HasForeignKey("ApprovePoUser_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Warehouse", "FromWarehouse")
                        .WithMany("POFromWarehouse")
                        .HasForeignKey("FromWarehouse_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Warehouse", "ToWarehouse")
                        .WithMany("PoToWareHose")
                        .HasForeignKey("ToWarehouse_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Vendor", "Vendor")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("Vendor_fk");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.SaleOrder", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "User")
                        .WithMany("SaleOrders")
                        .HasForeignKey("Cashier_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Customer", "Customer")
                        .WithMany("SaleOrders")
                        .HasForeignKey("Customer_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Province", "TaxArea")
                        .WithMany("SaleOrders")
                        .HasForeignKey("TaxArea_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Warehouse", "Warehouse")
                        .WithMany("SaleOrders")
                        .HasForeignKey("Warehouse_fk");
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.SoItem", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.ProductMaster", "ProductMaster")
                        .WithMany("SoItems")
                        .HasForeignKey("ProductMaster_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClubJumana.DataLayer.Entities.SaleOrder", "SaleOrder")
                        .WithMany("SoItems")
                        .HasForeignKey("So_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.User.Invitation", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "UserRegisterWithInvitation")
                        .WithOne("RegisterWithInvitation")
                        .HasForeignKey("ClubJumana.DataLayer.Entities.User.Invitation", "UserRegisterWithInvitation_fk");

                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "UserSendInvitation")
                        .WithMany("SendInvitations")
                        .HasForeignKey("UserSendInvitation_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClubJumana.DataLayer.Entities.Users.UserRole", b =>
                {
                    b.HasOne("ClubJumana.DataLayer.Entities.Users.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClubJumana.DataLayer.Entities.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
