using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
   public interface IPerchaseOrderService
   {

       IQueryable<PoViewModel> PoList();
       IQueryable<AsnViewModel> AsnList();
       IQueryable<GrnViewModel> GrnList();

       PurchaseOrder GivePurchaseOrderById(int id);

       int AddOrUpdatePoViewModel(PoViewModel poViewModel,IEnumerable<ItemsOfPurchaseOrderViewModel> items,bool done);
       int AddOrUpdateAsnViewModel(AsnViewModel asnViewModel,IEnumerable<ItemsOfPurchaseOrderViewModel> items,bool done);
       int AddOrUpdateGrnViewModel(GrnViewModel grnViewModel,IEnumerable<ItemsOfPurchaseOrderViewModel> items,bool done);

   }
}
