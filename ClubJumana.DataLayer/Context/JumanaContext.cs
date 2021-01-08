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
        public DbSet<Tax> taxes { get; set; }
        public DbSet<TaxRate> taxrates { get; set; }
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
        public DbSet<TablesVersion> tablesversion { get; set; }
        public DbSet<Term> terms { get; set; }

        public DbSet<Inner> inners { get; set; }
        public DbSet<MasterCarton> mastercartons { get; set; }
        public DbSet<InnerMasterCarton> innermastercartons { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<PaymentMethod> paymentmethods { get; set; }
        public DbSet<PaymentInvoice> paymentinvoices { get; set; }
        public DbSet<DepositTo> deposittos { get; set; }


        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var dbContextOptions = optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=magiclocaldb5;Trusted_Connection=True").EnableSensitiveDataLogging().Options;
            //optionsBuilder.UseMySQL("server=localhost;database=smm38;user=root;password=Mansour11568");
            //optionsBuilder.UseMySQL("server=localhost;database=Test3020;user=root;password=SmmRey2018");
              optionsBuilder.UseMySQL("server=localhost;database=Test86;user=root;password=SmmRey2018");
            //optionsBuilder.UseMySQL("server=localhost;database=MagicLocaldb;user=root;password=SmmRey2018");
            //optionsBuilder.UseMySQL("server=132.148.182.213;database=MagicDTS;user=mansour1989;password=SmmRey2018");

            //Update-Database -Context JummanaContext
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-------------------------------------TablesVersion------------------------------

            modelBuilder.Entity<TablesVersion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasData(new TablesVersion() { Id = 1, Name = "Barcodes", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 2, Name = "Colours", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 3, Name = "Brands", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 4, Name = "Countries", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 5, Name = "Materials", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 6, Name = "Companies", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 7, Name = "Products", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 8, Name = "Categories", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 9, Name = "SubCategories", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 10, Name = "CategoriesSubCategories", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 11, Name = "ProductTypes", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 12, Name = "Varaints", NeedToUpdate = false, RowVersion = 0 });
                entity.HasData(new TablesVersion() { Id = 13, Name = "Images", NeedToUpdate = false, RowVersion = 0 });

            });

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
                entity.Property(e => e.Id).ValueGeneratedNever();
            });


            //----------------------------------- SaleOrder ---------------------------------------
            modelBuilder.Entity<SaleOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
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

                entity.HasOne<Term>(s => s.Term)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.term_fk);
            });

            //----------------------------------- Province ---------------------------------------
            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.HST).HasColumnType("decimal(7,4)");
                entity.Property(e => e.GST).HasColumnType("decimal(7,4)");
                entity.Property(e => e.QST).HasColumnType("decimal(7,4)");
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

                entity.HasOne<Country>(s => s.Country)
                    .WithMany(g => g.Customers)
                    .HasForeignKey(s => s.CountryFK);

                entity.HasOne<Province>(s => s.Province)
                    .WithMany(g => g.Customers)
                    .HasForeignKey(s => s.ProvinceFK);
            });
            //----------------------------------- SoItem ---------------------------------------
            modelBuilder.Entity<SoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.QuantityRefunded).HasDefaultValue(0);
                entity.Property(e => e.IsAbaleToRefund).HasDefaultValue(true);

                entity.HasOne<SaleOrder>(s => s.SaleOrder)
                    .WithMany(g => g.SoItems)
                    .HasForeignKey(s => s.So_fk);

                entity.HasOne<ProductMaster>(s => s.ProductMaster)
                    .WithMany(g => g.SoItems)
                    .HasForeignKey(s => s.ProductMaster_fk);
            });
            //----------------------------------- Tax ---------------------------------------
            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne<SaleOrder>(s => s.SaleOrder)
                    .WithMany(g => g.Taxes)
                    .HasForeignKey(s => s.SalesOrderFK);
                entity.Property(e => e.Rate).HasColumnType("decimal(7,4)");
                entity.Property(e => e.Amount).HasColumnType("decimal(7,4)");
                entity.Property(e => e.TaxAmount).HasColumnType("decimal(7,4)");

            });
            //----------------------------------- Tax Rate ---------------------------------------
            modelBuilder.Entity<TaxRate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Rate).HasColumnType("decimal(7,4)");
            });
            //----------------------------------- Invitation ---------------------------------------
            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
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
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne<SaleOrder>(s => s.SaleOrder)
                    .WithMany(g => g.Refunds)
                    .HasForeignKey(s => s.SaleOrder_fk);


            });

            //----------------------------------- RefundItem ---------------------------------------
            modelBuilder.Entity<RefundItem>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
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
                entity.Property(e => e.FobPrice).HasColumnType("decimal(18,4)");
                entity.HasOne<ProductType>(s => s.ProductType)
                    .WithMany(g => g.Variants)
                    .HasForeignKey(s => s.ProductTypeFK);

                entity.HasOne<Colour>(s => s.Colour)
                    .WithMany(g => g.Variants)
                    .HasForeignKey(s => s.ColourFK);

                entity.HasOne<Product>(s => s.Product)
                    .WithMany(g => g.Variants)
                    .HasForeignKey(s => s.ProductFK);

                entity.HasOne(e => e.ProductMaster)
                    .WithOne(e => e.Variant)
                    .HasForeignKey<ProductMaster>(p => p.VariantFK);
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

            //----------------------------------- Term ---------------------------------------
            modelBuilder.Entity<Term>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });

            //----------------------------------- Inners ---------------------------------------
            modelBuilder.Entity<Inner>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<ProductMaster>(s => s.ProductMaster)
                    .WithMany(g => g.Inners)
                    .HasForeignKey(s => s.ProductMasterFK);

            });
            //----------------------------------- MasterCarton ---------------------------------------
            modelBuilder.Entity<MasterCarton>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });
            //----------------------------------- InnersMasterCarton ---------------------------------------
            modelBuilder.Entity<InnerMasterCarton>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<Inner>(s => s.Inner)
                    .WithMany(g => g.InnerMasterCartons)
                    .HasForeignKey(s => s.InnerFK);

                entity.HasOne<MasterCarton>(s => s.MasterCarton)
                    .WithMany(g => g.InnerMasterCartons)
                    .HasForeignKey(s => s.MasterCartonFK);
            });

            //----------------------------------- Payment ---------------------------------------
            modelBuilder.Entity<Payment>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<PaymentMethod>(s => s.PaymentMethod)
                    .WithMany(g => g.Payments)
                    .HasForeignKey(s => s.PaymentMethodFK);

                entity.HasOne<DepositTo>(s => s.DepositTo)
                    .WithMany(g => g.Payments)
                    .HasForeignKey(s => s.DepositToFK);

            });
            //----------------------------------- Payment Invoice ---------------------------------------
            modelBuilder.Entity<PaymentInvoice>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<Payment>(s => s.Payment)
                    .WithMany(g => g.PaymentInvoices)
                    .HasForeignKey(s => s.PaymenteFK);

                entity.HasOne<SaleOrder>(s => s.Invoice)
                    .WithMany(g => g.PaymentInvoices)
                    .HasForeignKey(s => s.InvoiceFK);
            });

            //----------------------------------- Payment Method ---------------------------------------
            modelBuilder.Entity<PaymentMethod>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });

            //----------------------------------- Deposit To ---------------------------------------
            modelBuilder.Entity<DepositTo>(entity =>
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
            //optionsBuilder.UseMySQL("server=localhost;database=db1;user=root;password=SmmRey2018");
            //optionsBuilder.UseMySQL("server=localhost;database=MagicLocaldb;user=root;password=SmmRey2018");
            optionsBuilder.UseMySQL("server=localhost;database=Test86;user=root;password=SmmRey2018");
            //optionsBuilder.UseMySQL("server=localhost;database=smm38;user=root;password=Man1989sour");
             // optionsBuilder.UseMySQL("server=132.148.182.213;database=MagicDTS;user=mansour1989;password=SmmRey2018");
            //optionsBuilder.UseMySQL("server=localhost;database=Test2020;user=root;password=SmmRey2018");



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
