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
        private OnlineContext onlineDb;
        public RepositoryService()
        {
            _context=new JummanaContext();

            onlineDb=new OnlineContext();
        }

        public void UpdateLocalDb()
        {
            

            var list1 = _context.countries.OrderBy(p => p.Id).Select(p => new { Id = p.Id, Barcode = p.Name, }).ToList();
            var list2 = onlineDb.countries.OrderBy(p => p.Id).Select(p => new { Id = p.Id, Barcode = p.Name }).ToList();

            var IdForAdd = list2.Select(p => p.Id).Except(list1.Select(p => p.Id));

            var list3 = list2.Except(list1);
                Country w;

                foreach (var VARIABLE in list3)
                {
                    w = onlineDb.countries.SingleOrDefault(p => p.Id == VARIABLE.Id);
                    if (IdForAdd.Contains(VARIABLE.Id))
                    {
                        _context.countries.Add(w);

                    }
                    else
                    {
                        _context.countries.Update(w);
                    }
                }
                _context.SaveChanges();


        }

        public IQueryable<ProductMaster> AllProductMasterList()
        {
            return _context.productmasters;
        }

        public IQueryable<PurchaseOrder> AllPurchaseOrder()
        {
            return _context.purchaseorders.AsNoTracking();
        }

        public IQueryable<SaleOrder> AllOrders()
        {
            return _context.saleorders.Include(p => p.SoItems).ThenInclude(p=>p.ProductMaster).Include(p => p.TaxArea)
                .Include(p=>p.Customer).AsNoTracking();
        }

        public IQueryable<Vendor> AllVendor()
        {
            return _context.vendors;
        }

        public IQueryable<Province> AllProvinces()
        {
            return _context.provinces;
        }

        public IQueryable<Customer> AllCustomers()
        {
            return _context.customers;
        }

        public IQueryable<PurchaseOrder> AsnPurchaseOrder()
        {
            return _context.purchaseorders.Where(p=>p.CreatedPO==true).AsNoTracking();
        }

        public IQueryable<PurchaseOrder> GrnPurchaseOrder()
        {
            return _context.purchaseorders.Where(p => p.CreatedAsn == true).AsNoTracking();
        }

        public IQueryable<Country> AllCountriesList()
        {
            return _context.countries;
        }

        public IQueryable<Company> AllCompaniesList()
        {
            return _context.companies;
        }

        public IQueryable<Category> AllCategoriesList()
        {
            return _context.categories;
        }

        public IQueryable<SubCategory> AllSubCategoriesList()
        {
            return _context.subcategories;
        }

        public IQueryable<CategoriesSubCategory> AllCategoriesSubCategoryList()
        {
            return _context.categoriessubcategories;
        }

        public IQueryable<ProductType> AllProductTypeList()
        {
            return _context.producttypes;
        }

        public IQueryable<Brand> AllBrandList()
        {
            return _context.brands;
        }

        public IQueryable<Colour> AllColourList()
        {
            return _context.colours;
        }

        public IQueryable<Material> AllMaterialList()
        {
            return _context.materials;
        }

        public IQueryable<Product> AllProductList()
        {
            return _context.products.Include(p=>p.Variants).ThenInclude(p=>p.ProductType.CategoriesSubCategory);
        }

        public Country GiveMeCountryByID(int Id)
        {
            return _context.countries.FirstOrDefault(p => p.Id == Id);
        }

        public ProductMaster GiveMeProductMasterByUPC(string UPC)
        {
            return _context.productmasters.FirstOrDefault(p => p.UPC.Trim().CompareTo(UPC.Trim()) == 0);
        }


        public IQueryable<Warehouse> AllWarehouse()
        {
            return _context.warehouses;
        }

        public int AddAndUpdateCustomer(Customer customer)
        {
            if (customer.Id == 0)
                _context.customers.Add(customer);
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
                _context.vendors.Add(vendor);
            else
            {
                _context.Update(vendor);
            }

            _context.SaveChanges();
            return 1;
        }

        public int AddAndUpdateItem(ProductMaster productMaster)
        {
            if (productMaster.Id == 0)
                _context.productmasters.Add(productMaster);
            else
            {
                _context.Update(productMaster);
            }

            _context.SaveChanges();
            return 1;
        }
    }
}
