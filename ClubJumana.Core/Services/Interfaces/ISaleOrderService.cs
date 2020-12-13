using System;
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
       public List<SalesOrderListview> SalesOrdersListView();
       public List<SalesOrderListview> SalesInvoceListView();
        public bool SendEmailOrPrint(SaleOrderViewModel saleOrder,bool IsPrint);
       public int CreateInvoice(int Id);

       public bool AddRefund(Refund refund);
       public List<RefundItem> CovertToRefundItem(ObservableCollection<RefundItemsViewModel> refundItemsViewModel);
    }
}
