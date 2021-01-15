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
            if (saleOrder.InvoiceNumber != null)
                IsInvoice = true;
            SaleOrder So = new SaleOrder();
            int newIdSo = 1;
            try
            {
                newIdSo = _context.saleorders.Max(p => p.Id) + 1;
            }
            catch (Exception e)
            {
                newIdSo = 1;
            }

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
                    TaxArea_fk = saleOrder.TaxArea_fk,
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
                So.TaxArea_fk = saleOrder.TaxArea_fk;
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
                            .FirstOrDefault(p => p.Warehouse_fk == saleOrder.Warehouse_fk);
                        inventoryProduct.Inventory -= model.Quantity;
                        inventoryProduct.OutCome += model.Quantity;
                        inventoryProduct.ProductMaster.StockOnHand -= model.Quantity;
                        inventoryProduct.ProductMaster.Outcome += model.Quantity;
                        _context.productinventorywarehouses.Update(inventoryProduct);
                        _context.productmasters.Update(inventoryProduct.ProductMaster);
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

                    if (soItem.Quantity != model.Quantity)
                    {
                        if (IsInvoice)
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
            So.TaxArea_fk = saleOrder.TaxArea_fk;
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
                .Include(p => p.SoItems).ThenInclude(p => p.ProductMaster).Include(p => p.TaxArea)
                  .Include(p => p.Customer).Include(p => p.User)
                .Include(p => p.Warehouse).Include(p => p.Taxes)
                .Include(p => p.PaymentInvoices).ThenInclude(p => p.Payment).SingleOrDefault();

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
                TaxArea_fk = saleOrder.TaxArea_fk,
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
                Deposit = Deposit
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
