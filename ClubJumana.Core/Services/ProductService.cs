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

        public List<string> GetAllInformationInventoryProduct(int Id)
        {
            List<string> list=new List<string>();

            int InventoryCount = 0;


            var InventoryDetail = _context.ProductInventoryWarehouses.Where(p => p.ProductMaster_fk == Id).AsNoTracking().ToList();

            var MainWarehouseCount = InventoryDetail.SingleOrDefault(p=>p.Warehouse_fk==2);
            var Store1Count = InventoryDetail.SingleOrDefault(p => p.Warehouse_fk == 3);
            var Store2Count = InventoryDetail.SingleOrDefault(p => p.Warehouse_fk == 4);


            foreach (var VARIABLE in InventoryDetail)
            {
                InventoryCount= VARIABLE.Inventory.Value + VARIABLE.OnTheWayInventory.Value + InventoryCount;
            }

            list.Add(InventoryCount.ToString());

            if (MainWarehouseCount!=null)
                list.Add($"{ MainWarehouseCount.Inventory} ------- Transit : { MainWarehouseCount.OnTheWayInventory}");
            else
            {
                list.Add("---------------");
            }

            if (Store1Count != null)
                list.Add($"{ Store1Count.Inventory} ------- Transit : { Store1Count.OnTheWayInventory}");
            else
            {
                list.Add("---------------");
            }

            if (Store2Count != null)
                list.Add($"{ Store2Count.Inventory} ------- Transit : { Store2Count.OnTheWayInventory}");
            else
            {
                list.Add("---------------");
            }

            return list;
        }
    }
}
