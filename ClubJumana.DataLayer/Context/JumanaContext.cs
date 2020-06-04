﻿using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.DataLayer.Context
{
   public class JummanaContext:DbContext
    {

        //public JummanaContext(DbContextOptions<JummanaContext> options):base(options)
        //{
            
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCore-smm35;Trusted_Connection=True");
        }
        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<ProductMaster> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductInventoryWarehouse> ProductInventoryWarehouses { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SoItem> SoItems { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<RefundItem> RefundItems { get; set; }
        public DbSet<Province> Provinces { get; set; }


        public DbSet<Product> Productw { get; set; }
        public DbSet<Towel> Towels { get; set; }
        public DbSet<Beding> Bedings { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<CategoriesSubCategory> CategoriesSubCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //----------------------------------- Items---------------------------------------

            modelBuilder.Entity<Item>().Property(b => b.Id).ValueGeneratedNever();
            modelBuilder.Entity<Item>().Property(b => b.PoQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.AsnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.GrnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.PoPrice).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.PoQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Checked).HasDefaultValue(false);
            modelBuilder.Entity<Item>().Property(b => b.Alert).HasDefaultValue(false);
            modelBuilder.Entity<Item>().Ignore(p => p.Price);
            modelBuilder.Entity<Item>().Ignore(p => p.TotalItemPrice);
            modelBuilder.Entity<Item>().Property(a => a.RowVersion).IsRowVersion();
            //modelBuilder.Entity<Item>().HasData(new Item
            //{
            //    Id = 100, Po_fk = 1, ProductMaster_fk = 1, PoQuantity = 0, AsnQuantity = 0, GrnQuantity = 0,
            //    PoPrice = 0, AsnPrice = 0, Cost = 0, PoItemsPrice = 0, AsnItemsPrice = 0, Alert = false, Checked = false
            //});

            // modelBuilder.Entity<Item>().Property(b => b.Note).HasColumnType("text").HasDefaultValue("Note :  ");

            modelBuilder.Entity<Item>()
                .HasOne<ProductMaster>(s => s.ProductMaster)
                .WithMany(g => g.Items)
                .HasForeignKey(s => s.ProductMaster_fk);

            modelBuilder.Entity<Item>()
                .HasOne<PurchaseOrder>(s => s.PurchaseOrder)
                .WithMany(g => g.Items)
                .HasForeignKey(s => s.Po_fk);



            //----------------------------------- Product Master------------------------------


            modelBuilder.Entity<ProductMaster>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.RowVersion).IsRowVersion();

            });

            //----------------------------------- Vendor--------------------------------------

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Note).HasColumnName("Note");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.RowVersion).IsRowVersion();
                entity.HasData(new Vendor { Id = 1, Name = "ClubJummana" });
                entity.HasData(new Vendor { Id = 2, Name = "Anzir" });
                entity.HasData(new Vendor { Id = 3, Name = "Noman" });

            });

            //----------------------------------- Purchase Order------------------------------

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateOrder).HasDefaultValueSql("getdate()");
                entity.Property(a => a.RowVersion).IsRowVersion();
                //entity.HasData();

                entity.HasOne<Vendor>(s => s.Vendor)
                    .WithMany(g => g.PurchaseOrders)
                    .HasForeignKey(s => s.Vendor_fk);

                entity.HasOne<Warehouse>(s => s.ToWarehouse)
                    .WithMany(g => g.PoToWareHose)
                    .HasForeignKey(s => s.ToWarehouse_fk);
                entity.HasOne<Warehouse>(s => s.FromWarehouse)
                    .WithMany(g => g.POFromWarehouse)
                    .HasForeignKey(s => s.FromWarehouse_fk);


                entity.HasOne<User>(s => s.UserCreatePo)
                    .WithMany(g => g.CreatePo)
                    .HasForeignKey(s => s.ApprovePoUser_fk);
                entity.HasOne<User>(s => s.UserCreateAsn)
                    .WithMany(g => g.CreateAsn)
                    .HasForeignKey(s => s.ApproveAsnUser_fk);
                entity.HasOne<User>(s => s.UserCreateGrn)
                    .WithMany(g => g.CreateGrn)
                    .HasForeignKey(s => s.ApproveGrnUser_fk);
            });
            //----------------------------------- Warehouse--------------------------------------

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.RowVersion).IsRowVersion();

            });

            //-----------------------------------     ProductInventoryWarehouse  ---------------------------------------

            modelBuilder.Entity<ProductInventoryWarehouse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(b => b.Inventory).HasDefaultValue(0);
                entity.Property(b => b.OnTheWayInventory).HasDefaultValue(0);
                entity.Property(b => b.RowVersion).IsRowVersion();

                entity.HasOne<ProductMaster>(s => s.ProductMaster)
                    .WithMany(g => g.ProductInventoryWarehouses)
                    .HasForeignKey(s => s.ProductMaster_fk);

                entity.HasOne<Warehouse>(s => s.Warehouse)
                    .WithMany(g => g.ProductInventoryWarehouses)
                    .HasForeignKey(s => s.Warehouse_fk);


            });


            //----------------------------------- User ---------------------------------------

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
            });


            //----------------------------------- SaleOrder ---------------------------------------
            modelBuilder.Entity<SaleOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderedDate).HasColumnType("smalldatetime");
                entity.Property(e => e.CancelDate).HasColumnType("smalldatetime");
                entity.Property(e => e.RowVersion).IsRowVersion();

                entity.HasOne<User>(s => s.User)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.Cashier_fk);

                entity.HasOne<Customer>(s => s.Customer)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.Customer_fk);

                entity.HasOne<Province>(s => s.TaxArea)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.TaxArea_fk);

                entity.HasOne<Warehouse>(s => s.Warehouse)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.Warehouse_fk);
            });

            //----------------------------------- Province ---------------------------------------
            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.RowVersion).IsRowVersion();
                //entity.Property(e => e.HST).HasColumnType("real");
                //entity.Property(e => e.GST).HasColumnType("real");
                //entity.Property(e => e.QST).HasColumnType("real");
            });
            //----------------------------------- Customer ---------------------------------------
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Ignore(p => p.FullName);
                entity.Property(p => p.RowVersion).IsRowVersion();

                entity.HasOne<User>(s => s.User)
                    .WithMany(g => g.Customers)
                    .HasForeignKey(s => s.CreatedBy_fk);
            });
            //----------------------------------- SoItem ---------------------------------------
            modelBuilder.Entity<SoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RowVersion).IsRowVersion();
                entity.Property(e => e.QuantityRefunded).HasDefaultValue(0);
                entity.Property(e => e.IsAbaleToRefund).HasDefaultValue(true);

                entity.HasOne<SaleOrder>(s => s.SaleOrder)
                    .WithMany(g => g.SoItems)
                    .HasForeignKey(s => s.So_fk);

                entity.HasOne<ProductMaster>(s => s.ProductMaster)
                    .WithMany(g => g.SoItems)
                    .HasForeignKey(s => s.ProductMaster_fk);
            });
            //----------------------------------- Invitation ---------------------------------------
            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne<User>(s => s.UserSendInvitation)
                    .WithMany(g => g.SendInvitations)
                    .HasForeignKey(s => s.UserSendInvitation_fk);

                entity.HasOne<User>(s=>s.UserRegisterWithInvitation)
                    .WithOne(g=>g.RegisterWithInvitation)
                    .HasForeignKey<Invitation>(s => s.UserRegisterWithInvitation_fk);
            });

            //----------------------------------- Refund ---------------------------------------

            modelBuilder.Entity<Refund>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(p => p.RowVersion).IsRowVersion();

                entity.HasOne<SaleOrder>(s => s.SaleOrder)
                    .WithMany(g => g.Refunds)
                    .HasForeignKey(s => s.SaleOrder_fk);

                
            });

            //----------------------------------- RefundItem ---------------------------------------
            modelBuilder.Entity<RefundItem>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(p => p.RowVersion).IsRowVersion();

                entity.HasOne<Refund>(s => s.Refund)
                    .WithMany(g => g.RefundItems)
                    .HasForeignKey(s => s.Refund_fk);

                entity.HasOne<ProductMaster>(s => s.ProductMaster)
                    .WithMany(g => g.RefundItems)
                    .HasForeignKey(s => s.ProductMaster_fk);

            });

            //----------------------------------- Product ---------------------------------------
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(p => p.RowVersion).IsRowVersion();

                entity.HasOne<Country>(s => s.CountryOfOrgin)
                    .WithMany(g => g.Products)
                    .HasForeignKey(s => s.CountryOfOrginFK);

                entity.HasOne<Brand>(s => s.Brand)
                    .WithMany(g => g.Products)
                    .HasForeignKey(s => s.BrandFK);

                entity.HasOne<Company>(s => s.Company)
                    .WithMany(g => g.Products)
                    .HasForeignKey(s => s.CompanyFK);
                entity.HasOne<Material>(s => s.Material)
                    .WithMany(g => g.Products)
                    .HasForeignKey(s => s.MaterialFK);

            });
            //----------------------------------- Towel ---------------------------------------
            modelBuilder.Entity<Towel>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(p => p.RowVersion).IsRowVersion();

                entity.HasOne<ProductType>(s => s.ProductType)
                    .WithMany(g => g.Towels)
                    .HasForeignKey(s => s.ProductTypeFK);

                entity.HasOne<Colour>(s => s.Colour)
                    .WithMany(g => g.Towels)
                    .HasForeignKey(s => s.ColourFK);

                entity.HasOne<Product>(s => s.Product)
                    .WithMany(g => g.Towels)
                    .HasForeignKey(s => s.ProductFK);
            });
            //----------------------------------- Beding ---------------------------------------
            modelBuilder.Entity<Beding>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(p => p.RowVersion).IsRowVersion();

                entity.HasOne<ProductType>(s => s.ProductType)
                    .WithMany(g => g.Bedings)
                    .HasForeignKey(s => s.ProductTypeFK);

                entity.HasOne<Colour>(s => s.Colour)
                    .WithMany(g => g.Bedings)
                    .HasForeignKey(s => s.ColourFK);

                entity.HasOne<Product>(s => s.Product)
                    .WithMany(g => g.Bedings)
                    .HasForeignKey(s => s.ProductFK);
            });
            //----------------------------------- Barcode ---------------------------------------
            modelBuilder.Entity<Barcode>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(e => e.Towel)
                    .WithOne(e => e.Barcode)
                    .HasForeignKey<Towel>(p => p.BarcodeFK);

                entity.HasOne(e => e.Beding)
                    .WithOne(e => e.Barcode)
                    .HasForeignKey<Beding>(p => p.BarcodeFK);
            });
            //----------------------------------- Image ---------------------------------------
            modelBuilder.Entity<Image>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                //entity.HasOne<Towel>(s => s.Towel)
                //    .WithMany(g => g.Images)
                //    .HasForeignKey(s => s.TowelFK);

                //entity.HasOne<Beding>(s => s.Beding)
                //    .WithMany(g => g.Images)
                //    .HasForeignKey(s => s.BedingFK);

            });
            //----------------------------------- Company ---------------------------------------
            modelBuilder.Entity<Company>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<Country>(s => s.Country)
                    .WithMany(g => g.Companies)
                    .HasForeignKey(s => s.CountryFK);

            });
            //----------------------------------- Country ---------------------------------------
            modelBuilder.Entity<Country>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });
            //----------------------------------- Brand ---------------------------------------
            modelBuilder.Entity<Brand>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();


            });
            //----------------------------------- Material ---------------------------------------
            modelBuilder.Entity<Material>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });
            //----------------------------------- Category ---------------------------------------
            modelBuilder.Entity<Category>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });
            //----------------------------------- SubCategory ---------------------------------------
            modelBuilder.Entity<SubCategory>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });
            //----------------------------------- CategoriesSubCategory ---------------------------------------
            modelBuilder.Entity<CategoriesSubCategory>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<Category>(s => s.Category)
                    .WithMany(g => g.CategoriesSubCategories)
                    .HasForeignKey(s => s.CategoryFK);

                entity.HasOne<SubCategory>(s => s.SubCategory)
                    .WithMany(g => g.CategoriesSubCategories)
                    .HasForeignKey(s => s.SubCategoryFK);

            });
            //----------------------------------- ProductType ---------------------------------------
            modelBuilder.Entity<ProductType>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<CategoriesSubCategory>(s => s.CategoriesSubCategory)
                    .WithMany(g => g.ProductTypes)
                    .HasForeignKey(s => s.CategorysubcategoreisFK);


            });
            //----------------------------------- Colour ---------------------------------------
            modelBuilder.Entity<Colour>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });
        }

    }
}