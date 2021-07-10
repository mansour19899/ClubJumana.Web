using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.Core.Generator;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubJumana.Core.Services
{
    public class PerchaseOrderService : IPerchaseOrderService
    {
        private OnlineContext _context;

        public PerchaseOrderService()
        {
            //var optionsBuilder = new DbContextOptionsBuilder<OnlineContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCore-smm28;Trusted_Connection=True");
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
        public int AddOrUpdatePoViewModel(PoViewModel poViewModel, IEnumerable<ItemsOfPurchaseOrderViewModel> items, bool done = false)
        {
            DetachedAllEntries();
            if (poViewModel.CreatedInvoice)
            {
                return 0;
            }
            else
            {
                PurchaseOrder PO = new PurchaseOrder();
                if (poViewModel.Id == 0)
                {
                    int NewId = 1;
                    if (_context.purchaseorders.FirstOrDefault() != null)
                        NewId = _context.purchaseorders.Max(p => p.Id) + 1;

                    PO = new PurchaseOrder()
                    {
                        Id = NewId,
                        Vendor_fk = poViewModel.Vendor_fk,
                        OrderDate = poViewModel.DateCompleted,
                        ShipDate = poViewModel.ShipDate,
                        CancelDate = poViewModel.CancelDate,
                        CreateOrder = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        PoTotal = poViewModel.TotalPrice,
                        AsnTotal = poViewModel.TotalPrice,
                        GrnTotal = 0,
                        PoSubtotal = poViewModel.SubtotalPrice,
                        AsnSubtotal = poViewModel.SubtotalPrice,
                        GrnSubtotal = 0,
                        CreatedPO = done,
                        ItemsPoCount = poViewModel.ItemsCount,
                        ItemsAsnCount = poViewModel.ItemsCount,
                        ItemsGrnCount = 0,
                        FromWarehouse_fk = poViewModel.FromWarehouse_fk,
                        ToWarehouse_fk = poViewModel.ToWarehouse_fk,
                        ApproveAsnUser_fk = poViewModel.ApproveUser_fk,
                        Freight = 0,
                        DiscountPercent = 0,
                        Percent = "",
                        DiscountDollers = 0,
                        Insurance = 0,
                        CustomsDuty = 0,
                        Handling = 0,
                        Forwarding = 0,
                        LandTransport = 0,
                        Others = 0,
                        TotalCharges = 0,
                    };
                    if (done)
                        PO.PoNumber = _context.purchaseorders.Max(p => p.PoNumber) + 1;
                    _context.purchaseorders.Add(PO);
                }
                else
                {
                    PO = _context.purchaseorders.SingleOrDefault(p => p.Id == poViewModel.Id);
                    PO.Vendor_fk = poViewModel.Vendor_fk;
                    if (poViewModel.DateCompleted.Value.Date == DateTime.Now.Date)
                        PO.OrderDate = DateTime.Now;
                    else
                        PO.OrderDate = poViewModel.DateCompleted;
                    PO.ShipDate = poViewModel.ShipDate;
                    PO.CancelDate = poViewModel.CancelDate;
                    PO.LastEditDate = DateTime.Now;
                    PO.PoTotal = poViewModel.TotalPrice;
                    PO.AsnTotal = poViewModel.TotalPrice;
                    PO.PoSubtotal = poViewModel.SubtotalPrice;
                    PO.AsnSubtotal = poViewModel.SubtotalPrice;
                    PO.CreatedPO = done;
                    PO.ItemsPoCount = poViewModel.ItemsCount;
                    PO.ItemsAsnCount = poViewModel.ItemsCount;
                    PO.FromWarehouse_fk = poViewModel.FromWarehouse_fk;
                    PO.ToWarehouse_fk = poViewModel.ToWarehouse_fk;
                    PO.ApproveAsnUser_fk = poViewModel.ApproveUser_fk;

                    if (done)
                        PO.PoNumber = _context.purchaseorders.Max(p => p.PoNumber) + 1;

                }


                // Edit Code Later
                int IdOfItems = 0;
                if (_context.items.Any())
                    IdOfItems = _context.items.Max(p => p.Id);
                else
                    IdOfItems = 1;

                Item itemm = new Item();
                Item ItemDeleted;
                foreach (var item in items)
                {
                    if (item.Id == 0 && !item.IsDeleted)
                    {
                        IdOfItems++;
                        _context.items.Add(new Item()
                        {
                            Id = IdOfItems,
                            Po_fk = PO.Id,
                            ProductMaster_fk = item.ProductMaster_fk,
                            PoQuantity = item.Quantity,
                            AsnQuantity = item.Quantity,
                            GrnQuantity = 0,
                            PoPrice = item.Price,
                            AsnPrice = item.Price,
                            GrnPrice = item.Price,
                            Cost = item.Cost,
                            PoItemsPrice = item.TotalItemPrice,
                            AsnItemsPrice = item.TotalItemPrice,
                            Alert = false,
                            Note = item.Note,
                            Checked = false
                        });

                    }

                    else if (item.IsChanged && item.Id != 0)
                    {
                        itemm = _context.items.SingleOrDefault(p => p.Id == item.Id);
                        itemm.ProductMaster_fk = item.ProductMaster_fk;
                        itemm.PoQuantity = item.Quantity;
                        itemm.AsnQuantity = item.Quantity;
                        itemm.GrnQuantity = 0;
                        itemm.PoPrice = item.Price;
                        itemm.AsnPrice = item.Price;
                        itemm.GrnPrice = item.Price;
                        itemm.Cost = item.Cost;
                        itemm.PoItemsPrice = item.TotalItemPrice;
                        itemm.AsnItemsPrice = item.TotalItemPrice;
                        itemm.Note = item.Note;


                    }
                    else if (item.Id != 0 && item.IsDeleted)
                    {
                        ItemDeleted = _context.items.SingleOrDefault(p => p.Id == item.Id);
                        if (ItemDeleted != null)
                            _context.items.Remove(ItemDeleted);
                    }

                    else
                    {

                    }


                }

                _context.SaveChanges();


                return PO.PoNumber;
            }

        }

        public int AddOrUpdateAsnViewModel(AsnViewModel asnViewModel, IEnumerable<ItemsOfPurchaseOrderViewModel> items, bool done = false)
        {
            DetachedAllEntries();

            if (asnViewModel.CreatedInvoice)
            {
                return 0;
            }
            else
            {
                PurchaseOrder PO = new PurchaseOrder();
                if (asnViewModel.Id == 0)
                {
                    PO = new PurchaseOrder()
                    {
                        Id = _context.purchaseorders.Max(p => p.Id) + 1,
                        Vendor_fk = asnViewModel.Vendor_fk,
                        AsnDate = asnViewModel.DateCompleted,
                        ShipDate = asnViewModel.ShipDate,
                        CancelDate = asnViewModel.CancelDate,
                        CreateOrder = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        AsnTotal = asnViewModel.TotalPrice,
                        AsnSubtotal = asnViewModel.SubtotalPrice,
                        CreatedAsn = done,
                        CreatedPO = true,
                        PoNumber = -1,
                        ItemsAsnCount = asnViewModel.ItemsCount,
                        FromWarehouse_fk = asnViewModel.FromWarehouse_fk,
                        ToWarehouse_fk = asnViewModel.ToWarehouse_fk,
                        ApproveAsnUser_fk = asnViewModel.ApproveUser_fk,
                        Freight = asnViewModel.Freight,
                        DiscountPercent = asnViewModel.DiscountPercent,
                        Percent = asnViewModel.Percent,
                        DiscountDollers = asnViewModel.DiscountDollers,
                        Insurance = asnViewModel.Insurance,
                        CustomsDuty = asnViewModel.CustomsDuty,
                        Handling = asnViewModel.Handling,
                        Forwarding = asnViewModel.Forwarding,
                        LandTransport = asnViewModel.LandTransport,
                        Others = asnViewModel.Others,
                        TotalCharges = asnViewModel.TotalCharges,
                        GrnSubtotal = 0m,
                        GrnTotal = asnViewModel.TotalCharges,
                    };
                    if (done&&PO.Asnumber==0)
                        PO.Asnumber = _context.purchaseorders.Max(p => p.Asnumber) + 1;
                    _context.purchaseorders.Add(PO);
                }
                else
                {
                    PO = _context.purchaseorders.SingleOrDefault(p => p.Id == asnViewModel.Id);
                    PO.Vendor_fk = asnViewModel.Vendor_fk;
                    if (asnViewModel.DateCompleted.Value.Date == DateTime.Now.Date)
                        PO.AsnDate = DateTime.Now;
                    else
                        PO.AsnDate = asnViewModel.DateCompleted;
                    PO.ShipDate = asnViewModel.ShipDate;
                    PO.CancelDate = asnViewModel.CancelDate;
                    PO.LastEditDate = DateTime.Now;
                    PO.AsnTotal = asnViewModel.TotalPrice;
                    PO.AsnSubtotal = asnViewModel.SubtotalPrice;
                    PO.CreatedAsn = done;
                    PO.ItemsAsnCount = asnViewModel.ItemsCount;
                    PO.FromWarehouse_fk = asnViewModel.FromWarehouse_fk;
                    PO.ToWarehouse_fk = asnViewModel.ToWarehouse_fk;
                    PO.ApproveAsnUser_fk = asnViewModel.ApproveUser_fk;
                    PO.Freight = asnViewModel.Freight;
                    PO.DiscountPercent = asnViewModel.DiscountPercent;
                    PO.Percent = asnViewModel.Percent;
                    PO.DiscountDollers = asnViewModel.DiscountDollers;
                    PO.Insurance = asnViewModel.Insurance;
                    PO.CustomsDuty = asnViewModel.CustomsDuty;
                    PO.Handling = asnViewModel.Handling;
                    PO.Forwarding = asnViewModel.Forwarding;
                    PO.LandTransport = asnViewModel.LandTransport;
                    PO.Others = asnViewModel.Others;
                    PO.TotalCharges = asnViewModel.TotalCharges;
                    PO.GrnSubtotal = 0m;
                    PO.GrnTotal = asnViewModel.TotalCharges;

                    if (done && PO.Asnumber == 0)
                        PO.Asnumber = _context.purchaseorders.Max(p => p.Asnumber) + 1;

                }

                int IdOfItems = 0;
                if (_context.items.Any())
                    IdOfItems = _context.items.Max(p => p.Id);
                else
                    IdOfItems = 1;
                Item itemm = new Item();

                foreach (var item in items)
                {
                    IdOfItems++;
                    if (item.Id == 0 && !item.IsDeleted)
                    {
                        _context.items.Add(new Item()
                        {
                            Id = IdOfItems,
                            Po_fk = PO.Id,
                            ProductMaster_fk = item.ProductMaster_fk,
                            AsnQuantity = item.Quantity,
                            GrnQuantity = 0,
                            AsnPrice = item.Price,
                            GrnPrice = item.Price,
                            Cost = item.Cost,
                            AsnItemsPrice = item.TotalItemPrice,
                            Alert = false,
                            Note = item.Note,
                            Checked = false
                        });

                    }

                    else if (item.IsChanged && item.Id != 0)
                    {
                        itemm = _context.items.SingleOrDefault(p => p.Id == item.Id);
                        itemm.ProductMaster_fk = item.ProductMaster_fk;
                        itemm.AsnQuantity = item.Quantity;
                        itemm.AsnPrice = item.Price;
                        itemm.GrnQuantity = 0;
                        itemm.GrnPrice = item.Price;
                        itemm.Cost = item.Cost;
                        itemm.AsnItemsPrice = item.TotalItemPrice;
                        itemm.Note = item.Note;
                        itemm.IsDeleted = item.IsDeleted;
                        itemm.Alert = false;
                        itemm.Checked = false;

                    }
                    else if (item.Id != 0 && item.IsDeleted)
                    {
                        itemm = _context.items.SingleOrDefault(p => p.Id == item.Id);
                        itemm.AsnQuantity = 0;
                        itemm.GrnQuantity = 0;
                        item.Quantity = 0;
                        itemm.IsDeleted = true;
                    }
                    else
                    {

                    }


                }

                if (done)
                {
                    int ID = 0;
                    if (_context.productinventorywarehouses.ToList().Count != 0)
                        ID = _context.productinventorywarehouses.Max(p => p.Id);
                    else
                    {
                        ID = 1;
                    }
                    foreach (var VARIABLE in items)
                    {
                        if (!VARIABLE.IsDeleted)
                        {
                            ID++;
                            ProductInventoryWarehouse productInventoryWarehouse =
                                _context.productinventorywarehouses.Include(p => p.ProductMaster).SingleOrDefault(p =>
                                      p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                                      p.Warehouse_fk == asnViewModel.ToWarehouse_fk);
                            if (productInventoryWarehouse != null)
                            {
                                productInventoryWarehouse.OnTheWayInventory += VARIABLE.Quantity;
                            }
                            else
                            {
                                productInventoryWarehouse = new ProductInventoryWarehouse();
                                productInventoryWarehouse.Id = ID;
                                productInventoryWarehouse.ProductMaster_fk = VARIABLE.ProductMaster_fk;
                                productInventoryWarehouse.Warehouse_fk = asnViewModel.ToWarehouse_fk.Value;
                                productInventoryWarehouse.OnTheWayInventory = VARIABLE.Quantity;
                                productInventoryWarehouse.Income = 0;
                                productInventoryWarehouse.OutCome = 0;
                                _context.Add(productInventoryWarehouse);
                            }
                            if (asnViewModel.FromWarehouse_fk != 1)
                            {
                                ProductInventoryWarehouse productInventoryWarehouseFrom =
                                    _context.productinventorywarehouses.SingleOrDefault(p =>
                                        p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                                        p.Warehouse_fk == asnViewModel.FromWarehouse_fk);
                                if (productInventoryWarehouseFrom != null)
                                {
                                    productInventoryWarehouseFrom.Inventory -= VARIABLE.Quantity;
                                    productInventoryWarehouseFrom.OutCome += VARIABLE.Quantity;
                                }

                            }
                            else
                            {
                                //productInventoryWarehouse.ProductMaster.Transit += VARIABLE.Quantity;
                                var productmater = _context.productmasters.SingleOrDefault(p =>
                                    p.Id == productInventoryWarehouse.ProductMaster_fk);
                                productmater.Transit += VARIABLE.Quantity;
                            }
                        }

                    }
                }
                _context.SaveChanges();


                return PO.Asnumber;
            }

        }

        public int AddOrUpdateGrnViewModel(GrnViewModel grnViewModel, IEnumerable<ItemsOfPurchaseOrderViewModel> items, bool done = false)
        {
            DetachedAllEntries();
            if (grnViewModel.CreatedInvoice)
            {
                return 0;
            }
            else
            {
                PurchaseOrder PO = new PurchaseOrder();
                if (grnViewModel.Id == 0)
                {
                    PO = new PurchaseOrder()
                    {
                        Id = _context.purchaseorders.Max(p => p.Id) + 1,
                        Vendor_fk = grnViewModel.Vendor_fk,
                        GrnDate = grnViewModel.DateCompleted,
                        ShipDate = grnViewModel.ShipDate,
                        CancelDate = grnViewModel.CancelDate,
                        CreateOrder = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        CreatedGrn = done,
                        ItemsGrnCount = grnViewModel.ItemsCount,
                        FromWarehouse_fk = grnViewModel.FromWarehouse_fk,
                        ToWarehouse_fk = grnViewModel.ToWarehouse_fk,
                        ApproveAsnUser_fk = grnViewModel.ApproveUser_fk,
                        Freight = grnViewModel.Freight,
                        DiscountPercent = grnViewModel.DiscountPercent,
                        Percent = grnViewModel.Percent,
                        DiscountDollers = grnViewModel.DiscountDollers,
                        Insurance = grnViewModel.Insurance,
                        CustomsDuty = grnViewModel.CustomsDuty,
                        Handling = grnViewModel.Handling,
                        Forwarding = grnViewModel.Forwarding,
                        LandTransport = grnViewModel.LandTransport,
                        Others = grnViewModel.Others,
                        TotalCharges = grnViewModel.TotalCharges,
                        GrnSubtotal = grnViewModel.SubtotalPrice,
                        GrnTotal = grnViewModel.TotalPrice,

                    };
                    if (done&&PO.Grnumber==0)
                        PO.Grnumber = _context.purchaseorders.Max(p => p.Grnumber) + 1;
                    _context.purchaseorders.Add(PO);
                }
                else
                {
                    PO = _context.purchaseorders.SingleOrDefault(p => p.Id == grnViewModel.Id);
                    PO.Vendor_fk = grnViewModel.Vendor_fk;
                    PO.Vendor_fk = grnViewModel.Vendor_fk;
                    if (grnViewModel.DateCompleted.Value.Date == DateTime.Now.Date)
                        PO.GrnDate = DateTime.Now;
                    else
                        PO.GrnDate = grnViewModel.DateCompleted;

                    PO.ShipDate = grnViewModel.ShipDate;
                    PO.CancelDate = grnViewModel.CancelDate;
                    PO.LastEditDate = DateTime.Now;
                    PO.CreatedGrn = done;
                    PO.ItemsGrnCount = grnViewModel.ItemsCount;
                    PO.FromWarehouse_fk = grnViewModel.FromWarehouse_fk;
                    PO.ToWarehouse_fk = grnViewModel.ToWarehouse_fk;
                    PO.ApproveAsnUser_fk = grnViewModel.ApproveUser_fk;
                    PO.Freight = grnViewModel.Freight;
                    PO.DiscountPercent = grnViewModel.DiscountPercent;
                    PO.Percent = grnViewModel.Percent;
                    PO.DiscountDollers = grnViewModel.DiscountDollers;
                    PO.Insurance = grnViewModel.Insurance;
                    PO.CustomsDuty = grnViewModel.CustomsDuty;
                    PO.Handling = grnViewModel.Handling;
                    PO.Forwarding = grnViewModel.Forwarding;
                    PO.LandTransport = grnViewModel.LandTransport;
                    PO.Others = grnViewModel.Others;
                    PO.TotalCharges = grnViewModel.TotalCharges;
                    PO.GrnSubtotal = grnViewModel.SubtotalPrice;
                    PO.GrnTotal = grnViewModel.TotalPrice;
                    if (done&&PO.Grnumber==0)
                        PO.Grnumber = _context.purchaseorders.Max(p => p.Grnumber) + 1;

                }
                //_context.SaveChanges();

                Item itemm = new Item();
                int NewQuantityEnter = 0;
                int DiffrentQuntity = 0;
                foreach (var item in items)
                {
                    ProductInventoryWarehouse productInventory;
                    if (item.IsChanged && item.Id != 0)
                    {
                        itemm = _context.items.SingleOrDefault(p => p.Id == item.Id);
                        NewQuantityEnter = item.Quantity - itemm.GrnQuantity;
                        itemm.ProductMaster_fk = item.ProductMaster_fk;
                        itemm.GrnQuantity = item.Quantity;
                        itemm.GrnPrice = item.Price;
                        itemm.GrnItemsPrice = item.TotalItemPrice;
                        itemm.Diffrent = item.Diffrent;
                        itemm.Cost = item.Cost;
                        itemm.Note = item.Note;

                        if (itemm.Diffrent != 0)
                        {
                            itemm.Alert = true;
                            itemm.Checked = false;
                        }
                        else
                        {
                            itemm.Alert = false;
                            itemm.Checked = false;
                        }

                        DiffrentQuntity = itemm.AsnQuantity - itemm.GrnQuantity;
                        productInventory = _context.productinventorywarehouses.Include(p => p.ProductMaster).SingleOrDefault(p =>
                            p.ProductMaster_fk == item.ProductMaster_fk &&
                            p.Warehouse_fk == grnViewModel.ToWarehouse_fk);
                        if (productInventory != null)
                        {
                            productInventory.Income += NewQuantityEnter;
                            productInventory.Inventory += NewQuantityEnter;
                            productInventory.OnTheWayInventory -= NewQuantityEnter;
                            if (done)
                                productInventory.OnTheWayInventory -= DiffrentQuntity;
                            if (PO.FromWarehouse_fk == 1)
                            {
                                productInventory.ProductMaster.Income += NewQuantityEnter;
                                productInventory.ProductMaster.StockOnHand += NewQuantityEnter;
                                productInventory.ProductMaster.Transit -= NewQuantityEnter;
                                if (done)
                                    productInventory.ProductMaster.Transit -= DiffrentQuntity;
                            }

                        }

                    }


                    if (!item.IsDeleted)
                    {

                    }
                }

                //if (done)
                //{
                //    ProductInventoryWarehouse productInventory;
                //    foreach (var VARIABLE in items)
                //    {
                //        if (!VARIABLE.IsDeleted)
                //        {
                //            productInventory = _context.productinventorywarehouses.Include(p => p.ProductMaster).SingleOrDefault(p =>
                //                  p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                //                  p.Warehouse_fk == grnViewModel.ToWarehouse_fk);
                //            if (productInventory != null)
                //            {
                //                productInventory.Income += VARIABLE.Quantity;
                //                productInventory.Inventory += VARIABLE.Quantity;
                //                productInventory.OnTheWayInventory -= VARIABLE.PreviousQuantity;
                //                if (PO.FromWarehouse_fk == 1)
                //                {
                //                    productInventory.ProductMaster.Income += VARIABLE.Quantity;
                //                    productInventory.ProductMaster.StockOnHand += VARIABLE.Quantity;
                //                    productInventory.ProductMaster.Transit -= VARIABLE.Quantity;
                //                }
                //                else
                //                {
                //                    productInventory.ProductMaster.StockOnHand += VARIABLE.Diffrent;
                //                }

                //            }
                //        }

                //    }
                //}

                _context.SaveChanges();


                return PO.Grnumber;
            }

        }

        public int AddPurchaseOrder(PurchaseOrder purchaseOrder, bool isSave = true)
        {
            if (isSave)
                DetachedAllEntries();
            if (purchaseOrder.Id == 0)
            {
                //product.Id = _context.customers.Max(p => p.Id) + 1;
                //_context.productmasters.Add(product);
            }
            else if (!isSave)
            {
                
                int itemId = _context.items.Max(p => p.Id);
                foreach (var VARIABLE in purchaseOrder.Items)
                {
                    itemId++;
                    VARIABLE.Id = itemId;
                }
                _context.purchaseorders.Add(purchaseOrder);
            }

            else
            {

                int itemId = 0;
                if(_context.items.Count()!=0)
                   itemId= _context.items.Max(p => p.Id);
                var deletitem = new List<Item>();
                var duplicate = new List<int>();
                deletitem.Clear();
                //Check Later 
                foreach (var VARIABLE in purchaseOrder.Items)
                {

                    if (_context.productmasters.FirstOrDefault(p => p.Id == VARIABLE.ProductMaster_fk) == null)
                        return -11;
                    if(purchaseOrder.Items.Where(p=>p.ProductMaster_fk==VARIABLE.ProductMaster_fk).ToList().Count>1)
                        duplicate.Add(VARIABLE.ProductMaster_fk);
                    else
                    {
                        itemId++;
                        VARIABLE.Id = itemId;
                    }
                }

                if (duplicate.Count > 0)
                {
                    var duplicateId = duplicate.Distinct().ToList();

                    foreach (var VARIABLE in duplicateId)
                    {
                        var ttr = purchaseOrder.Items.Where(p => p.ProductMaster_fk == VARIABLE).ToList();
                        foreach (var dupi in ttr.Skip(1).ToList())
                        {
                            itemId++;
                            ttr.ElementAt(0).Id += itemId;
                            ttr.ElementAt(0).AsnQuantity += dupi.AsnQuantity;
                            ttr.ElementAt(0).GrnQuantity += dupi.GrnQuantity;
                            ttr.ElementAt(0).AsnItemsPrice += dupi.AsnItemsPrice;
                            ttr.ElementAt(0).GrnItemsPrice += dupi.GrnItemsPrice;
                            purchaseOrder.Items.Remove(dupi);
                        }

                    }
                }

                _context.purchaseorders.Add(purchaseOrder);
            }
            if (isSave)
                _context.SaveChanges();
            return 1;
        }

        public int UpdatePurchaseOrder(PurchaseOrder purchaseOrder, bool isSave = true)
        {

            if (isSave)
                DetachedAllEntries();
            if (purchaseOrder.Id == 0)
            {
                //product.Id = _context.customers.Max(p => p.Id) + 1;
                //_context.productmasters.Add(product);
            }
            else if (!isSave)
            {
                PurchaseOrder ww;
                int resualt = 0;

                ww = _context.purchaseorders.SingleOrDefault(p => p.Id ==purchaseOrder.Id);

                ww.DocNumber = purchaseOrder.DocNumber;
                ww.FromWarehouse_fk = purchaseOrder.FromWarehouse_fk;
                ww.ToWarehouse_fk = purchaseOrder.ToWarehouse_fk;
                ww.PoNumber = purchaseOrder.PoNumber;
                ww.Asnumber = purchaseOrder.Asnumber;
                ww.Grnumber = purchaseOrder.Grnumber;
                ww.AsnTotal = purchaseOrder.AsnTotal;
                ww.PrivateNote = purchaseOrder.PrivateNote;
                ww.Note = purchaseOrder.Note;
                ww.LastUpdateTime = purchaseOrder.LastUpdateTime;
                ww.PoStatus = purchaseOrder.PoStatus;
                ww.Vendor_fk = purchaseOrder.Vendor_fk;
                ww.LastUpdateTime = purchaseOrder.LastUpdateTime;
                ww.ShipAddress = purchaseOrder.ShipAddress;

                _context.purchaseorders.Update(ww);
            }

            else
            {

            }
            if (isSave)
                _context.SaveChanges();
            return 1;
        }


        public bool SaveDatabase()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public IQueryable<AsnViewModel> AsnList()
        {
            var AsnViewModels = _context.purchaseorders.Select(p => new AsnViewModel()
            {
                Id = p.Id,
                Number = p.Asnumber.ShowAsnNumber(),
                Vendor_fk = p.Vendor_fk,
                Associate = p.Associate,
                DateCompleted = p.AsnDate,
                ShipDate = p.ShipDate,
                CancelDate = p.CancelDate,
                TotalPrice = p.AsnTotal,
                ItemsCount = p.ItemsAsnCount,
                ToWarehouse_fk = p.ToWarehouse_fk,
                FromWarehouse_fk = p.FromWarehouse_fk,
                ApproveUser_fk = p.ApproveAsnUser_fk,
                Items = p.Items,
                Vendor = p.Vendor,
                UserCreate = p.UserCreateAsn,
                ToWarehouse = p.ToWarehouse,
                FromWarehouse = p.FromWarehouse,
            });

            return AsnViewModels;
        }

        public PurchaseOrder GivePurchaseOrderById(int id)
        {
            return _context.purchaseorders.Include(p => p.Items).ThenInclude(p => p.ProductMaster)
                .Include(p => p.FromWarehouse).Include(p => p.ToWarehouse)
                .Include(p => p.UserCreatePo).Include(p => p.UserCreateAsn)
                .Include(p => p.UserCreateGrn)
                .SingleOrDefault(p => p.Id == id);
        }



        public IQueryable<GrnViewModel> GrnList()
        {
            var GrnViewModels = _context.purchaseorders.Select(p => new GrnViewModel()
            {
                Id = p.Id,
                Number = p.Grnumber.ShowGrnNumber(),
                Vendor_fk = p.Vendor_fk,
                Associate = p.Associate,
                DateCompleted = p.GrnDate,
                ShipDate = p.ShipDate,
                CancelDate = p.CancelDate,
                TotalPrice = p.GrnTotal,
                ItemsCount = p.ItemsGrnCount,
                ToWarehouse_fk = p.ToWarehouse_fk,
                FromWarehouse_fk = p.FromWarehouse_fk,
                ApproveUser_fk = p.ApproveGrnUser_fk,
                Items = p.Items,
                Vendor = p.Vendor,
                UserCreate = p.UserCreateGrn,
                ToWarehouse = p.ToWarehouse,
                FromWarehouse = p.FromWarehouse,
            });

            return GrnViewModels;
        }

        public IQueryable<PoViewModel> PoList()
        {
            var poViewModels = _context.purchaseorders.Select(p => new PoViewModel()
            {
                Id = p.Id,
                Number = p.PoNumber.ShowPoNumber(),
                Vendor_fk = p.Vendor_fk,
                Associate = p.Associate,
                DateCompleted = p.OrderDate,
                ShipDate = p.ShipDate,
                CancelDate = p.CancelDate,
                TotalPrice = p.PoTotal,
                ItemsCount = p.ItemsPoCount,
                ToWarehouse_fk = p.ToWarehouse_fk,
                FromWarehouse_fk = p.FromWarehouse_fk,
                ApproveUser_fk = p.ApprovePoUser_fk,
                Items = p.Items,
                Vendor = p.Vendor,
                UserCreate = p.UserCreatePo,
                ToWarehouse = p.ToWarehouse,
                FromWarehouse = p.FromWarehouse,
            });

            return poViewModels;
        }



    }
}
