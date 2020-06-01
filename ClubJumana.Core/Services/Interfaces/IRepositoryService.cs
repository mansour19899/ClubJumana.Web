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
        IQueryable<ProductMaster> AllProductList();
        IQueryable<PurchaseOrder> AllPurchaseOrder();
        IQueryable<SaleOrder> AllOrders();
        IQueryable<Warehouse> AllWarehouse();
        IQueryable<Vendor> AllVendor();
        IQueryable<Province> AllProvinces();
        IQueryable<Customer> AllCustomers();
        IQueryable<PurchaseOrder> AsnPurchaseOrder();
        IQueryable<PurchaseOrder> GrnPurchaseOrder();
        IQueryable<Country> AllCountriesList();
        IQueryable<Category> AllCategoriesList();
        IQueryable<SubCategory> AllSubCategoriesList();
        IQueryable<CategoriesSubCategory> AllCategoriesSubCategoryList();
        IQueryable<ProductType> AllProductTypeList();
        IQueryable<Brand> AllBrandList();
        IQueryable<Colour> AllColourList();
        IQueryable<Material> AllMaterialList();


    }
}
