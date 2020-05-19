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
        public IQueryable<ProductMaster> AllProductList()
        {
            return _context.Products;
        }

        public IQueryable<PurchaseOrder> AllPurchaseOrder()
        {
            return _context.PurchaseOrders.AsNoTracking();
        }

        public IQueryable<Vendor> AllVendor()
        {
            return _context.Vendors;
        }

        public IQueryable<PurchaseOrder> AsnPurchaseOrder()
        {
            return _context.PurchaseOrders.Where(p=>p.CreatedPO==true).AsNoTracking();
        }

        public IQueryable<PurchaseOrder> GrnPurchaseOrder()
        {
            return _context.PurchaseOrders.Where(p => p.CreatedAsn == true).AsNoTracking();
        }


        public IQueryable<Warehouse> AllWarehouse()
        {
            return _context.Warehouses;
        }
    }
}
