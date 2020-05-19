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
        private JummanaContext _context;

        public PerchaseOrderService()
        {
            //var optionsBuilder = new DbContextOptionsBuilder<JummanaContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCore-smm28;Trusted_Connection=True");
            _context = new JummanaContext();
        }


        public bool AddOrUpdatePoViewModel(PoViewModel poViewModel, IEnumerable<ItemsOfPurchaseOrderViewModel> items, bool done = false)
        {
            if (poViewModel.CreatedInvoice)
            {
                return false;
            }
            else
            {
                PurchaseOrder PO = new PurchaseOrder();
                if (poViewModel.Id == 0)
                {
                    PO = new PurchaseOrder()
                    {
                        Id = _context.PurchaseOrders.Max(p => p.Id) + 1,
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
                        TotalCharges = 0
                    };
                    if (done)
                        PO.PoNumber = _context.PurchaseOrders.Max(p => p.PoNumber) + 1;
                    _context.PurchaseOrders.Add(PO);
                }
                else
                {
                    PO = _context.PurchaseOrders.SingleOrDefault(p => p.Id == poViewModel.Id);
                    PO.Vendor_fk = poViewModel.Vendor_fk;
                    PO.Vendor_fk = poViewModel.Vendor_fk;
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
                        PO.PoNumber = _context.PurchaseOrders.Max(p => p.PoNumber) + 1;

                }
                _context.SaveChanges();

                int IdOfItems = _context.Items.Max(p => p.Id) + 1;
                Item itemm = new Item();
                foreach (var item in items)
                {
                    if (item.Id == 0)
                    {
                        _context.Items.Add(new Item()
                        {
                            Id = IdOfItems,
                            Po_fk = PO.Id,
                            ProductMaster_fk = item.ProductMaster_fk,
                            PoQuantity = item.Quantity,
                            AsnQuantity = item.Quantity,
                            PoPrice = item.Price,
                            AsnPrice = item.Price,
                            Cost = item.Cost,
                            PoItemsPrice = item.TotalItemPrice,
                            AsnItemsPrice = item.TotalItemPrice,
                            Alert = item.Alert,
                            Note = item.Note,
                            Checked = item.Checked
                        });
                        IdOfItems++;
                    }

                    if (item.IsChanged && item.Id != 0)
                    {
                        itemm = _context.Items.SingleOrDefault(p => p.Id == item.Id);
                        itemm.ProductMaster_fk = item.ProductMaster_fk;
                        itemm.PoQuantity = item.Quantity;
                        itemm.AsnQuantity = item.Quantity;
                        itemm.PoPrice = item.Price;
                        itemm.AsnPrice = item.Price;
                        itemm.Cost = item.Cost;
                        itemm.PoItemsPrice = item.TotalItemPrice;
                        itemm.AsnItemsPrice = item.TotalItemPrice;
                        itemm.Alert = item.Alert;
                        itemm.Note = item.Note;
                        itemm.Checked = item.Checked;


                    }


                }

                _context.SaveChanges();


                return true;
            }

        }

        public bool AddOrUpdateAsnViewModel(AsnViewModel asnViewModel, IEnumerable<ItemsOfPurchaseOrderViewModel> items, bool done = false)
        {
            if (asnViewModel.CreatedInvoice)
            {
                return false;
            }
            else
            {
                PurchaseOrder PO = new PurchaseOrder();
                if (asnViewModel.Id == 0)
                {
                    PO = new PurchaseOrder()
                    {
                        Id = _context.PurchaseOrders.Max(p => p.Id) + 1,
                        Vendor_fk = asnViewModel.Vendor_fk,
                        AsnDate = asnViewModel.DateCompleted,
                        ShipDate = asnViewModel.ShipDate,
                        CancelDate = asnViewModel.CancelDate,
                        CreateOrder = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        AsnTotal = asnViewModel.TotalPrice,
                        AsnSubtotal = asnViewModel.SubtotalPrice,
                        GrnTotal = asnViewModel.TotalPrice,
                        GrnSubtotal = asnViewModel.SubtotalPrice,
                        CreatedAsn = done,
                        CreatedPO = true,
                        PoNumber = -1,
                        ItemsPoCount = asnViewModel.ItemsCount,
                        FromWarehouse_fk = asnViewModel.FromWarehouse_fk,
                        ToWarehouse_fk = asnViewModel.ToWarehouse_fk,
                        ApproveAsnUser_fk = asnViewModel.ApproveUser_fk,
                        Freight = 0,
                        DiscountPercent = 0,
                        Percent ="",
                        DiscountDollers =0,
                        Insurance = 0,
                        CustomsDuty = 0,
                        Handling = 0,
                        Forwarding = 0,
                        LandTransport =0,
                        Others =0,
                        TotalCharges = 0

                    };
                    if (done)
                        PO.Asnumber = _context.PurchaseOrders.Max(p => p.Asnumber) + 1;
                    _context.PurchaseOrders.Add(PO);
                }
                else
                {
                    PO = _context.PurchaseOrders.SingleOrDefault(p => p.Id == asnViewModel.Id);
                    PO.Vendor_fk = asnViewModel.Vendor_fk;
                    PO.Vendor_fk = asnViewModel.Vendor_fk;
                    PO.AsnDate = asnViewModel.DateCompleted;
                    PO.ShipDate = asnViewModel.ShipDate;
                    PO.CancelDate = asnViewModel.CancelDate;
                    PO.LastEditDate = DateTime.Now;
                    PO.AsnTotal = asnViewModel.TotalPrice;
                    PO.AsnSubtotal = asnViewModel.SubtotalPrice;
                    PO.GrnTotal = asnViewModel.TotalPrice;
                    PO.GrnSubtotal= asnViewModel.SubtotalPrice;
                    PO.CreatedAsn = done;
                    PO.ItemsPoCount = asnViewModel.ItemsCount;
                    PO.FromWarehouse_fk = asnViewModel.FromWarehouse_fk;
                    PO.ToWarehouse_fk = asnViewModel.ToWarehouse_fk;
                    PO.ApproveAsnUser_fk = asnViewModel.ApproveUser_fk;

                    if (done)
                        PO.Asnumber = _context.PurchaseOrders.Max(p => p.Asnumber) + 1;

                }
                _context.SaveChanges();

                int IdOfItems = _context.Items.Max(p => p.Id) + 1;
                Item itemm = new Item();

                foreach (var item in items)
                {
                    if (item.Id == 0)
                    {
                        _context.Items.Add(new Item()
                        {
                            Id = IdOfItems,
                            Po_fk = PO.Id,
                            ProductMaster_fk = item.ProductMaster_fk,
                            AsnQuantity = item.Quantity,
                            AsnPrice = item.Price,
                            Cost = item.Cost,
                            PoItemsPrice = item.TotalItemPrice,
                            Alert = item.Alert,
                            Note = item.Note,
                            Checked = item.Checked
                        });
                        IdOfItems++;
                    }

                    else if (item.IsChanged && item.Id != 0)
                    {
                        itemm = _context.Items.SingleOrDefault(p => p.Id == item.Id);
                        itemm.ProductMaster_fk = item.ProductMaster_fk;
                        itemm.AsnQuantity = item.Quantity;
                        itemm.AsnPrice = item.Price;
                        itemm.Cost = item.Cost;
                        itemm.AsnItemsPrice = item.TotalItemPrice;
                        itemm.Alert = item.Alert;
                        itemm.Note = item.Note;
                        itemm.Checked = item.Checked;


                    }
                    else
                    {

                    }


                }

                if (done)
                {
                    int ID = 0;
                    if (_context.ProductInventoryWarehouses.ToList().Count != 0)
                        ID = _context.ProductInventoryWarehouses.Max(p => p.Id);
                    else
                    {
                        ID = 1;
                    }
                    foreach (var VARIABLE in items)
                    {
                        ID++;
                        ProductInventoryWarehouse productInventoryWarehouse =
                            _context.ProductInventoryWarehouses.SingleOrDefault(p =>
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
                            productInventoryWarehouse.Warehouse_fk = asnViewModel.ToWarehouse_fk;
                            productInventoryWarehouse.OnTheWayInventory = VARIABLE.Quantity;
                            productInventoryWarehouse.Income = 0;
                            productInventoryWarehouse.OutCome = 0;
                            _context.Add(productInventoryWarehouse);
                        }
                        if (asnViewModel.FromWarehouse_fk != 2)
                        {
                            ProductInventoryWarehouse productInventoryWarehouseFrom =
                                _context.ProductInventoryWarehouses.SingleOrDefault(p =>
                                    p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                                    p.Warehouse_fk == asnViewModel.FromWarehouse_fk);
                            if (productInventoryWarehouseFrom != null)
                                productInventoryWarehouseFrom.Inventory = VARIABLE.Quantity;
                        }
                    }
                }
                _context.SaveChanges();


                return true;
            }
          
        }

        public bool AddOrUpdateGrnViewModel(GrnViewModel grnViewModel, IEnumerable<ItemsOfPurchaseOrderViewModel> items, bool done = false)
        {
            if (grnViewModel.CreatedInvoice)
            {
                return false;
            }
            else
            {
                PurchaseOrder PO = new PurchaseOrder();
                if (grnViewModel.Id == 0)
                {
                    PO = new PurchaseOrder()
                    {
                        Id = _context.PurchaseOrders.Max(p => p.Id) + 1,
                        Vendor_fk = grnViewModel.Vendor_fk,
                        GrnDate = grnViewModel.DateCompleted,
                        ShipDate = grnViewModel.ShipDate,
                        CancelDate = grnViewModel.CancelDate,
                        CreateOrder = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        CreatedGrn = done,
                        ItemsPoCount = grnViewModel.ItemsCount,
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
                        GrnTotal = grnViewModel.TotalPrice,
                        TotalCharges = grnViewModel.TotalCharges
                    };
                    if (done)
                        PO.Grnumber = _context.PurchaseOrders.Max(p => p.Grnumber) + 1;
                    _context.PurchaseOrders.Add(PO);
                }
                else
                {
                    PO = _context.PurchaseOrders.SingleOrDefault(p => p.Id == grnViewModel.Id);
                    PO.Vendor_fk = grnViewModel.Vendor_fk;
                    PO.Vendor_fk = grnViewModel.Vendor_fk;
                    PO.GrnDate = grnViewModel.DateCompleted;
                    PO.ShipDate = grnViewModel.ShipDate;
                    PO.CancelDate = grnViewModel.CancelDate;
                    PO.LastEditDate = DateTime.Now;
                    PO.CreatedGrn = done;
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
                    PO.GrnTotal = grnViewModel.TotalPrice;
                    PO.TotalCharges = grnViewModel.TotalCharges;
                    if (done)
                        PO.Grnumber = _context.PurchaseOrders.Max(p => p.Grnumber) + 1;

                }
                _context.SaveChanges();

                int IdOfItems = _context.Items.Max(p => p.Id) + 1;
                Item itemm = new Item();
                foreach (var item in items)
                {
                    if (item.Id == 0)
                    {
                        _context.Items.Add(new Item()
                        {
                            Id = IdOfItems,
                            Po_fk = PO.Id,
                            ProductMaster_fk = item.ProductMaster_fk,
                            GrnQuantity = item.Quantity,
                            Diffrent = item.Diffrent,
                            Cost = item.Cost,
                            Alert = item.Alert,
                            Note = item.Note,
                            Checked = item.Checked
                        });
                        IdOfItems++;
                    }

                    if (item.IsChanged && item.Id != 0)
                    {
                        itemm = _context.Items.SingleOrDefault(p => p.Id == item.Id);
                        itemm.ProductMaster_fk = item.ProductMaster_fk;
                        itemm.GrnQuantity = item.Quantity;
                        itemm.Diffrent = item.Diffrent;
                        itemm.Cost = item.Cost;
                        itemm.Alert = item.Alert;
                        itemm.Note = item.Note;
                        itemm.Checked = item.Checked;


                    }


                }

                if (done)
                {
                    ProductInventoryWarehouse productInventory;
                    foreach (var VARIABLE in items)
                    {
                        productInventory = _context.ProductInventoryWarehouses.SingleOrDefault(p =>
                            p.ProductMaster_fk == VARIABLE.ProductMaster_fk &&
                            p.Warehouse_fk == grnViewModel.ToWarehouse_fk);
                        if (productInventory != null)
                        {
                            productInventory.Income += VARIABLE.Quantity;
                            productInventory.Inventory += VARIABLE.Quantity;
                            productInventory.OnTheWayInventory -= VARIABLE.PreviousQuantity;
                            VARIABLE.ProductMaster.Income += VARIABLE.Quantity;
                            VARIABLE.ProductMaster.StockOnHand += VARIABLE.Quantity;
                        }
                    }
                }

                _context.SaveChanges();


                return true;
            }

        }

        public IQueryable<AsnViewModel> AsnList()
        {
            var AsnViewModels = _context.PurchaseOrders.Select(p => new AsnViewModel()
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
            return _context.PurchaseOrders.Include(p => p.Items).ThenInclude(p => p.ProductMaster)
                .Include(p => p.FromWarehouse).Include(p => p.ToWarehouse)
                .Include(p => p.UserCreatePo).Include(p => p.UserCreateAsn)
                .Include(p => p.UserCreateGrn)
                .SingleOrDefault(p => p.Id == id);
        }



        public IQueryable<GrnViewModel> GrnList()
        {
            var GrnViewModels = _context.PurchaseOrders.Select(p => new GrnViewModel()
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
            var poViewModels = _context.PurchaseOrders.Select(p => new PoViewModel()
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
