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
        IQueryable<Warehouse> AllWarehouse();
        IQueryable<Vendor> AllVendor();
        IQueryable<PurchaseOrder> AsnPurchaseOrder();
        IQueryable<PurchaseOrder> GrnPurchaseOrder();


    }
}
