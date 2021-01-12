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
        private OnlineContext _context;

        public ProductService()
        {
            _context = new OnlineContext();
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

        public ProductMaster GetProductMasterById(int Id)
        {
            return _context.productmasters.Include(p=>p.Inners).ThenInclude(p=>p.InnerMasterCartons).ThenInclude(p=>p.MasterCarton).FirstOrDefault(p => p.Id == Id);
        }

        public int AddInner(Inner inner)
        {
            try
            {
                inner.Id = _context.inners.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                inner.Id = 1;
            }
            _context.inners.Add(inner);
            _context.SaveChanges();
            return 1;
        }

        public int AddMaster(MasterCarton masterCarton)
        {
            var temp = masterCarton.InnerMasterCartons.ToList();
            int newIdInnerMaster = 1;
            int totalQuantity = 0;
            try
            {
                masterCarton.Id = _context.mastercartons.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                masterCarton.Id = 1;

            }

            try
            {
                 newIdInnerMaster = _context.innermastercartons.Max(p=>p.Id)+1;
            }
            catch (Exception e)
            {
                newIdInnerMaster = 1;
            }
            foreach (var item in masterCarton.InnerMasterCartons)
            {
                item.Id = newIdInnerMaster;
                item.MasterCartonFK = masterCarton.Id;
                totalQuantity += item.InnerQuntity*item.Inner.Quantity;
                 newIdInnerMaster++;
            }

            masterCarton.TotalQuantity = totalQuantity;
            _context.mastercartons.Add(masterCarton);
            _context.SaveChanges();
            return 1;
        }

        public bool CheckInnerITF(string itf14)
        {
            var x = _context.inners.FirstOrDefault(p => p.ITF14.CompareTo(itf14) == 0);

            if (x == null)
                return false;
            else
                return true;

        }

        public bool CheckMasterCartonITF(string itf14)
        {
            var x = _context.mastercartons.FirstOrDefault(p => p.ITF14.CompareTo(itf14) == 0);

            if (x == null)
                return false;
            else
                return true;
        }

        public Inner GetInnerByITF(string itf14)
        {
            return _context.inners.Include(p=>p.ProductMaster).FirstOrDefault(p => p.ITF14.Trim().CompareTo(itf14.Trim()) == 0);
        }

        public MasterCarton GetMasterByITF(string itf14)
        {
            return _context.mastercartons.Include(p=>p.InnerMasterCartons)
                .ThenInclude(p=>p.Inner).ThenInclude(p=>p.ProductMaster)
                .FirstOrDefault(p => p.ITF14.Trim().CompareTo(itf14.Trim()) == 0);
        }
    }
}
