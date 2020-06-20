using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.DataLayer.Context
{
    public class JummanaContext : DbContext
    {

        //public JummanaContext(DbContextOptions<JummanaContext> options):base(options)
        //{

        //}

        #region User

        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserRole> userroles { get; set; }
        public DbSet<Invitation> invitations { get; set; }

        public DbSet<ProductMaster> productmasters { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<PurchaseOrder> purchaseorders { get; set; }
        public DbSet<Vendor> vendors { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<ProductInventoryWarehouse> productinventorywarehouses { get; set; }
        public DbSet<SaleOrder> saleorders { get; set; }
        public DbSet<SoItem> soitems { get; set; }
        public DbSet<Refund> refunds { get; set; }
        public DbSet<RefundItem> refunditems { get; set; }
        public DbSet<Province> provinces { get; set; }


        public DbSet<Product> products { get; set; }
        public DbSet<Variant> variants { get; set; }
        public DbSet<Colour> colours { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<Barcode> barcodes { get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Material> materials { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<ProductType> producttypes { get; set; }
        public DbSet<CategoriesSubCategory> categoriessubcategories { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<SubCategory> subcategories { get; set; }


        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // var dbContextOptions = optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=magiclocaldb5;Trusted_Connection=True").EnableSensitiveDataLogging().Options;
            // optionsBuilder.UseMySQL("server=localhost;database=smm38;user=root;password=Mansour11568");
             optionsBuilder.UseMySQL("server=localhost;database=MagicLocaldb;user=root;password=SmmRey2018");
            //optionsBuilder.UseMySQL("server=148.72.112.16;database=MagicDTS;user=mansour1989;password=Man1989sour");

            //Update - Database - Context JummanaContext
        }
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
            //modelBuilder.Entity<Item>().Property(e => e.AsnItemsPrice).HasColumnType("decimal(5, 2)");
            modelBuilder.Entity<Item>().Ignore(p => p.Price);
            modelBuilder.Entity<Item>().Ignore(p => p.TotalItemPrice);
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


            });

            //----------------------------------- Vendor--------------------------------------

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Note).HasColumnName("Note");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.HasData(new Vendor { Id = 1, Name = "ClubJummana" });
                entity.HasData(new Vendor { Id = 2, Name = "Anzir" });
                entity.HasData(new Vendor { Id = 3, Name = "Noman" });

            });

            //----------------------------------- Purchase Order------------------------------

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                //entity.Property(e => e.CreateOrder).HasDefaultValueSql("getdate()");
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

            });

            //-----------------------------------     ProductInventoryWarehouse  ---------------------------------------

            modelBuilder.Entity<ProductInventoryWarehouse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(b => b.Inventory).HasDefaultValue(0);
                entity.Property(b => b.OnTheWayInventory).HasDefaultValue(0);

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
                //entity.Property(e => e.OrderedDate).HasColumnType("smalldatetime");
                //entity.Property(e => e.CancelDate).HasColumnType("smalldatetime");

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

                entity.HasOne<User>(s => s.User)
                    .WithMany(g => g.Customers)
                    .HasForeignKey(s => s.CreatedBy_fk);
            });
            //----------------------------------- SoItem ---------------------------------------
            modelBuilder.Entity<SoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
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

                entity.HasOne<User>(s => s.UserRegisterWithInvitation)
                    .WithOne(g => g.RegisterWithInvitation)
                    .HasForeignKey<Invitation>(s => s.UserRegisterWithInvitation_fk);
            });

            //----------------------------------- Refund ---------------------------------------

            modelBuilder.Entity<Refund>(entity =>
            {

                entity.HasKey(e => e.Id);

                entity.HasOne<SaleOrder>(s => s.SaleOrder)
                    .WithMany(g => g.Refunds)
                    .HasForeignKey(s => s.SaleOrder_fk);


            });

            //----------------------------------- RefundItem ---------------------------------------
            modelBuilder.Entity<RefundItem>(entity =>
            {

                entity.HasKey(e => e.Id);

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
            //----------------------------------- Variant ---------------------------------------
            modelBuilder.Entity<Variant>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<ProductType>(s => s.ProductType)
                    .WithMany(g => g.Variants)
                    .HasForeignKey(s => s.ProductTypeFK);

                entity.HasOne<Colour>(s => s.Colour)
                    .WithMany(g => g.Variants)
                    .HasForeignKey(s => s.ColourFK);

                entity.HasOne<Product>(s => s.Product)
                    .WithMany(g => g.Variants)
                    .HasForeignKey(s => s.ProductFK);


            });

            //----------------------------------- Barcode ---------------------------------------
            modelBuilder.Entity<Barcode>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Id).HasMaxLength(12);
                //entity.Property(e => e.BarcodeNumber).HasColumnType("smalldatetime");

                entity.HasOne(e => e.Variant)
                    .WithOne(e => e.Barcode)
                    .HasForeignKey<Variant>(p => p.BarcodeFK);
            });
            //----------------------------------- Image ---------------------------------------
            modelBuilder.Entity<Image>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<Variant>(s => s.Variant)
                    .WithMany(g => g.Images)
                    .HasForeignKey(s => s.VariantFK);

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
                entity.Property(e => e.ExChangeRate).HasColumnType("decimal(14,5)");
                entity.Property(e => e.Duty).HasColumnType("decimal(5, 2)");

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


    public class OnlineContext : JummanaContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var dbContextOptionss = optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCore-smm97;Trusted_Connection=True").EnableSensitiveDataLogging().Options; ;
            // optionsBuilder.UseMySQL("server=localhost;database=db1;user=root;password=SmmRey2018");
            //optionsBuilder.UseMySQL("server=localhost;database=smm38;user=root;password=Man1989sour");
            optionsBuilder.UseMySQL("server=148.72.112.16;database=MagicDTS;user=mansour1989;password=Man1989sour");


            //Update-Database -Context JummanaContext
            //Update-Database -Context OnlineContext

            //Add-Migration InitialCreate -Context JummanaContext
            //Add-Migration InitialCreate -Context OnlineContext
        }
    }
    public class text2Context : JummanaContext
    {

    }
}
