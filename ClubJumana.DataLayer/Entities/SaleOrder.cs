﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClubJumana.DataLayer.Entities.Users;
using JetBrains.Annotations;

namespace ClubJumana.DataLayer.Entities
{
    public class SaleOrder {
        public int Id { get; set; }
        //WholeSale Is True and Retail is False
        public bool Type { get; set; }
        public bool HaveDeposit { get; set; } = false;
        public DateTime? SoDate { get; set; }
        public DateTime? ExpriationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? InvoiceNumber { get; set; }
        public string DocNumber { get; set; }
        public int? Cashier_fk { get; set; }
        public int? Customer_fk { get; set; }
        public int? Warehouse_fk { get; set; }
        public int? term_fk { get; set; }
        public int? ShipMethod_fk { get; set; }
        [Column(TypeName = "decimal(12, 6)")]
        public decimal ExchangeRate { get; set; } = 0;
        public decimal TermPercent { get; set; } = 0;
        public decimal Subtotal { get; set; }

        public decimal SoTotalPrice { get; set; }
        public decimal OpenBalance { get; set; } = -1;
        public decimal HomeBalance { get; set; } = -1;
        public decimal HomeTotalAmt { get; set; } = -1;


        public decimal Handling { get; set; }
        public byte HandlingTaxCode { get; set; }


        public decimal Shipping { get; set; }
        public byte ShippingTaxCode { get; set; }


        public decimal TotalDiscount { get; set; }


        [StringLength(100)]
        public string PoNumber { get; set; }
        public string ShipVia { get; set; }
        public string TrackingNo { get; set; }
        public string BillingAddress { get; set; }
        [StringLength(100)]
        public string BillEmail { get; set; }
        [StringLength(10)]
        public string Currency { get; set; }
        public string ShippingAddress { get; set; }
        public string MessageOnInvoice { get; set; }
        public string MessageOnStatment { get; set; }
        public string CustomerMemo { get; set; }
        public string Note { get; set; }
        public int? Quantity { get; set; } = 0;
        public string? GlobalTaxCalculation { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsPaid { get; set; } = false;
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public ICollection<SoItem> SoItems { get; set; }
        public ICollection<Tax> Taxes { get; set; }
        public ICollection<Refund> Refunds { get; set; }
        public ICollection<PaymentInvoice> PaymentInvoices { get; set; }
        public Users.User User { get; set; }


        private Customer _customer;
        public Customer Customer { get; set; }

        public Warehouse Warehouse { get; set; }
        public Term Term { get; set; }

    }
}
