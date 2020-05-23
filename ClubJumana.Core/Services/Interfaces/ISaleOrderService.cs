﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
    interface ISaleOrderService
    {
        public int SaveAndUpdateSaleOrder(SaleOrderViewModel saleOrder);
       public SaleOrderViewModel GiveSaleOrderById(int id);

       public bool SendEmailOrPrint(SaleOrderViewModel saleOrder,bool IsPrint);
       public bool CreateInvoice(SaleOrderViewModel saleOrder);

       public bool AddRefund(Refund refund);
       public List<RefundItem> CovertToRefundItem(ObservableCollection<RefundItemsViewModel> refundItemsViewModel);
    }
}
