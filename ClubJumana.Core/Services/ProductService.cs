using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace ClubJumana.Core.Services
{
   public class ProductService: IProductService
    {
        private JummanaContext _context;

        public ProductService()
        {
            _context = new JummanaContext();
        }
        public int AddOrUpdateProduct(ProductMaster product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return 0;
        }

        public IDictionary<string, string> GetAllInformationInventoryProduct(int Id)
        {
            IDictionary<string, string> dic=new Dictionary<string, string>();

            int InventoryCount = 0;

            //var InventoryDetail = _context.ProductInventoryWarehouses.Where(p => p.ProductMaster_fk == Id).AsNoTracking().ToList();
            var InventoryDetail = _context.productmasters.Include(p=>p.ProductInventoryWarehouses).AsNoTracking().SingleOrDefault(p => p.Id == Id);

            var MainWarehouseCount = InventoryDetail.ProductInventoryWarehouses.SingleOrDefault(p=>p.Warehouse_fk==2);
            var Store1Count = InventoryDetail.ProductInventoryWarehouses.SingleOrDefault(p => p.Warehouse_fk == 3);
            var Store2Count = InventoryDetail.ProductInventoryWarehouses.SingleOrDefault(p => p.Warehouse_fk == 4);


            //foreach (var VARIABLE in InventoryDetail)
            //{
            //    InventoryCount= VARIABLE.Inventory.Value + VARIABLE.OnTheWayInventory.Value + InventoryCount;
            //}

            dic.Add("StockOnHand", InventoryDetail.StockOnHand.ToString());
            dic.Add("Sale", InventoryDetail.Outcome.ToString());
            dic.Add("Reserved", InventoryDetail.GoodsReserved.ToString());

            dic.Add("TempBalance",(InventoryDetail.StockOnHand -InventoryDetail.GoodsReserved).ToString());

            if (MainWarehouseCount != null)
                dic.Add("MainWarehouse", $"{MainWarehouseCount.Inventory} -------Transit : {MainWarehouseCount.OnTheWayInventory}"); 
            else
            {
                dic.Add("MainWarehouse", "---------------");
            }

            if (Store1Count != null)
                dic.Add("Store1", $"{Store1Count.Inventory} -------Transit : {Store1Count.OnTheWayInventory}");
            else
            {
                dic.Add("Store1", "---------------");
            }

            if (Store2Count != null)
                dic.Add("Store2", $"{Store2Count.Inventory} -------Transit : {Store2Count.OnTheWayInventory}");
            else
            {
                dic.Add("Store2", "---------------");
            }

            return dic;
        }
    }
}
