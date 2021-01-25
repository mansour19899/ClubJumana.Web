using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using ClubJumana.DataLayer.Context;
using JetBrains.Annotations;

namespace ClubJumana.DataLayer.Entities
{
   public class Refund : INotifyPropertyChanged
    {
        
        public int Id { get; set; }
        public DateTime? RefundDate { get; set; }
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
            set
            {
                _refundTotalPrice = value;
                OnPropertyChanged();
            }
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }

        }
        private decimal _discountAmount = 0;

        public decimal DiscountAmount
        {
            get { return _discountAmount; }
            set
            {
                _discountAmount = value;
                //_tax = Math.Round(_subtotalPrice * 0.13m, 2, MidpointRounding.ToEven);
                //_refundTotalPrice = _subtotalPrice + _tax;
                OnPropertyChanged();
            }

        }
        private decimal _discount=0;

        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                if (TypeOfDiscount)
                {
                    _discountAmount = Math.Round(_discount * _subtotalPrice / 100m, 2, MidpointRounding.AwayFromZero);;
                }
                else
                {
                    _discountAmount = _discount;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(DiscountAmount));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
