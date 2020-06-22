using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
    interface IRepositoryService
    {
        void UpdateLocalDb();
        IQueryable<ProductMaster> AllProductMasterList();
        IQueryable<PurchaseOrder> AllPurchaseOrder();
        IQueryable<SaleOrder> AllOrders();
        IQueryable<Warehouse> AllWarehouse();
        IQueryable<Vendor> AllVendor();
        IQueryable<Province> AllProvinces();
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

        Country GiveMeCountryByID(int Id);
        ProductMaster GiveMeProductMasterByUPC(string UPC);

        public int AddAndUpdateCustomer(Customer customer);
        public int AddAndUpdateVendor(Vendor vendor);
        public int AddAndUpdateItem(ProductMaster productMaster);

        public string UploadFileToFTP( string fileName, string UploadDirectory);
        public string DownloadFileFromFTP( string fileName, string UploadDirectory);

    }
}
