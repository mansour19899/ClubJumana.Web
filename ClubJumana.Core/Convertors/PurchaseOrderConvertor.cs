using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.Core.Generator;
using ClubJumana.Core.Services;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Convertors
{
   public class PurchaseOrderConvertor
   {

        public PurchaseOrderConvertor()
        {
        }

        public PoViewModel ConvertToPO(PurchaseOrder purchaseOrder,List<Vendor> vendors,List<Warehouse> warehouses)
        {
            return new PoViewModel()
            {
                Id = purchaseOrder.Id,
                Number = purchaseOrder.PoNumber.ShowPoNumber(),
                Vendor_fk = purchaseOrder.Vendor_fk,
                Associate = purchaseOrder.Associate,
                DateCompleted = DateTime.Today,
                ShipDate = purchaseOrder.ShipDate,
                CancelDate = purchaseOrder.CancelDate,
                ItemsCount = purchaseOrder.ItemsPoCount,
                ToWarehouse_fk = purchaseOrder.ToWarehouse_fk,
                FromWarehouse_fk = purchaseOrder.FromWarehouse_fk,
                ApproveUser_fk = purchaseOrder.ApprovePoUser_fk,
                Items = purchaseOrder.Items,
                Vendor = purchaseOrder.Vendor,
                UserCreate = purchaseOrder.UserCreatePo,
                ToWarehouse = purchaseOrder.ToWarehouse,
                FromWarehouse = purchaseOrder.FromWarehouse,
                CreatedInvoice = purchaseOrder.CreatedPO,
                WarehousesList = warehouses,
                VendorsList = vendors,
                ModeName = "PO ",
                PurchasingName = "PURCHASE ORDER",
                LastEdit = purchaseOrder.LastEditDate,
                ItemsOfPurchaseOrderViewModels= CovertItemsOfPurchaseOrderViewModels(purchaseOrder, Mode.PO),
                TotalPrice = purchaseOrder.PoTotal,
                SubtotalPrice = purchaseOrder.PoSubtotal,

            };
        }
        public AsnViewModel ConvertToAsn(PurchaseOrder purchaseOrder, List<Vendor> vendors, List<Warehouse> warehouses)
        {
            return new AsnViewModel()
            {
                Id = purchaseOrder.Id,
                Number = purchaseOrder.Asnumber.ShowAsnNumber(),
                Vendor_fk = purchaseOrder.Vendor_fk,
                Associate = purchaseOrder.Associate,
                DateCompleted = DateTime.Today,
                ShipDate = purchaseOrder.ShipDate,
                CancelDate = purchaseOrder.CancelDate,
                ItemsCount = purchaseOrder.ItemsAsnCount,
                ToWarehouse_fk = purchaseOrder.ToWarehouse_fk,
                FromWarehouse_fk = purchaseOrder.FromWarehouse_fk,
                ApproveUser_fk = purchaseOrder.ApproveAsnUser_fk,
                Items = purchaseOrder.Items,
                Vendor = purchaseOrder.Vendor,
                UserCreate = purchaseOrder.UserCreateAsn,
                ToWarehouse = purchaseOrder.ToWarehouse,
                FromWarehouse = purchaseOrder.FromWarehouse,
                CreatedInvoice = purchaseOrder.CreatedAsn,
                WarehousesList = warehouses,
                VendorsList = vendors,
                Freight = purchaseOrder.Freight,
                DiscountPercent = purchaseOrder.DiscountPercent,
                Percent = purchaseOrder.Percent,
                DiscountDollers = purchaseOrder.DiscountDollers,
                Insurance = purchaseOrder.Insurance,
                CustomsDuty = purchaseOrder.CustomsDuty,
                Handling = purchaseOrder.Handling,
                Forwarding = purchaseOrder.Forwarding,
                LandTransport = purchaseOrder.LandTransport,
                Others = purchaseOrder.Others,
                TotalCharges = purchaseOrder.TotalCharges,
                ModeName = "GIT ",
                PurchasingName = "GOODS IN TRANSIT",
                LastEdit = purchaseOrder.LastEditDate,
                ItemsOfPurchaseOrderViewModels = CovertItemsOfPurchaseOrderViewModels(purchaseOrder, Mode.Asn),
                TotalPrice = purchaseOrder.AsnTotal,
                SubtotalPrice = purchaseOrder.AsnSubtotal,
            };
        }
        public GrnViewModel ConvertToGrn(PurchaseOrder purchaseOrder, List<Vendor> vendors, List<Warehouse> warehouses)
        {
            return new GrnViewModel()
            {
                Id = purchaseOrder.Id,
                Number = purchaseOrder.Grnumber.ShowGrnNumber(),
                Vendor_fk = purchaseOrder.Vendor_fk,
                Associate = purchaseOrder.Associate,
                DateCompleted = DateTime.Today,
                ShipDate = purchaseOrder.ShipDate,
                CancelDate = purchaseOrder.CancelDate,
                ItemsCount = purchaseOrder.ItemsGrnCount,
                ToWarehouse_fk = purchaseOrder.ToWarehouse_fk,
                FromWarehouse_fk = purchaseOrder.FromWarehouse_fk,
                ApproveUser_fk = purchaseOrder.ApproveGrnUser_fk,
                Items = purchaseOrder.Items,
                Vendor = purchaseOrder.Vendor,
                UserCreate = purchaseOrder.UserCreateGrn,
                ToWarehouse = purchaseOrder.ToWarehouse,
                FromWarehouse = purchaseOrder.FromWarehouse,
                CreatedInvoice = purchaseOrder.CreatedGrn,
                WarehousesList = warehouses,
                VendorsList = vendors,
                Freight =purchaseOrder.Freight,
                DiscountPercent = purchaseOrder.DiscountPercent,
                Percent = purchaseOrder.Percent,
                DiscountDollers = purchaseOrder.DiscountDollers,
                Insurance = purchaseOrder.Insurance,
                CustomsDuty = purchaseOrder.CustomsDuty,
                Handling = purchaseOrder.Handling,
                Forwarding = purchaseOrder.Forwarding,
                LandTransport = purchaseOrder.LandTransport,
                Others = purchaseOrder.Others,
                TotalCharges = purchaseOrder.TotalCharges,
                ModeName = "GRN ",
                PurchasingName = "GOODS RECEIVE NOTE",
                LastEdit = purchaseOrder.LastEditDate,
                ItemsOfPurchaseOrderViewModels = CovertItemsOfPurchaseOrderViewModels(purchaseOrder, Mode.Grn),
                TotalPrice = purchaseOrder.GrnTotal,
                SubtotalPrice = purchaseOrder.GrnSubtotal
            };
        }

        public ObservableCollection<ItemsOfPurchaseOrderViewModel> CovertItemsOfPurchaseOrderViewModels(PurchaseOrder purchaseOrder,Mode mode)
        {
            ObservableCollection<ItemsOfPurchaseOrderViewModel> itemsOfPurchaseOrderViewModel=new ObservableCollection<ItemsOfPurchaseOrderViewModel>();
            switch (mode)
            {
                case Mode.PO:
                    foreach (var item in purchaseOrder.Items)
                    {
                        if (item.PoQuantity == 0 && purchaseOrder.CreatedPO)
                        {

                        }
                        else
                        {
                            itemsOfPurchaseOrderViewModel.Add(new ItemsOfPurchaseOrderViewModel()
                            {
                                Id = item.Id,
                                ProductMaster_fk = item.ProductMaster_fk,
                                ProductMaster = item.ProductMaster,
                                Price = item.PoPrice,
                                Quantity = item.PoQuantity,
                                Cost = item.Cost,
                                TotalItemPrice = item.PoItemsPrice,
                                IsChanged = false,
                                IsDeleted = item.IsDeleted,
                                 Checked= item.Checked
                            });
                        }

                    }
                    break;
                case Mode.Asn:
                    foreach (var item in purchaseOrder.Items)
                    {
                        if (item.IsDeleted == false)
                        {
                            itemsOfPurchaseOrderViewModel.Add(new ItemsOfPurchaseOrderViewModel()
                            {
                                Id = item.Id,
                                ProductMaster_fk = item.ProductMaster_fk,
                                ProductMaster = item.ProductMaster,
                                PreviousQuantity = item.PoQuantity,
                                Price = item.AsnPrice,
                                Quantity = item.AsnQuantity,
                                Cost = item.Cost,
                                TotalItemPrice = item.AsnItemsPrice,
                                IsChanged = false,
                                IsDeleted = item.IsDeleted,
                                Checked = item.Checked
                            });
                        }
                       
                    }
                    break;
                case Mode.Grn:
                    foreach (var item in purchaseOrder.Items)
                    {
                        if (item.IsDeleted == false)
                        {
                            itemsOfPurchaseOrderViewModel.Add(new ItemsOfPurchaseOrderViewModel()
                            {
                                Id = item.Id,
                                ProductMaster_fk = item.ProductMaster_fk,
                                ProductMaster = item.ProductMaster,
                                PreviousQuantity = item.AsnQuantity,
                                Price = item.GrnPrice,
                                Quantity = item.GrnQuantity,
                                Cost = item.Cost,
                                TotalItemPrice = item.GrnItemsPrice,
                                IsChanged = false,
                                IsDeleted = item.IsDeleted,
                                Checked = item.Checked
                            });
                        }
                       
                    }
                    break;
            }
            return itemsOfPurchaseOrderViewModel;
        }
         
   }
}
