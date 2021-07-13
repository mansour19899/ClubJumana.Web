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
        public int AddSalesOrder(SaleOrder saleOrder, bool isSave = true);
        public int UpdateSalesOrder(SaleOrder saleOrder, bool isSave = true);
        public SaleOrderViewModel GiveSaleOrderById(int id);
       public List<SalesOrderListview> SalesOrdersListView();
       public List<SalesOrderListview> SalesInvoceListView();
        public bool SendEmailOrPrint(SaleOrderViewModel saleOrder,bool IsPrint);
       public int CreateInvoice(int Id);

       public int AddRefund(Refund refund,List<RefundItemsViewModel> refundItemsViewModel);

       public List<RefundItem> CovertToRefundItem(ObservableCollection<RefundItemsViewModel> refundItemsViewModel);
       public List<CustomersInvoiceViewModel> GiveMeAllOpenInvoiceForCustomer(int CustomrId);
    }
}
