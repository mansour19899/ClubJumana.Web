﻿using System;
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
        private void DetachedAllEntries()
        {
            foreach (var entry in _context.ChangeTracker.Entries().ToList())
            {
                _context.Entry(entry.Entity).State = EntityState.Detached;
            }
            //foreach (var entry in _onlineContext.ChangeTracker.Entries().ToList())
            //{
            //    _onlineContext.Entry(entry.Entity).State = EntityState.Detached;
            //}
        }
        public int SaveAndUpdateSaleOrder(SaleOrderViewModel saleOrder)
        {
            DetachedAllEntries();
            SaleOrder So = new SaleOrder();
            if (saleOrder.Id == 0)
            {
                So = new SaleOrder()
                {
                    Type = saleOrder.Type,
                    SoDate = saleOrder.SoDate,
                    ShipDate = saleOrder.ShipDate,
                    ExpriationDate = saleOrder.ExpriationDate,
                    DueDate = saleOrder.DueDate,
                    InvoiceDate = saleOrder.InvoiceDate,
                    term_fk = saleOrder.term_fk,
                    InvoiceNumber = saleOrder.InvoiceNumber,
                    Cashier_fk = saleOrder.Cashier_fk,
                    Customer_fk = saleOrder.Customer_fk,
                    Warehouse_fk = saleOrder.Warehouse_fk,
                    ShipMethod_fk = saleOrder.ShipMethod_fk,
                    Subtotal = saleOrder.Subtotal,
                    SoTotalPrice = saleOrder.SoTotalPrice,
                    TaxArea_fk = saleOrder.TaxArea_fk,
                    Tax = saleOrder.Tax,
                    Handling = saleOrder.Handling,
                    Freight = saleOrder.Freight,
                    TotalDiscount = saleOrder.TotalDiscount,
                    TrackingNo = saleOrder.TrackingNo,
                    BillingAddress = saleOrder.BillingAddress,
                    ShippingAddress = saleOrder.ShippingAddress,
                    ShipVia = saleOrder.ShipVia,
                    MessageOnInvoice = saleOrder.MessageOnInvoice,
                    MessageOnStatment = saleOrder.MessageOnStatment,
                    Note = saleOrder.Note,
                    Quantity = saleOrder.Quantity,
                    IsDeleted = false,

                };
                _context.saleorders.Add(So);

                _context.SaveChanges();

            }
            else
            {
                So = _context.saleorders.SingleOrDefault(p => p.Id == saleOrder.Id);
                So.Type = saleOrder.Type;
                So.SoDate = saleOrder.SoDate;
                So.ShipDate = saleOrder.ShipDate;
                So.ExpriationDate = saleOrder.ExpriationDate;
                So.DueDate = saleOrder.DueDate;
                So.InvoiceDate = saleOrder.InvoiceDate;
                So.term_fk = saleOrder.term_fk;
                So.InvoiceNumber = saleOrder.InvoiceNumber;
                So.Cashier_fk = saleOrder.Cashier_fk;
                So.Customer_fk = saleOrder.Customer_fk;
                So.Warehouse_fk = saleOrder.Warehouse_fk;
                So.ShipMethod_fk = saleOrder.ShipMethod_fk;
                So.Subtotal = saleOrder.Subtotal;
                So.SoTotalPrice = saleOrder.SoTotalPrice;
                So.TaxArea_fk = saleOrder.TaxArea_fk;
                So.Tax = saleOrder.Tax;
                So.Handling = saleOrder.Handling;
                So.Freight = saleOrder.Freight;
                So.TotalDiscount = saleOrder.TotalDiscount;
                So.TrackingNo = saleOrder.TrackingNo;
                So.BillingAddress = saleOrder.BillingAddress;
                So.ShippingAddress = saleOrder.ShippingAddress;
                So.ShipVia = saleOrder.ShipVia;
                So.MessageOnInvoice = saleOrder.MessageOnInvoice;
                So.MessageOnStatment = saleOrder.MessageOnStatment;
                So.Note = saleOrder.Note;
                So.Quantity = saleOrder.Quantity;
                So.IsDeleted = false;

            }

            SoItem soItem = new SoItem();

            foreach (var model in saleOrder.SoItems)
            {
                if (model.Id == 0 && !model.IsDeleted)
                {
                    _context.soitems.Add(new SoItem()
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
                    soItem = _context.soitems.SingleOrDefault(p => p.Id == model.Id);
                    _context.Remove(soItem);
                }
                else if (model.Id != 0)
                {
                    soItem = _context.soitems.SingleOrDefault(p => p.Id == model.Id);
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

            return saleOrder.Id;
        }

        public SaleOrderViewModel GiveSaleOrderById(int id)
        {

            var saleOrder = _context.saleorders.Where(p => p.Id == id).Include(p=>p.Term).Include(p => p.SoItems).ThenInclude(p => p.ProductMaster).Include(p => p.TaxArea)
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
                    TotalPrice = VARIABLE.TotalPrice,
                    PriceTerm = VARIABLE.PriceTerm,
                    TermPercent = saleOrder.TermPercent
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
                SoDate = saleOrder.SoDate,
                ShipDate = saleOrder.ShipDate,
                ExpriationDate = saleOrder.ExpriationDate,
                DueDate = saleOrder.DueDate,
                InvoiceDate = saleOrder.InvoiceDate,
                term_fk = saleOrder.term_fk,
                Term = saleOrder.Term,
                InvoiceNumber = saleOrder.InvoiceNumber,
                Cashier_fk = saleOrder.Cashier_fk,
                Customer_fk = saleOrder.Customer_fk,
                Customer = saleOrder.Customer,
                Warehouse_fk = saleOrder.Warehouse.Id,
                ShipMethod_fk = saleOrder.ShipMethod_fk,
                Subtotal = saleOrder.Subtotal,
                SoTotalPrice = saleOrder.SoTotalPrice,
                TaxArea_fk = saleOrder.TaxArea_fk,
                Tax = saleOrder.Tax,
                Handling = saleOrder.Handling,
                Freight = saleOrder.Freight,
                TotalDiscount = saleOrder.TotalDiscount,
                TrackingNo = saleOrder.TrackingNo,
                BillingAddress = saleOrder.BillingAddress,
                ShippingAddress = saleOrder.ShippingAddress,
                ShipVia = saleOrder.ShipVia,
                MessageOnInvoice = saleOrder.MessageOnInvoice,
                MessageOnStatment = saleOrder.MessageOnStatment,
                Note = saleOrder.Note,
                Quantity = saleOrder.Quantity,
                TermPercent = saleOrder.TermPercent,
                IsDeleted = false,
                SoItems = new ObservableCollection<SoItemVeiwModel>(listSoItem),
                User = saleOrder.User
            };
        }

        public List<SalesOrderListview> SalesOrdersListView()
        {
            return _context.saleorders.Select(p => new SalesOrderListview() { No = p.Id, CustomerName = p.Customer.CompanyName, DueDate = p.DueDate, Balance =0, TotalBeforeTax = p.Subtotal,Total = p.SoTotalPrice}).ToList();

        }

        public bool SendEmailOrPrint(SaleOrderViewModel saleOrder, bool IsPrint = false)
        {
            saleOrder.Id = _context.saleorders.Max(p => p.Id) + 1;
            if (saleOrder.Id == null)
                saleOrder.Id = 345;
            ResrevdItemsOfSaleOrder(saleOrder.Id);
            SaveAndUpdateSaleOrder(saleOrder);

            return true;
        }

        public bool CreateInvoice(SaleOrderViewModel saleOrder)
        {
            saleOrder.InvoiceNumber = _context.saleorders.Max(p => p.InvoiceNumber) + 1;
            if (saleOrder.InvoiceNumber == null)
                saleOrder.InvoiceNumber = 890;
            //Edit Code Later
            var soItem = _context.soitems.Where(p => p.So_fk == saleOrder.Id).Include(p => p.ProductMaster).ThenInclude(e => e.ProductInventoryWarehouses).ToList();
            ProductInventoryWarehouse inventoryWarehouse;
            foreach (SoItem item in soItem)
            {
                if (saleOrder.Id != null)
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
            DetachedAllEntries();
            refund.RefundDate = DateTime.Now;
            refund.RefundNumber = 1368;
            _context.refunds.Add(refund);

            var inventorys = _context.productinventorywarehouses.Where(p => p.Warehouse_fk == refund.WarehouseId.Value).ToList();
            var SoItems = _context.soitems.Where(p => p.So_fk == refund.SaleOrder_fk);
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
            var soItem = _context.soitems.Where(p => p.So_fk == Id).Include(p => p.ProductMaster).ToList();

            foreach (SoItem item in soItem)
            {
                item.ProductMaster.GoodsReserved += item.Quantity;
            }
        }
    }
}
