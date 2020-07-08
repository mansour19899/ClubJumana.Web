using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ClubJumana.DataLayer.Entities;
using JetBrains.Annotations;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.Core.DTOs
{
    public class SaleOrderViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        //WholeSale Is True and Retail is False
        public bool Type { get; set; }
        public DateTime? SoDate { get; set; }
        public DateTime? ExpriationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? InvoiceNumber { get; set; }
        public int? Cashier_fk { get; set; }
        public int? Customer_fk { get; set; }
        public int? Warehouse_fk { get; set; }
        public int? ShipMethod_fk { get; set; }

        private decimal _subtotal;

        public decimal Subtotal
        {
            get
            {
                if (IsSaveDatabase)
                    return _subtotal;
                else
                    return Math.Round(_subtotal, 2, MidpointRounding.ToEven);

            }
            set
            {
                _subtotal = value;
                _subtotalwithServices = _subtotal + _freight + _handling;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SubtotalwithServices));
                CalculateTotalPrice();
            }
        }

        private decimal _soTotalPrice;

        public decimal SoTotalPrice
        {
            get
            {
                if (IsSaveDatabase)
                    return _soTotalPrice;
                else
                    return Math.Round(_soTotalPrice, 2, MidpointRounding.ToEven);

            }
            set
            {
                _soTotalPrice = value;
                OnPropertyChanged();
            }
        }

        public int? TaxArea_fk { get; set; }
        public int? term_fk { get; set; }
        public Term Term { get; set; }
        private decimal _tax;

        public decimal Tax
        {
            get
            {
                if (IsSaveDatabase)
                    return _tax;
                else
                    return Math.Round(_tax, 2, MidpointRounding.ToEven);
            }
            set { _tax = value; }
        }


        private decimal _handling;

        public decimal Handling
        {
            get
            {
                if (IsSaveDatabase)
                    return _handling;
                else
                    return Math.Round(_handling, 2, MidpointRounding.ToEven);

            }
            set
            {
                _handling = value;
                _subtotalwithServices = _subtotal + value + _freight;
                OnPropertyChanged(nameof(SoTotalPrice));
                OnPropertyChanged(nameof(SubtotalwithServices));
                OnPropertyChanged();
                CalculateTotalPrice();
            }
        }


        private decimal _freight;

        public decimal Freight
        {
            get
            {
                if (IsSaveDatabase)
                    return _freight;
                else
                    return Math.Round(_freight, 2, MidpointRounding.ToEven);


            }
            set
            {
                _freight = value;
                _subtotalwithServices = _subtotal + value + _handling;
                OnPropertyChanged(nameof(SoTotalPrice));
                OnPropertyChanged(nameof(SubtotalwithServices));
                OnPropertyChanged();
                CalculateTotalPrice();
            }
        }

        private decimal _totalDiscount;

        public decimal TotalDiscount
        {
            get
            {
                if (IsSaveDatabase)
                    return _totalDiscount;
                else
                    return Math.Round(_totalDiscount, 2, MidpointRounding.ToEven);

            }
            set
            {
                _totalDiscount = value;
                OnPropertyChanged();
            }
        }
        public string PoNumber { get; set; }
        public string ShipVia { get; set; }
        public string TrackingNo { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string MessageOnInvoice { get; set; }
        public string MessageOnStatment { get; set; }
        public string Note { get; set; }
        public int? Quantity { get; set; } = 0;

        public bool IsDeleted { get; set; } = false;


        public ObservableCollection<SoItemVeiwModel> SoItems { get; set; }

        public User User { get; set; }

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged();

            }
        }
        public Province TaxArea { get; set; }
        public Warehouse Warehouse { get; set; }

        public string TaxName { get; set; }

        private decimal _subtotalwithServices;

        public decimal SubtotalwithServices
        {
            get
            {
                if (IsSaveDatabase)
                    return _subtotalwithServices;
                else
                    return Math.Round(_subtotalwithServices, 2, MidpointRounding.ToEven);

            }
            set
            {
                _subtotalwithServices = value;
                OnPropertyChanged();
            }
        }

        private List<decimal> _taxRate;

        public List<decimal> TaxRate
        {
            get { return _taxRate; }
            set
            {
                _taxRate = value;
                CalculateTotalPrice();
            }
        }

        private void CalculateTotalPrice()
        {
            _tax = 0;
            foreach (var VARIABLE in TaxRate)
            {
                if (VARIABLE != 0)
                    _tax = VARIABLE * _subtotalwithServices + _tax;
            }

            _soTotalPrice = _subtotalwithServices + _tax;
            OnPropertyChanged(nameof(Tax));
            OnPropertyChanged(nameof(SoTotalPrice));
        }

        public bool IsSaveDatabase { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SoItemVeiwModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int So_fk { get; set; }
        public int ProductMaster_fk { get; set; }
        public decimal Cost { get; set; }

        private decimal _discount = 0;

        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                if (_discount == 0)
                    _totalPrice = _quantity * _price;
                else
                    _totalPrice = _quantity * _price - (_quantity * _price * _discount / 100);

                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));


            }
        }

        private int _quantity = 0;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                if (_discount != 0)
                    _totalPrice = _quantity * _price - (_quantity * _price * _discount / 100);
                else
                    _totalPrice = _quantity * _price;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));

            }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                if (_discount != 0)
                    _totalPrice = _quantity * _price - (_quantity * _price * _discount / 100);
                else
                    _totalPrice = _quantity * _price;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }
        public int? QuantityRefunded { get; set; }
        public bool? IsAbaleToRefund { get; set; }

        public bool IsDeleted { get; set; }

        public SaleOrderViewModel SaleOrderViewsModel { get; set; }
        public ProductMaster ProductMaster { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RefundItemsViewModel
    {
        public int Id { get; set; }
        public int Refund_fk { get; set; }
        public string StyleNumber { get; set; }
        public string UPC { get; set; }
        public int ProductMaster_fk { get; set; }
        public int? AbleReturn { get; set; }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                _totalPrice = Math.Round(value * Price, 2, MidpointRounding.ToEven) ;
            }
        }

        public decimal Cost { get; set; }
        public decimal Price { get; set; }

        private decimal _totalPrice=0;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }

    }

    public class CustomerListview
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string LocationCode { get; set; }
        public string PhoneNo { get; set; }
        public string Contact { get; set; }
        public string BalanceLCY { get; set; }
        public string BalanceDueLCY { get; set; }
    }

    public class SalesOrderListview
    {
        public int No { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public decimal SalesTax { get; set; }
        public decimal Total { get; set; }
    }
}
