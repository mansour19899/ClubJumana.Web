using System;
using System.Collections.Generic;
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
    }
}
