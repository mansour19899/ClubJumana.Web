using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace ClubJumana.Core.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private JummanaContext _context;
        public SaleOrderService()
        {
            _context = new JummanaContext();
        }
        public int SaveAndUpdateSaleOrder(SaleOrderViewModel saleOrder)
        {
            SaleOrder So = new SaleOrder();
            if (saleOrder.Id == 0)
            {
                So = new SaleOrder()
                {
                    Type = saleOrder.Type,
                    OrderedDate = saleOrder.OrderedDate,
                    ShipDate = saleOrder.ShipDate,
                    CancelDate = saleOrder.CancelDate,
                    SalesOrderNumber = saleOrder.SalesOrderNumber,
                    InvoiceNumber = saleOrder.InvoiceNumber,
                    Cashier_fk = saleOrder.User.Id,
                    Customer_fk = saleOrder.Customer.Id,
                    Warehouse_fk = saleOrder.Warehouse.Id,
                    ShipMethod_fk = saleOrder.ShipMethod_fk,
                    Subtotal = saleOrder.Subtotal,
                    SoTotalPrice = saleOrder.SoTotalPrice,
                    TaxArea_fk = saleOrder.TaxArea_fk,
                    Tax = saleOrder.Tax,
                    Handling = saleOrder.Handling,
                    Freight = saleOrder.Freight,
                    TotalDiscount = saleOrder.TotalDiscount,
                    ShipToAddressName = saleOrder.ShipToAddressName,
                    ShipToAddressNam1 = saleOrder.ShipToAddressNam1,
                    ShipToAddressNam2 = saleOrder.ShipToAddressNam2,
                    ShipToPostalCode = saleOrder.ShipToPostalCode,
                    ShipToPostalPhone1 = saleOrder.ShipToPostalPhone1,
                    Quantity = saleOrder.Quantity,
                    IsDeleted = false,

                };
                _context.SaleOrders.Add(So);

                _context.SaveChanges();

            }
            else
            {
                So = _context.SaleOrders.SingleOrDefault(p => p.Id == saleOrder.Id);
                So.Type = saleOrder.Type;
                So.OrderedDate = saleOrder.OrderedDate;
                So.ShipDate = saleOrder.ShipDate;
                So.CancelDate = saleOrder.CancelDate;
                So.SalesOrderNumber = saleOrder.SalesOrderNumber;
                So.InvoiceNumber = saleOrder.InvoiceNumber;
                So.Cashier_fk = saleOrder.User.Id;
                So.Customer_fk = saleOrder.Customer.Id;
                So.Warehouse_fk = saleOrder.Warehouse.Id;
                So.ShipMethod_fk = saleOrder.ShipMethod_fk;
                So.Subtotal = saleOrder.Subtotal;
                So.SoTotalPrice = saleOrder.SoTotalPrice;
                So.TaxArea_fk = saleOrder.TaxArea_fk;
                So.Tax = saleOrder.Tax;
                So.Handling = saleOrder.Handling;
                So.Freight = saleOrder.Freight;
                So.TotalDiscount = saleOrder.TotalDiscount;
                So.ShipToAddressName = saleOrder.ShipToAddressName;
                So.ShipToAddressNam1 = saleOrder.ShipToAddressNam1;
                So.ShipToAddressNam2 = saleOrder.ShipToAddressNam2;
                So.ShipToPostalCode = saleOrder.ShipToPostalCode;
                So.ShipToPostalPhone1 = saleOrder.ShipToPostalPhone1;
                So.Quantity = saleOrder.Quantity;
                So.IsDeleted = saleOrder.IsDeleted;

            }

            SoItem soItem = new SoItem();

            foreach (var model in saleOrder.SoItems)
            {
                if (model.Id == 0 && !model.IsDeleted)
                {
                    _context.SoItems.Add(new SoItem()
                    {
                        So_fk = So.Id,
                        ProductMaster_fk = model.ProductMaster_fk,
                        Cost = model.Cost,
                        Discount = model.Discount,
                        Quantity = model.Quantity,
                        Price = model.Price,
                        TotalPrice = model.TotalPrice
                    });
                }
                else if (model.Id != 0 && model.IsDeleted)
                {
                    soItem = _context.SoItems.SingleOrDefault(p => p.Id == model.Id);
                    _context.Remove(soItem);
                }
                else if (model.Id != 0)
                {
                    soItem = _context.SoItems.SingleOrDefault(p => p.Id == model.Id);
                    soItem.Cost = model.Cost;
                    soItem.Discount = model.Discount;
                    soItem.Quantity = model.Quantity;
                    soItem.Price = model.Price;
                    soItem.TotalPrice = model.TotalPrice;
                }
                else
                {

                }
            }

            _context.SaveChanges();

            return 1;
        }

        public SaleOrderViewModel GiveSaleOrderById(int id)
        {

            var saleOrder = _context.SaleOrders.Where(p => p.Id == id).Include(p => p.SoItems).ThenInclude(p => p.ProductMaster).Include(p => p.TaxArea)
                  .Include(p => p.Customer).Include(p => p.User).Include(p => p.Warehouse).SingleOrDefault();

            var listSoItem = new List<SoItemVeiwModel>();

            foreach (var VARIABLE in saleOrder.SoItems)
            {
                listSoItem.Add(new SoItemVeiwModel()
                {
                    Id = VARIABLE.Id,
                    So_fk = VARIABLE.So_fk,
                    ProductMaster_fk = VARIABLE.ProductMaster_fk,
                    ProductMaster = VARIABLE.ProductMaster,
                    Cost = VARIABLE.Cost,
                    Discount = VARIABLE.Discount,
                    Quantity = VARIABLE.Quantity,
                    Price = VARIABLE.Price,
                    QuantityRefunded = VARIABLE.QuantityRefunded,
                    IsAbaleToRefund = VARIABLE.IsAbaleToRefund,
                    TotalPrice = VARIABLE.TotalPrice
                });
            }

            List<decimal> TaxRate2 = new List<decimal>();
            TaxRate2.Add(saleOrder.TaxArea.HST.Value);
            TaxRate2.Add(saleOrder.TaxArea.QST.Value);
            TaxRate2.Add(saleOrder.TaxArea.GST.Value);


            return new SaleOrderViewModel()
            {
                Id = saleOrder.Id,
                TaxRate = TaxRate2,
                Type = saleOrder.Type,
                OrderedDate = saleOrder.OrderedDate,
                ShipDate = saleOrder.ShipDate,
                CancelDate = saleOrder.CancelDate,
                SalesOrderNumber = saleOrder.SalesOrderNumber,
                InvoiceNumber = saleOrder.InvoiceNumber,
                Cashier_fk = saleOrder.User.Id,
                Customer_fk = saleOrder.Customer.Id,
                Customer = saleOrder.Customer,
                Warehouse_fk = saleOrder.Warehouse.Id,
                Warehouse = saleOrder.Warehouse,
                ShipMethod_fk = saleOrder.ShipMethod_fk,
                Subtotal = saleOrder.Subtotal,
                SoTotalPrice = saleOrder.SoTotalPrice,
                TaxArea_fk = saleOrder.TaxArea_fk,
                TaxArea = saleOrder.TaxArea,
                Tax = saleOrder.Tax,
                Handling = saleOrder.Handling,
                Freight = saleOrder.Freight,
                TotalDiscount = saleOrder.TotalDiscount,
                ShipToAddressName = saleOrder.ShipToAddressName,
                ShipToAddressNam1 = saleOrder.ShipToAddressNam1,
                ShipToAddressNam2 = saleOrder.ShipToAddressNam2,
                ShipToPostalCode = saleOrder.ShipToPostalCode,
                ShipToPostalPhone1 = saleOrder.ShipToPostalPhone1,
                Quantity = saleOrder.Quantity,
                IsDeleted = saleOrder.IsDeleted,
                SoItems = listSoItem,
                User = saleOrder.User
            };
        }

        public bool SendEmailOrPrint(SaleOrderViewModel saleOrder, bool IsPrint = false)
        {
            saleOrder.SalesOrderNumber = _context.SaleOrders.Max(p => p.SalesOrderNumber) + 1;
            if (saleOrder.SalesOrderNumber == null)
                saleOrder.SalesOrderNumber = 345;
            ResrevdItemsOfSaleOrder(saleOrder.Id);
            SaveAndUpdateSaleOrder(saleOrder);

            return true;
        }

        public bool CreateInvoice(SaleOrderViewModel saleOrder)
        {
            saleOrder.InvoiceNumber = _context.SaleOrders.Max(p => p.InvoiceNumber) + 1;
            if (saleOrder.InvoiceNumber == null)
                saleOrder.InvoiceNumber = 890;
            //Edit Code Later
            var soItem = _context.SoItems.Where(p => p.So_fk == saleOrder.Id).Include(p => p.ProductMaster).ThenInclude(e => e.ProductInventoryWarehouses).ToList();
            ProductInventoryWarehouse inventoryWarehouse;
            foreach (SoItem item in soItem)
            {
                if (saleOrder.SalesOrderNumber != null)
                    item.ProductMaster.GoodsReserved -= item.Quantity;
                item.ProductMaster.StockOnHand -= item.Quantity;
                item.ProductMaster.Outcome += item.Quantity;
                inventoryWarehouse = item.ProductMaster.ProductInventoryWarehouses.SingleOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk);
                inventoryWarehouse.Inventory -= item.Quantity;
                inventoryWarehouse.OutCome += item.Quantity;

            }

            SaveAndUpdateSaleOrder(saleOrder);
            return true;
        }

        public bool AddRefund(Refund refund)
        {
            refund.RefundDate = DateTime.Now;
            refund.RefundNumber = 1368;
            _context.Refunds.Add(refund);

            var inventorys = _context.ProductInventoryWarehouses.Where(p => p.Warehouse_fk == refund.WarehouseId.Value).ToList();
            var SoItems = _context.SoItems.Where(p => p.So_fk == refund.SaleOrder_fk);
            ProductInventoryWarehouse x;
            foreach (var model in refund.RefundItems)
            {
                model.ProductMaster.StockOnHand += model.Quantity;
                model.ProductMaster.RefundQuantity += model.Quantity;

                x = inventorys.SingleOrDefault(p => p.ProductMaster_fk == model.ProductMaster_fk);
                SoItems.SingleOrDefault(p => p.ProductMaster_fk == model.ProductMaster_fk).QuantityRefunded +=
                    model.Quantity;
                x.Inventory += model.Quantity;
                x.RefundQuantity += model.Quantity;
            }
            _context.SaveChanges();
            return true;
        }

        public List<RefundItem> CovertToRefundItem(ObservableCollection<RefundItemsViewModel> refundItemsViewModel)
        {
            List<RefundItem> refundItems = new List<RefundItem>();

            foreach (var model in refundItemsViewModel)
            {
                refundItems.Add(new RefundItem()
                {
                    ProductMaster_fk = model.ProductMaster_fk,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    TotalPrice = model.TotalPrice
                });
            }

            return refundItems;
        }


        private void ResrevdItemsOfSaleOrder(int Id)
        {
            var soItem = _context.SoItems.Where(p => p.So_fk == Id).Include(p => p.ProductMaster).ToList();

            foreach (SoItem item in soItem)
            {
                item.ProductMaster.GoodsReserved += item.Quantity;
            }
        }
    }
}
