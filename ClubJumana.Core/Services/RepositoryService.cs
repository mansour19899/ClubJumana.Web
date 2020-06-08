using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubJumana.Core.Services
{
   public class RepositoryService: IRepositoryService
    {
        private JummanaContext _context;
        public RepositoryService()
        {
            _context=new JummanaContext();
        }
        public IQueryable<ProductMaster> AllProductMasterList()
        {
            return _context.ProductMasters;
        }

        public IQueryable<PurchaseOrder> AllPurchaseOrder()
        {
            return _context.PurchaseOrders.AsNoTracking();
        }

        public IQueryable<SaleOrder> AllOrders()
        {
            return _context.SaleOrders.Include(p => p.SoItems).ThenInclude(p=>p.ProductMaster).Include(p => p.TaxArea)
                .Include(p=>p.Customer).AsNoTracking();
        }

        public IQueryable<Vendor> AllVendor()
        {
            return _context.Vendors;
        }

        public IQueryable<Province> AllProvinces()
        {
            return _context.Provinces;
        }

        public IQueryable<Customer> AllCustomers()
        {
            return _context.Customers;
        }

        public IQueryable<PurchaseOrder> AsnPurchaseOrder()
        {
            return _context.PurchaseOrders.Where(p=>p.CreatedPO==true).AsNoTracking();
        }

        public IQueryable<PurchaseOrder> GrnPurchaseOrder()
        {
            return _context.PurchaseOrders.Where(p => p.CreatedAsn == true).AsNoTracking();
        }

        public IQueryable<Country> AllCountriesList()
        {
            return _context.Countries;
        }

        public IQueryable<Company> AllCompaniesList()
        {
            return _context.Companies;
        }

        public IQueryable<Category> AllCategoriesList()
        {
            return _context.Categories;
        }

        public IQueryable<SubCategory> AllSubCategoriesList()
        {
            return _context.SubCategories;
        }

        public IQueryable<CategoriesSubCategory> AllCategoriesSubCategoryList()
        {
            return _context.CategoriesSubCategories;
        }

        public IQueryable<ProductType> AllProductTypeList()
        {
            return _context.ProductTypes;
        }

        public IQueryable<Brand> AllBrandList()
        {
            return _context.Brands;
        }

        public IQueryable<Colour> AllColourList()
        {
            return _context.Colours;
        }

        public IQueryable<Material> AllMaterialList()
        {
            return _context.Materials;
        }

        public IQueryable<Product> AllProductList()
        {
            return _context.Products.Include(p=>p.Variants).ThenInclude(p=>p.ProductType.CategoriesSubCategory);
        }

        public Country GiveMeCountryByID(int Id)
        {
            return _context.Countries.FirstOrDefault(p => p.Id == Id);
        }


        public IQueryable<Warehouse> AllWarehouse()
        {
            return _context.Warehouses;
        }

        public int AddAndUpdateCustomer(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                _context.Update(customer);
            }

            _context.SaveChanges();
            return 1;
        }

        public int AddAndUpdateVendor(Vendor vendor)
        {
            if (vendor.Id == 0)
                _context.Vendors.Add(vendor);
            else
            {
                _context.Update(vendor);
            }

            _context.SaveChanges();
            return 1;
        }
    }
}
