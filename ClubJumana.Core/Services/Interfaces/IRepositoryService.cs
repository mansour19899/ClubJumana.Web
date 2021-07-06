using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
   public interface IRepositoryService
    {
        void UpdateLocalDb();
        IQueryable<Variant> allVariants();
        IQueryable<ProductMaster> AllProductMasterList();
        IQueryable<ProductMaster> AllProductMasterWithInventoryList();
        IQueryable<PurchaseOrder> AllPurchaseOrder();
        IQueryable<SaleOrder> AllOrders();
        IQueryable<Warehouse> AllWarehouse();
        IQueryable<Vendor> AllVendor();
        IQueryable<Province> AllProvinces();
        IQueryable<TaxRate> AlltaTaxRates();
        IQueryable<Customer> AllCustomers();
        IQueryable<PurchaseOrder> AsnPurchaseOrder();
        IQueryable<PurchaseOrder> GrnPurchaseOrder();
        IQueryable<Country> AllCountriesList();
        IQueryable<Company> AllCompaniesList();
        IQueryable<Category> AllCategoriesList();
        IQueryable<SubCategory> AllSubCategoriesList();
        IQueryable<CategoriesSubCategory> AllCategoriesSubCategoryList();
        IQueryable<ProductType> AllProductTypeList();
        IQueryable<Brand> AllBrandList();
        IQueryable<Colour> AllColourList();
        IQueryable<Material> AllMaterialList();
        IQueryable<Product> AllProductList();
        IQueryable<Term> AllTerms();
        IQueryable<PaymentMethod> AllPaymentMethods();
        IQueryable<DepositTo> AllDepositTos();
        IQueryable<UOM> AllUoms();

        Country GiveMeCountryByID(int Id);
        ProductMaster GiveMeProductMasterByUPC(string UPC);
        Customer GiveMeCustomerById(int Id);
        Vendor GiveMeVendorById(int Id);

        public int AddAndUpdateCustomer(Customer customer, bool isSave = true);
        public int AddAndUpdateVendor(Vendor vendor, bool isSave = true);

        public string UploadFileToFTP( string fileName, string UploadDirectory);
        public string DownloadFileFromFTP( string fileName, string UploadDirectory);
        public bool SaveDatabase();
    }
}
