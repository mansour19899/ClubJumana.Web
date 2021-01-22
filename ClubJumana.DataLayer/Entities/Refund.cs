using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ClubJumana.DataLayer.Context;

namespace ClubJumana.DataLayer.Entities
{
   public class Refund
    {
        
        public int Id { get; set; }
        public DateTime? RefundDate { get; set; }
        public int? RefundNumber { get; set; }
        public string Email { get; set; }
        public string ChequeNo { get; set; }
        public string BillingAddress { get; set; }
        public string MessageOnRefund { get; set; }
        public string MessageOnStatement { get; set; }
        public int SaleOrderFK{ get; set; }
        public int? PaymentMethodFK { get; set; }
        public int? RefundFromFK { get; set; }
        public int? WarehouseId { get; set; }

        private decimal _refundTotalPrice;

        public decimal RefundTotalPrice
        {
            get { return _refundTotalPrice; }
            set { _refundTotalPrice = value; }
        }

        private decimal _subtotalPrice;

        public decimal SubtotalPrice
        {
            get { return _subtotalPrice; }
            set
            {
                _subtotalPrice = value;
                //_tax = Math.Round(_subtotalPrice * 0.13m, 2, MidpointRounding.ToEven);
                //_refundTotalPrice = _subtotalPrice + _tax;
            }

        }
        private decimal _shipping;
        public string ShippingTaxCode { get; set; }
        public decimal Shipping
        {
            get { return _shipping; }
            set
            {
                _shipping = value;
                //_tax = Math.Round(_subtotalPrice * 0.13m, 2, MidpointRounding.ToEven);
                //_refundTotalPrice = _subtotalPrice + _tax;
            }

        }
        private decimal _discount=0;

        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                //_tax = Math.Round(_subtotalPrice * 0.13m, 2, MidpointRounding.ToEven);
                //_refundTotalPrice = _subtotalPrice + _tax;
            }

        }

        //Persntage True Value False
        public bool TypeOfDiscount { get; set; } = false;

        public ICollection<TaxRefund> TaxesRefunds { get; set; }

        public SaleOrder SaleOrder { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual DepositTo RefundFrom { get; set; }
        public ICollection<RefundItem> RefundItems { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

    }
}
