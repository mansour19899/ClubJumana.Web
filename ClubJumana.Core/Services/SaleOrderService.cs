using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
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
        private OnlineContext _context;
        public SaleOrderService()
        {
            _context = new OnlineContext();
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
            bool IsInvoice = false;
            int? PreviousWarehouse = 0;
            if (saleOrder.InvoiceNumber != null)
                IsInvoice = true;
            SaleOrder So = new SaleOrder();
            int newIdSo = _context.saleorders.Count() > 0 ? _context.saleorders.Max(p => p.Id) + 1 : 1;

            if (saleOrder.AmountDeposit != 0)
                saleOrder.HaveDeposit = true;

            if (saleOrder.Id == 0)
            {
                So = new SaleOrder()
                {
                    Id = newIdSo,
                    Type = saleOrder.Type,
                    HaveDeposit = saleOrder.HaveDeposit,
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
                    Handling = saleOrder.Handling,
                    HandlingTaxCode = Convert.ToByte(saleOrder.HandlingTaxCode),
                    Shipping = saleOrder.Shipping,
                    ShippingTaxCode = Convert.ToByte(saleOrder.ShippingTaxCode),
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
                    OpenBalance = saleOrder.OpenBalance,
                    Taxes = new List<Tax>()
                };
                _context.saleorders.Add(So);

                _context.SaveChanges();

            }
            else
            {
                So = _context.saleorders.Include(p => p.Taxes).Include(p => p.PaymentInvoices).ThenInclude(p => p.Payment).SingleOrDefault(p => p.Id == saleOrder.Id);
                PreviousWarehouse = So.Warehouse_fk;
                So.Type = saleOrder.Type;
                So.HaveDeposit = saleOrder.HaveDeposit;
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
                So.Handling = saleOrder.Handling;
                So.HandlingTaxCode = Convert.ToByte(saleOrder.HandlingTaxCode);
                So.Shipping = saleOrder.Shipping;
                So.ShippingTaxCode = Convert.ToByte(saleOrder.ShippingTaxCode);
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
                So.OpenBalance = saleOrder.OpenBalance;

                if (saleOrder.Deposit.Id != 0 && saleOrder.HaveDeposit)
                {
                    So.PaymentInvoices.OrderBy(p => p.Id).ElementAt(0).Payment.AmountReceived =
                        saleOrder.Deposit.AmountReceived;
                    So.PaymentInvoices.OrderBy(p => p.Id).ElementAt(0).Payment.DepositToFK =
                        saleOrder.Deposit.DepositToFK;
                    So.PaymentInvoices.OrderBy(p => p.Id).ElementAt(0).Payment.PaymentMethodFK =
                        saleOrder.Deposit.PaymentMethodFK;
                    So.PaymentInvoices.OrderBy(p => p.Id).ElementAt(0).Payment.ReferenceNo =
                        saleOrder.Deposit.ReferenceNo;
                    So.PaymentInvoices.OrderBy(p => p.Id).ElementAt(0).Payment.PaymentDate = DateTime.Now;
                    So.PaymentInvoices.OrderBy(p => p.Id).ElementAt(0).Amount = saleOrder.Deposit.AmountReceived;

                }
            }

            SoItem soItem = new SoItem();
            ProductInventoryWarehouse inventoryProduct;
            ProductInventoryWarehouse OldInventoryProduct;
            int def = 0;
            int newId = 1;
            try
            {
                newId = _context.soitems.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                newId = 1;
            }
            foreach (var model in saleOrder.SoItems)
            {
                if (model.Id == 0 && !model.IsDeleted)
                {
                    _context.soitems.Add(new SoItem()
                    {
                        Id = newId,
                        So_fk = So.Id,
                        ProductMaster_fk = model.ProductMaster_fk,
                        Cost = model.Cost,
                        Discount = model.Discount,
                        Quantity = model.Quantity,
                        Price = model.Price,
                        TotalPrice = model.TotalPrice,
                        TaxCode = Convert.ToByte(model.TaxCode)
                    });
                    model.Id = newId;
                    newId++;
                    if (IsInvoice)
                    {
                        inventoryProduct = _context.productinventorywarehouses.Include(p => p.ProductMaster)
                            .FirstOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == model.ProductMaster_fk);
                        if (inventoryProduct == null)
                        {
                            AddProductInventory();
                            var tttt = _context.productmasters.FirstOrDefault(p => p.Id == model.ProductMaster_fk);
                            tttt.StockOnHand -= model.Quantity;
                            tttt.Outcome += model.Quantity;
                            _context.productmasters.Update(tttt);
                        }

                        else
                        {
                            inventoryProduct.Inventory -= model.Quantity;
                            inventoryProduct.OutCome += model.Quantity;
                            inventoryProduct.ProductMaster.StockOnHand -= model.Quantity;
                            inventoryProduct.ProductMaster.Outcome += model.Quantity;
                            _context.productinventorywarehouses.Update(inventoryProduct);
                            _context.productmasters.Update(inventoryProduct.ProductMaster);
                        }

                    }
                    else
                    {
                        _context.productmasters.FirstOrDefault(p => p.Id == model.ProductMaster_fk).GoodsReserved +=
                            model.Quantity;
                    }


                }
                else if (model.Id != 0 && model.IsDeleted)
                {
                    soItem = _context.soitems.Include(p => p.ProductMaster).SingleOrDefault(p => p.Id == model.Id);
                    if (IsInvoice)
                    {
                        inventoryProduct = _context.productinventorywarehouses.Include(p => p.ProductMaster)
                            .FirstOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == model.ProductMaster_fk);
                        inventoryProduct.Inventory += model.Quantity;
                        inventoryProduct.OutCome -= model.Quantity;
                        inventoryProduct.ProductMaster.StockOnHand += model.Quantity;
                        inventoryProduct.ProductMaster.Outcome -= model.Quantity;
                        _context.productinventorywarehouses.Update(inventoryProduct);
                        _context.productmasters.Update(inventoryProduct.ProductMaster);
                    }
                    else
                    {
                        soItem.ProductMaster.GoodsReserved -= model.Quantity;
                    }

                    _context.Remove(soItem);
                }
                else if (model.Id != 0)
                {
                    soItem = _context.soitems.Include(p => p.ProductMaster).ThenInclude(p => p.ProductInventoryWarehouses)
                        .SingleOrDefault(p => p.Id == model.Id);
                    soItem.Cost = model.Cost;
                    soItem.Discount = model.Discount;
                    soItem.Price = model.Price;
                    soItem.TotalPrice = model.TotalPrice;
                    soItem.TaxCode = Convert.ToByte(model.TaxCode);

                    if (soItem.Quantity != model.Quantity || So.Warehouse_fk != PreviousWarehouse)
                    {
                        def = model.Quantity - soItem.Quantity;
                        if (IsInvoice)
                        {
                            inventoryProduct = soItem.ProductMaster.ProductInventoryWarehouses.FirstOrDefault(p =>
                                  p.Warehouse_fk == saleOrder.Warehouse_fk);

                            if (inventoryProduct == null)
                            {
                                AddProductInventory();
                            }
                            else if (So.Warehouse_fk != PreviousWarehouse)
                            {
                                OldInventoryProduct = soItem.ProductMaster.ProductInventoryWarehouses.FirstOrDefault(p =>
                                    p.Warehouse_fk == PreviousWarehouse);
                                if (OldInventoryProduct != null)
                                {
                                    OldInventoryProduct.Inventory += soItem.Quantity;
                                    OldInventoryProduct.OutCome -= soItem.Quantity;
                                    _context.productinventorywarehouses.Update(OldInventoryProduct);
                                }
                                inventoryProduct.Inventory -= model.Quantity;
                                inventoryProduct.OutCome += model.Quantity;
                                _context.productinventorywarehouses.Update(inventoryProduct);
                            }
                            else
                            {
                                inventoryProduct.Inventory -= def;
                                inventoryProduct.OutCome += def;
                                _context.productinventorywarehouses.Update(inventoryProduct);
                            }

                            soItem.Quantity = model.Quantity;
                            soItem.ProductMaster.StockOnHand -= def;
                            soItem.ProductMaster.Outcome += def;
                        }
                        else
                        {
                            soItem.ProductMaster.GoodsReserved -= soItem.Quantity;
                            soItem.Quantity = model.Quantity;
                            soItem.ProductMaster.GoodsReserved += model.Quantity;
                        }
                        _context.productmasters.Update(soItem.ProductMaster);
                    }

                }
                else
                {

                }

                void AddProductInventory()
                {
                    int newIdd = _context.productinventorywarehouses.Max(p => p.Id) + 1;
                    inventoryProduct = new ProductInventoryWarehouse()
                    {
                        Id = newIdd,
                        Warehouse_fk = saleOrder.Warehouse_fk.Value,
                        ProductMaster_fk = model.ProductMaster_fk,
                        Income = 0,
                        OutCome = model.Quantity,
                        OnTheWayInventory = 0,
                        RefundQuantity = 0,
                        Inventory = -1 * model.Quantity
                    };
                    _context.productinventorywarehouses.Add(inventoryProduct);
                }
            }

            Tax tax = new Tax();
            int newIdTax = 1;
            try
            {
                newIdTax = _context.taxes.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                newIdTax = 1;
            }

            int SoTaxesOld = So.Taxes.Count;
            int IndexTax = 0;
            foreach (var model in saleOrder.Taxes)
            {
                if (IndexTax >= SoTaxesOld)
                {
                    _context.taxes.Add(new Tax()
                    {
                        Id = newIdTax,
                        Code = model.Code,
                        TaxAmount = model.TaxAmount,
                        Amount = model.Amount,
                        SalesOrderFK = So.Id,
                        Rate = model.Rate,

                    });
                    newIdTax++;
                }
                else if (IndexTax < SoTaxesOld)
                {
                    So.Taxes.ElementAt(IndexTax).Code = model.Code;
                    So.Taxes.ElementAt(IndexTax).TaxAmount = model.TaxAmount;
                    So.Taxes.ElementAt(IndexTax).Amount = model.Amount;
                    So.Taxes.ElementAt(IndexTax).SalesOrderFK = So.Id;
                    So.Taxes.ElementAt(IndexTax).Rate = model.Rate;
                    IndexTax++;
                }
            }

            if (IndexTax < SoTaxesOld)
            {
                for (int i = IndexTax; i < SoTaxesOld; i++)
                {
                    _context.taxes.Remove(So.Taxes.ElementAt(i));
                }
            }


            if (saleOrder.Deposit.Id == 0 && saleOrder.AmountDeposit != 0)
            {
                saleOrder.Deposit.Note = "Deposit";
                try
                {
                    saleOrder.Deposit.Id = _context.payments.Max(p => p.Id) + 1;
                }
                catch (Exception e)
                {
                    saleOrder.Deposit.Id = 1;
                }
                int IdInvoicePayment = 1;
                try
                {
                    IdInvoicePayment = _context.paymentinvoices.Max(p => p.Id) + 1;
                }
                catch (Exception e)
                {
                    IdInvoicePayment = 1;
                }
                saleOrder.Deposit.PaymentInvoices.Add(new PaymentInvoice() { Id = IdInvoicePayment, InvoiceFK = So.Id, PaymenteFK = saleOrder.Deposit.Id, Amount = saleOrder.AmountDeposit });
                _context.payments.Add(saleOrder.Deposit);
            }

            _context.SaveChanges();

            return So.Id;

        }

        public int AddSalesOrder(SaleOrder saleOrder, bool isSave = true)
        {
            var duplicate = new List<int>();
            if (isSave)
                DetachedAllEntries();
            if (saleOrder.Id == 0)
            {
                //product.Id = _context.customers.Max(p => p.Id) + 1;
                //_context.productmasters.Add(product);
            }
            else if (!isSave)
            {

                int itemId = _context.soitems.Max(p => p.Id);
                foreach (var VARIABLE in saleOrder.SoItems)
                {
                    itemId++;
                    VARIABLE.Id = itemId;
                }
                _context.saleorders.Add(saleOrder);
            }

            else
            {
                ProductInventoryWarehouse inventoryWarehouse;
                ProductMaster productMaster;
                int InvReportId = (_context.inventoryreports.Count() > 0)
                    ? _context.inventoryreports.Max(p => p.Id)
                    : 0;
                int ProductInventoryId = (_context.productinventorywarehouses.Count() > 0)
                    ? _context.productinventorywarehouses.Max(p => p.Id)
                    : 0;
                int itemId = 0;
                if (_context.soitems.Count() != 0)
                    itemId = _context.soitems.Max(p => p.Id);

                int QuantityItemSalesOrder = 0;
                //Check Later 

                foreach (var item in saleOrder.SoItems)
                {
                    if (saleOrder.SoItems.Where(p => p.ProductMaster_fk == item.ProductMaster_fk).ToList().Count > 1)
                        duplicate.Add(item.ProductMaster_fk);
                }

                if (duplicate.Count > 0)
                {
                    var duplicateId = duplicate.Distinct().ToList();

                    foreach (var VARIABLE in duplicateId)
                    {
                        var ttr = saleOrder.SoItems.Where(p => p.ProductMaster_fk == VARIABLE).ToList();
                        foreach (var dupi in ttr.Skip(1).ToList())
                        {
                            itemId++;
                            ttr.ElementAt(0).Id = itemId++;
                            ttr.ElementAt(0).Quantity += dupi.Quantity;
                            ttr.ElementAt(0).TotalPrice += dupi.TotalPrice;
                            if (ttr.ElementAt(0).Price != dupi.Price)
                                ttr.ElementAt(0).Price = ttr.ElementAt(0).Price + dupi.Price / 2;
                            saleOrder.SoItems.Remove(dupi);
                        }

                    }
                }

                foreach (var VARIABLE in saleOrder.SoItems)
                {

                    if (_context.productmasters.FirstOrDefault(p => p.Id == VARIABLE.ProductMaster_fk) == null)
                        return -11;

                    else
                    {
                        InvReportId++;
                        itemId++;
                        VARIABLE.Id = itemId;
                        productMaster = _context.productmasters.FirstOrDefault(P => P.Id == VARIABLE.ProductMaster_fk);
                        _context.inventoryreports.Add(new InventoryReport()
                        {
                            Id = InvReportId,
                            ProductMasterFK = VARIABLE.ProductMaster_fk,
                            Description = "Invoice :" + saleOrder.DocNumber +"," + saleOrder.LastUpdateTime.ToShortDateString(),
                            Change = -1*VARIABLE.Quantity,
                            OldBalance = productMaster.StockOnHand,
                            NewBalance = productMaster.StockOnHand - VARIABLE.Quantity
                        });

                        productMaster.StockOnHand -= VARIABLE.Quantity;
                        productMaster.Outcome += VARIABLE.Quantity;
                        inventoryWarehouse = _context.productinventorywarehouses.SingleOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk&&p.ProductMaster_fk==VARIABLE.ProductMaster_fk);
                        if (inventoryWarehouse == null)
                        {
                            ProductInventoryId++;
                            _context.productinventorywarehouses.Add(new ProductInventoryWarehouse()
                            {
                                Id = ProductInventoryId,
                                ProductMaster_fk = VARIABLE.ProductMaster_fk,
                                Warehouse_fk = saleOrder.Warehouse_fk.Value,
                                Inventory = -1 * VARIABLE.Quantity,
                                OutCome = VARIABLE.Quantity,
                                OnTheWayInventory = 0,
                                RefundQuantity = 0,
                            });
                        }

                        else
                        {
                            inventoryWarehouse.Inventory -= VARIABLE.Quantity;
                            inventoryWarehouse.OutCome += VARIABLE.Quantity;
                        }

                        QuantityItemSalesOrder += VARIABLE.Quantity;

                    }
                }


                saleOrder.Quantity = QuantityItemSalesOrder;

                _context.saleorders.Add(saleOrder);
            }
            if (isSave)
                _context.SaveChanges();
            return duplicate.Count>0?-14:1;
        }


        public int UpdateSalesOrder(SaleOrder saleOrder, bool isSave = true)
        {
            DetachedAllEntries();
            bool IsInvoice = false;
            int? PreviousWarehouse = 0;
            if (saleOrder.InvoiceNumber != null)
                IsInvoice = true;

            var So = _context.saleorders.Include(p=>p.SoItems).SingleOrDefault(p => p.Id == saleOrder.Id);

           PreviousWarehouse = So.Warehouse_fk;
           So.Type = saleOrder.Type;
           So.HaveDeposit = saleOrder.HaveDeposit;
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
           So.Handling = saleOrder.Handling;
           So.HandlingTaxCode = Convert.ToByte(saleOrder.HandlingTaxCode);
           So.Shipping = saleOrder.Shipping;
           So.ShippingTaxCode = Convert.ToByte(saleOrder.ShippingTaxCode);
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
           So.OpenBalance = saleOrder.OpenBalance;
           So.LastUpdateTime = saleOrder.LastUpdateTime;
           So.HomeBalance = saleOrder.HomeBalance;
           So.HomeTotalAmt = saleOrder.HomeTotalAmt;
           So.ExchangeRate = saleOrder.ExchangeRate;
           So.DocNumber = saleOrder.DocNumber;
           So.CustomerMemo = saleOrder.CustomerMemo;
           So.Currency = saleOrder.Currency;
           So.BillEmail = saleOrder.BillEmail;


            SoItem soItem = new SoItem();
            ProductInventoryWarehouse inventoryProduct;
            ProductInventoryWarehouse OldInventoryProduct;
            int def = 0;
            int newId = _context.soitems.Count()>0? _context.soitems.Max(p => p.Id) + 1:1;


            var deletedItemList = So.SoItems.Select(p => p.ProductMaster_fk)
                .Except(saleOrder.SoItems.Select(p => p.ProductMaster_fk)).ToList();

            var addedItemList = saleOrder.SoItems.Select(p => p.ProductMaster_fk)
                .Except(So.SoItems.Select(p => p.ProductMaster_fk)).ToList();

            var ItemRemindList = So.SoItems.Select(p => p.ProductMaster_fk).Except(deletedItemList).Except(addedItemList)
                .ToList();
            SoItem SoItemForAdd;
            SoItem SoItemForDelete;
            SoItem SoItemOldForEdit;
            SoItem SoItemNewForEdit;


            ProductInventoryWarehouse inventoryWarehouse;
            ProductMaster productMaster;
            int InvReportId = (_context.inventoryreports.Count() > 0)
                ? _context.inventoryreports.Max(p => p.Id)
                : 0;
            int ProductInventoryId = (_context.productinventorywarehouses.Count() > 0)
                ? _context.productinventorywarehouses.Max(p => p.Id)
                : 0;
            int itemId = 0;
            if (_context.soitems.Count() != 0)
                itemId = _context.soitems.Max(p => p.Id);
            var duplicate = new List<int>();



            foreach (var VARIABLE in addedItemList)
            {
                itemId++;
                SoItemForAdd= saleOrder.SoItems.FirstOrDefault(p => p.ProductMaster_fk == VARIABLE);
                if (SoItemForAdd == null)
                    return -14;
                _context.soitems.Add(new SoItem()
                {
                    Id = itemId,
                    So_fk = SoItemForAdd.So_fk,
                    ProductMaster_fk = SoItemForAdd.ProductMaster_fk,
                    Cost = SoItemForAdd.Cost,
                    Discount = SoItemForAdd.Discount,
                    Quantity = SoItemForAdd.Quantity,
                    QuantityRefunded = SoItemForAdd.QuantityRefunded,
                    IsAbaleToRefund = SoItemForAdd.IsAbaleToRefund,
                    Price = SoItemForAdd.Price,
                    TotalPrice = SoItemForAdd.TotalPrice,
                    TaxCode = SoItemForAdd.TaxCode
                });

                InvReportId++;
                productMaster = _context.productmasters.FirstOrDefault(P => P.Id == VARIABLE);
                _context.inventoryreports.Add(new InventoryReport()
                {
                    Id = InvReportId,
                    ProductMasterFK = VARIABLE,
                    Description = "Invoice :" + saleOrder.DocNumber+","+saleOrder.LastUpdateTime.ToShortDateString(),
                    Change = -1 * SoItemForAdd.Quantity,
                    OldBalance = productMaster.StockOnHand,
                    NewBalance = productMaster.StockOnHand - SoItemForAdd.Quantity
                });

                productMaster.StockOnHand -= SoItemForAdd.Quantity;
                productMaster.Outcome += SoItemForAdd.Quantity;
                inventoryWarehouse = _context.productinventorywarehouses.SingleOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == VARIABLE);
                if (inventoryWarehouse == null)
                {
                    ProductInventoryId++;
                    _context.productinventorywarehouses.Add(new ProductInventoryWarehouse()
                    {
                        Id = ProductInventoryId,
                        ProductMaster_fk = VARIABLE,
                        Warehouse_fk = saleOrder.Warehouse_fk.Value,
                        Inventory = -1 * SoItemForAdd.Quantity,
                        OutCome = SoItemForAdd.Quantity,
                        OnTheWayInventory = 0,
                        RefundQuantity = 0,
                    });
                }

                else
                {
                    inventoryWarehouse.Inventory -= SoItemForAdd.Quantity;
                    inventoryWarehouse.OutCome += SoItemForAdd.Quantity;
                }


            }

            foreach (var VARIABLE in deletedItemList)
            {
                itemId++;
                SoItemForDelete = So.SoItems.FirstOrDefault(p => p.ProductMaster_fk == VARIABLE);
                if (SoItemForDelete == null)
                    return -14;
                _context.soitems.Remove(SoItemForDelete);

                InvReportId++;
                productMaster = _context.productmasters.FirstOrDefault(P => P.Id == VARIABLE);
                _context.inventoryreports.Add(new InventoryReport()
                {
                    Id = InvReportId,
                    ProductMasterFK = VARIABLE,
                    Description = "Invoice :" + saleOrder.DocNumber + "," + saleOrder.LastUpdateTime.ToShortDateString(),
                    Change = SoItemForDelete.Quantity,
                    OldBalance = productMaster.StockOnHand,
                    NewBalance = productMaster.StockOnHand + SoItemForDelete.Quantity
                });

                productMaster.StockOnHand += SoItemForDelete.Quantity;
                productMaster.Outcome -= SoItemForDelete.Quantity;
                inventoryWarehouse = _context.productinventorywarehouses.SingleOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == VARIABLE);
                inventoryWarehouse.Inventory -= SoItemForDelete.Quantity;
                inventoryWarehouse.OutCome += SoItemForDelete.Quantity;
            }


            foreach (var VARIABLE in ItemRemindList)
            {

                SoItemOldForEdit = So.SoItems.FirstOrDefault(p => p.ProductMaster_fk == VARIABLE);
                SoItemNewForEdit = saleOrder.SoItems.FirstOrDefault(p => p.ProductMaster_fk == VARIABLE);
                if (SoItemOldForEdit == null||SoItemNewForEdit==null)
                    return -14;
                if (SoItemOldForEdit.Quantity != SoItemNewForEdit.Quantity||SoItemOldForEdit.Price!=SoItemNewForEdit.Price)
                {
                    def = SoItemNewForEdit.Quantity - SoItemOldForEdit.Quantity;
                    SoItemOldForEdit.Quantity = SoItemNewForEdit.Quantity;
                    SoItemOldForEdit.Cost = SoItemNewForEdit.Cost;
                    SoItemOldForEdit.Discount = SoItemNewForEdit.Discount;
                    SoItemOldForEdit.Price = SoItemNewForEdit.Price;
                    SoItemOldForEdit.TotalPrice = SoItemNewForEdit.TotalPrice;
                    SoItemOldForEdit.TaxCode = SoItemNewForEdit.TaxCode;

                    _context.soitems.Update(SoItemOldForEdit);
                }

                if (def != 0)
                {
                    InvReportId++;
                    productMaster = _context.productmasters.FirstOrDefault(P => P.Id == VARIABLE);
                    _context.inventoryreports.Add(new InventoryReport()
                    {
                        Id = InvReportId,
                        ProductMasterFK = VARIABLE,
                        Description = "Invoice :" + saleOrder.DocNumber + "," + saleOrder.LastUpdateTime.ToShortDateString() + "- Change Quantity",
                        Change = -1*def,
                        OldBalance = productMaster.StockOnHand,
                        NewBalance = productMaster.StockOnHand - def
                    });

                    productMaster.StockOnHand -= def;
                    productMaster.Outcome += def;
                    inventoryWarehouse = _context.productinventorywarehouses.SingleOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == VARIABLE);
                    inventoryWarehouse.Inventory -= def;
                    inventoryWarehouse.OutCome += def;
                }


            }




            _context.SaveChanges();
            //foreach (var model in saleOrder.SoItems)

            //{
            //    if (model.Id == 0 && !model.IsDeleted)
            //    {
            //        _context.soitems.Add(new SoItem()
            //        {
            //            Id = newId,
            //            So_fk = So.Id,
            //            ProductMaster_fk = model.ProductMaster_fk,
            //            Cost = model.Cost,
            //            Discount = model.Discount,
            //            Quantity = model.Quantity,
            //            Price = model.Price,
            //            TotalPrice = model.TotalPrice,
            //            TaxCode = Convert.ToByte(model.TaxCode)
            //        });
            //        model.Id = newId;
            //        newId++;
            //        if (IsInvoice)
            //        {
            //            inventoryProduct = _context.productinventorywarehouses.Include(p => p.ProductMaster)
            //                .FirstOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == model.ProductMaster_fk);
            //            if (inventoryProduct == null)
            //            {
            //                AddProductInventory();
            //                var tttt = _context.productmasters.FirstOrDefault(p => p.Id == model.ProductMaster_fk);
            //                tttt.StockOnHand -= model.Quantity;
            //                tttt.Outcome += model.Quantity;
            //                _context.productmasters.Update(tttt);
            //            }

            //            else
            //            {
            //                inventoryProduct.Inventory -= model.Quantity;
            //                inventoryProduct.OutCome += model.Quantity;
            //                inventoryProduct.ProductMaster.StockOnHand -= model.Quantity;
            //                inventoryProduct.ProductMaster.Outcome += model.Quantity;
            //                _context.productinventorywarehouses.Update(inventoryProduct);
            //                _context.productmasters.Update(inventoryProduct.ProductMaster);
            //            }

            //        }
            //        else
            //        {
            //            _context.productmasters.FirstOrDefault(p => p.Id == model.ProductMaster_fk).GoodsReserved +=
            //                model.Quantity;
            //        }


            //    }
            //    else if (model.Id != 0 && model.IsDeleted)
            //    {
            //        soItem = _context.soitems.Include(p => p.ProductMaster).SingleOrDefault(p => p.Id == model.Id);
            //        if (IsInvoice)
            //        {
            //            inventoryProduct = _context.productinventorywarehouses.Include(p => p.ProductMaster)
            //                .FirstOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == model.ProductMaster_fk);
            //            inventoryProduct.Inventory += model.Quantity;
            //            inventoryProduct.OutCome -= model.Quantity;
            //            inventoryProduct.ProductMaster.StockOnHand += model.Quantity;
            //            inventoryProduct.ProductMaster.Outcome -= model.Quantity;
            //            _context.productinventorywarehouses.Update(inventoryProduct);
            //            _context.productmasters.Update(inventoryProduct.ProductMaster);
            //        }
            //        else
            //        {
            //            soItem.ProductMaster.GoodsReserved -= model.Quantity;
            //        }

            //        _context.Remove(soItem);
            //    }
            //    else if (model.Id != 0)
            //    {
            //        soItem = _context.soitems.Include(p => p.ProductMaster).ThenInclude(p => p.ProductInventoryWarehouses)
            //            .SingleOrDefault(p => p.Id == model.Id);
            //        soItem.Cost = model.Cost;
            //        soItem.Discount = model.Discount;
            //        soItem.Price = model.Price;
            //        soItem.TotalPrice = model.TotalPrice;
            //        soItem.TaxCode = Convert.ToByte(model.TaxCode);

            //        if (soItem.Quantity != model.Quantity || So.Warehouse_fk != PreviousWarehouse)
            //        {
            //            def = model.Quantity - soItem.Quantity;
            //            if (IsInvoice)
            //            {
            //                inventoryProduct = soItem.ProductMaster.ProductInventoryWarehouses.FirstOrDefault(p =>
            //                      p.Warehouse_fk == saleOrder.Warehouse_fk);

            //                if (inventoryProduct == null)
            //                {
            //                    AddProductInventory();
            //                }
            //                else if (So.Warehouse_fk != PreviousWarehouse)
            //                {
            //                    OldInventoryProduct = soItem.ProductMaster.ProductInventoryWarehouses.FirstOrDefault(p =>
            //                        p.Warehouse_fk == PreviousWarehouse);
            //                    if (OldInventoryProduct != null)
            //                    {
            //                        OldInventoryProduct.Inventory += soItem.Quantity;
            //                        OldInventoryProduct.OutCome -= soItem.Quantity;
            //                        _context.productinventorywarehouses.Update(OldInventoryProduct);
            //                    }
            //                    inventoryProduct.Inventory -= model.Quantity;
            //                    inventoryProduct.OutCome += model.Quantity;
            //                    _context.productinventorywarehouses.Update(inventoryProduct);
            //                }
            //                else
            //                {
            //                    inventoryProduct.Inventory -= def;
            //                    inventoryProduct.OutCome += def;
            //                    _context.productinventorywarehouses.Update(inventoryProduct);
            //                }

            //                soItem.Quantity = model.Quantity;
            //                soItem.ProductMaster.StockOnHand -= def;
            //                soItem.ProductMaster.Outcome += def;
            //            }
            //            else
            //            {
            //                soItem.ProductMaster.GoodsReserved -= soItem.Quantity;
            //                soItem.Quantity = model.Quantity;
            //                soItem.ProductMaster.GoodsReserved += model.Quantity;
            //            }
            //            _context.productmasters.Update(soItem.ProductMaster);
            //        }

            //    }
            //    else
            //    {

            //    }

            //    void AddProductInventory()
            //    {
            //        int newIdd = _context.productinventorywarehouses.Max(p => p.Id) + 1;
            //        inventoryProduct = new ProductInventoryWarehouse()
            //        {
            //            Id = newIdd,
            //            Warehouse_fk = saleOrder.Warehouse_fk.Value,
            //            ProductMaster_fk = model.ProductMaster_fk,
            //            Income = 0,
            //            OutCome = model.Quantity,
            //            OnTheWayInventory = 0,
            //            RefundQuantity = 0,
            //            Inventory = -1 * model.Quantity
            //        };
            //        _context.productinventorywarehouses.Add(inventoryProduct);
            //    }
            //}

            //Tax tax = new Tax();
            //int newIdTax = 1;
            //try
            //{
            //    newIdTax = _context.taxes.Max(p => p.Id) + 1;
            //}
            //catch (Exception e)
            //{
            //    newIdTax = 1;
            //}

            //int SoTaxesOld = So.Taxes.Count;
            //int IndexTax = 0;
            //foreach (var model in saleOrder.Taxes)
            //{
            //    if (IndexTax >= SoTaxesOld)
            //    {
            //        _context.taxes.Add(new Tax()
            //        {
            //            Id = newIdTax,
            //            Code = model.Code,
            //            TaxAmount = model.TaxAmount,
            //            Amount = model.Amount,
            //            SalesOrderFK = So.Id,
            //            Rate = model.Rate,

            //        });
            //        newIdTax++;
            //    }
            //    else if (IndexTax < SoTaxesOld)
            //    {
            //        So.Taxes.ElementAt(IndexTax).Code = model.Code;
            //        So.Taxes.ElementAt(IndexTax).TaxAmount = model.TaxAmount;
            //        So.Taxes.ElementAt(IndexTax).Amount = model.Amount;
            //        So.Taxes.ElementAt(IndexTax).SalesOrderFK = So.Id;
            //        So.Taxes.ElementAt(IndexTax).Rate = model.Rate;
            //        IndexTax++;
            //    }
            //}

            //if (IndexTax < SoTaxesOld)
            //{
            //    for (int i = IndexTax; i < SoTaxesOld; i++)
            //    {
            //        _context.taxes.Remove(So.Taxes.ElementAt(i));
            //    }
            //}




            return 1;
        }

        public int UpdateInvoice(SaleOrderViewModel saleOrder)
        {
            DetachedAllEntries();
            SaleOrder So = new SaleOrder();

            So = _context.saleorders.SingleOrDefault(p => p.Id == saleOrder.Id);
            So.Type = saleOrder.Type;
            So.HaveDeposit = saleOrder.HaveDeposit;
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
            So.Taxes = saleOrder.Taxes;
            So.Handling = saleOrder.Handling;
            So.HandlingTaxCode = Convert.ToByte(saleOrder.HandlingTaxCode);
            So.Shipping = saleOrder.Shipping;
            So.ShippingTaxCode = Convert.ToByte(saleOrder.ShippingTaxCode);
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
            So.OpenBalance = saleOrder.OpenBalance;

            SoItem soItem = new SoItem();
            ProductInventoryWarehouse inventoryProduct;
            int def = 0;

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
                    inventoryProduct = _context.productinventorywarehouses.Include(p => p.ProductMaster)
                       .FirstOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk);
                    inventoryProduct.Inventory -= model.Quantity;
                    inventoryProduct.OutCome += model.Quantity;
                    inventoryProduct.ProductMaster.StockOnHand -= model.Quantity;
                    inventoryProduct.ProductMaster.Outcome += model.Quantity;

                    _context.productinventorywarehouses.Update(inventoryProduct);
                    _context.productmasters.Update(inventoryProduct.ProductMaster);

                }
                else if (model.Id != 0 && model.IsDeleted)
                {
                    soItem = _context.soitems.SingleOrDefault(p => p.Id == model.Id);
                    inventoryProduct = _context.productinventorywarehouses.Include(p => p.ProductMaster)
                        .FirstOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk && p.ProductMaster_fk == model.ProductMaster_fk);
                    inventoryProduct.Inventory += model.Quantity;
                    inventoryProduct.OutCome -= model.Quantity;
                    inventoryProduct.ProductMaster.StockOnHand += model.Quantity;
                    inventoryProduct.ProductMaster.Outcome -= model.Quantity;

                    _context.Remove(soItem);
                }
                else if (model.Id != 0)
                {
                    soItem = _context.soitems.Include(p => p.ProductMaster).ThenInclude(p => p.ProductInventoryWarehouses)
                        .SingleOrDefault(p => p.Id == model.Id);
                    soItem.Cost = model.Cost;
                    soItem.Discount = model.Discount;
                    soItem.Price = model.Price;
                    soItem.TotalPrice = model.TotalPrice;
                    if (soItem.Quantity != model.Quantity)
                    {
                        def = model.Quantity - soItem.Quantity;
                        soItem.Quantity = model.Quantity;
                        soItem.ProductMaster.StockOnHand -= def;
                        soItem.ProductMaster.Outcome += def;

                        inventoryProduct = soItem.ProductMaster.ProductInventoryWarehouses.FirstOrDefault(p =>
                            p.Warehouse_fk == saleOrder.Warehouse_fk);
                        inventoryProduct.Inventory -= def;
                        inventoryProduct.OutCome += def;
                        _context.productinventorywarehouses.Update(inventoryProduct);
                        _context.productmasters.Update(soItem.ProductMaster);
                    }

                }
                else
                {

                }
            }

            _context.SaveChanges();

            return So.Id;
        }

        public SaleOrderViewModel GiveSaleOrderById(int id)
        {

            var saleOrder = _context.saleorders.AsNoTracking().Where(p => p.Id == id).Include(p => p.Term)
                .Include(p => p.SoItems).ThenInclude(p => p.ProductMaster).ThenInclude(p => p.ProductInventoryWarehouses)
                  .Include(p => p.Customer).Include(p => p.User)
                .Include(p => p.Warehouse).Include(p => p.Taxes)
                .Include(p => p.PaymentInvoices).ThenInclude(p => p.Payment).Include(p => p.Refunds).SingleOrDefault();

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
                    TermPercent = saleOrder.TermPercent,
                    TaxCode = Convert.ToInt32(VARIABLE.TaxCode)
                });
            }

            Payment Deposit;
            if (saleOrder.HaveDeposit)
            {
                Deposit = saleOrder.PaymentInvoices.OrderBy(p => p.Id).ElementAt(0).Payment;
            }
            else
            {
                Deposit = new Payment() { Id = 0, DepositToFK = 1, PaymentMethodFK = 1, AmountReceived = 0, PaymentInvoices = new List<PaymentInvoice>() };
            }

            return new SaleOrderViewModel()
            {
                Id = saleOrder.Id,
                Type = saleOrder.Type,
                HaveDeposit = saleOrder.HaveDeposit,
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
                Taxes = saleOrder.Taxes.ToList(),
                Handling = saleOrder.Handling,
                HandlingTaxCode = saleOrder.HandlingTaxCode,
                Shipping = saleOrder.Shipping,
                ShippingTaxCode = saleOrder.ShippingTaxCode,
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
                User = saleOrder.User,
                OpenBalance = saleOrder.OpenBalance,
                Deposit = Deposit,
                Refunds = saleOrder.Refunds
            };
        }

        public List<SalesOrderListview> SalesOrdersListView()
        {
            return _context.saleorders.Where(p => p.InvoiceNumber == null).Select(p => new SalesOrderListview() { No = p.Id, CustomerName = p.Customer.CompanyName, DueDate = p.DueDate, OpenBalance = 0, TotalBeforeTax = p.Subtotal, Total = p.SoTotalPrice }).OrderByDescending(p => p.No).ToList();

        }
        public List<SalesOrderListview> SalesInvoceListView()
        {
            return _context.saleorders.Where(p => p.InvoiceNumber != null).Select(p => new SalesOrderListview() { No = p.Id, CustomerName = p.Customer.CompanyName, DueDate = p.DueDate, OpenBalance = 0, TotalBeforeTax = p.Subtotal, Total = p.SoTotalPrice }).OrderByDescending(p => p.No).ToList();

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

        public int CreateInvoice(int Id)
        {
            DetachedAllEntries();
            var SalesOrder = _context.saleorders.FirstOrDefault(p => p.Id == Id);
            SalesOrder.InvoiceNumber = _context.saleorders.Max(p => p.InvoiceNumber) + 1;
            if (SalesOrder.InvoiceNumber == null)
                SalesOrder.InvoiceNumber = 890;
            //Edit Code Later
            var soItem = _context.soitems.Where(p => p.So_fk == SalesOrder.Id).Include(p => p.ProductMaster).ThenInclude(e => e.ProductInventoryWarehouses).ToList();
            ProductInventoryWarehouse inventoryWarehouse;
            foreach (SoItem item in soItem)
            {
                if (SalesOrder.Id != null)
                    item.ProductMaster.GoodsReserved -= item.Quantity;
                item.ProductMaster.StockOnHand -= item.Quantity;
                item.ProductMaster.Outcome += item.Quantity;
                inventoryWarehouse = item.ProductMaster.ProductInventoryWarehouses.SingleOrDefault(p => p.Warehouse_fk == SalesOrder.Warehouse_fk);
                inventoryWarehouse.Inventory -= item.Quantity;
                inventoryWarehouse.OutCome += item.Quantity;

            }
            _context.SaveChanges();
            return SalesOrder.InvoiceNumber.Value;
        }

        public int AddRefund(Refund refund, List<RefundItemsViewModel> refundItemsViewModel)
        {
            DetachedAllEntries();
            int Id = 0;
            int RefundItemId = 0;
            int TaxRefundId = 0;
            try
            {
                Id = _context.refunds.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                Id = 1;
            }
            refund.Id = Id;
            try
            {
                RefundItemId = _context.refunditems.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                RefundItemId = 1;
            }
            try
            {
                TaxRefundId = _context.taxrefunds.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                TaxRefundId = 1;
            }
            refund.RefundItems = new List<RefundItem>();
            foreach (var item in refundItemsViewModel)
            {
                refund.RefundItems.Add(new RefundItem()
                {
                    Id = RefundItemId,
                    Refund_fk = Id,
                    ProductMaster_fk = item.ProductMaster.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalPrice = item.TotalPrice,
                    TaxCodeName = item.TaxCodeName
                });
                RefundItemId++;
            }

            foreach (var item in refund.TaxesRefunds)
            {
                item.Id = TaxRefundId;
                TaxRefundId++;
            }

            _context.refunds.Add(refund);
            ProductInventoryWarehouse x;
            foreach (var item in refund.RefundItems)
            {
                var SoItem = _context.soitems.Include(p => p.ProductMaster).ThenInclude(p => p.ProductInventoryWarehouses).FirstOrDefault(p => p.So_fk == item.Refund.SaleOrderFK && p.ProductMaster_fk == item.ProductMaster_fk);
                SoItem.QuantityRefunded += item.Quantity;
                item.ProductMaster.StockOnHand += item.Quantity;
                item.ProductMaster.RefundQuantity += item.Quantity;

                x = SoItem.ProductMaster.ProductInventoryWarehouses.FirstOrDefault(p =>
                    p.Warehouse_fk == refund.WarehouseId.Value);
                if (x != null)
                {
                    x.Inventory += item.Quantity;
                    x.RefundQuantity += item.Quantity;
                }
                else
                {
                    ProductInventoryWarehouse newProductInventory = new ProductInventoryWarehouse();
                    newProductInventory.Id = _context.productinventorywarehouses.Max(p => p.Id) + 1;
                    newProductInventory.ProductMaster_fk = item.ProductMaster_fk;
                    newProductInventory.Warehouse_fk = refund.WarehouseId.Value;
                    newProductInventory.Inventory = item.Quantity;
                    newProductInventory.RefundQuantity = item.Quantity;
                    newProductInventory.Income = 0;
                    newProductInventory.OutCome = 0;
                    newProductInventory.OnTheWayInventory = 0;
                    _context.productinventorywarehouses.Add(newProductInventory);
                }

            }
            //var inventorys = _context.productinventorywarehouses.Where(p => p.Warehouse_fk == refund.WarehouseId.Value);
            //var SoItems = _context.soitems.Where(p => p.So_fk == refund.SaleOrderFK);

            //foreach (var model in refund.RefundItems)
            //{
            //    model.ProductMaster.StockOnHand += model.Quantity;
            //    model.ProductMaster.RefundQuantity += model.Quantity;

            //    x = inventorys.SingleOrDefault(p => p.ProductMaster_fk == model.ProductMaster_fk);
            //    SoItems.SingleOrDefault(p => p.ProductMaster_fk == model.ProductMaster_fk).QuantityRefunded +=
            //        model.Quantity;
            //    x.Inventory += model.Quantity;
            //    x.RefundQuantity += model.Quantity;
            //}
            _context.SaveChanges();
            return 1;
        }

        public List<RefundItem> CovertToRefundItem(ObservableCollection<RefundItemsViewModel> refundItemsViewModel)
        {
            List<RefundItem> refundItems = new List<RefundItem>();

            foreach (var model in refundItemsViewModel)
            {
                refundItems.Add(new RefundItem()
                {
                    ProductMaster = model.ProductMaster,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    TotalPrice = model.TotalPrice
                });
            }
            return refundItems;
        }

        public List<CustomersInvoiceViewModel> GiveMeAllOpenInvoiceForCustomer(int CustomrId)
        {
            return _context.saleorders.Where(p => p.InvoiceNumber != null && p.IsPaid == false && p.Customer_fk.Value == CustomrId).Select(p => new CustomersInvoiceViewModel()
            { Id = p.Id, InvoiceNumber = p.InvoiceNumber, InvoiceDate = p.InvoiceDate, DueDate = p.DueDate, OrginalAmount = p.SoTotalPrice, OpenBalance = p.OpenBalance, Payment = 0M, IsSelected = false })
                .ToList();
        }


        private void ResrevdItemsOfSaleOrder(int Id)
        {
            //Check
            var soItem = _context.soitems.Where(p => p.So_fk == Id).Include(p => p.ProductMaster).ToList();

            foreach (SoItem item in soItem)
            {
                item.ProductMaster.GoodsReserved += item.Quantity;
            }
        }
    }
}
