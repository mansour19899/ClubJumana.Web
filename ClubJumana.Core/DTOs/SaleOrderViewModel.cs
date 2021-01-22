using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ClubJumana.DataLayer.Entities;
using JetBrains.Annotations;
using ClubJumana.DataLayer.Entities.Users;
using ClubJumana.Core.Generator;

namespace ClubJumana.Core.DTOs
{
    public class SaleOrderViewModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();

            }
        }

        public bool AllowToCalculate = false;
        //WholeSale Is True and Retail is False
        public bool Type { get; set; }
        public bool HaveDeposit { get; set; } = false;
        public bool IsRefund { get; set; } = false;
        private Payment _deposit;
        public Payment Deposit
        {
            get
            { return _deposit; }
            set
            {
                _deposit = value;
                AmountDeposit = _deposit.AmountReceived;
                OnPropertyChanged();
            }
        }
        public Refund Refund { get; set; }

        private decimal _amountDeposit;

        public decimal AmountDeposit
        {
            get
            {
                if (IsSaveDatabase)
                    return _amountDeposit;
                else
                    return Math.Round(_amountDeposit, 2, MidpointRounding.AwayFromZero);

            }
            set
            {
                _amountDeposit = value;
                _deposit.AmountReceived = _amountDeposit;
                _openBalance = _soTotalPrice - _amountDeposit;
                OnPropertyChanged();
                OnPropertyChanged(nameof(OpenBalance));
            }
        }
        public DateTime? SoDate { get; set; }
        public DateTime? ExpriationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        private int? _invoiceNumber;
        public int? InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                _invoiceNumber = value;
                OnPropertyChanged();

            }
        }
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
                    return Math.Round(_subtotal, 2, MidpointRounding.AwayFromZero);

            }
            set
            {
                _subtotal = value;
                OnPropertyChanged();
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
                    return Math.Round(_soTotalPrice, 2, MidpointRounding.AwayFromZero);

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
        public decimal TermPercent { get; set; } = 0;
        private List<Tax> _taxes;

        public List<Tax> Taxes
        {
            get
            {
                return _taxes;
            }
            set { _taxes = value; }
        }


        private decimal _handling;

        public decimal Handling
        {
            get
            {
                if (IsSaveDatabase)
                    return _handling;
                else
                    return Math.Round(_handling, 2, MidpointRounding.AwayFromZero);

            }
            set
            {
                _handling = value;
                OnPropertyChanged();
                SumPrice();
                CalculateTotalPrice();
            }
        }

        private int _handlingTaxCode;
        public int HandlingTaxCode
        {
            get
            {
                return _handlingTaxCode;
            }
            set
            {
                _handlingTaxCode = value;
                OnPropertyChanged();
                SumPrice();
                CalculateTotalPrice();
            }
        }


        private decimal _Shipping;

        public decimal Shipping
        {
            get
            {
                if (IsSaveDatabase)
                    return _Shipping;
                else
                    return Math.Round(_Shipping, 2, MidpointRounding.AwayFromZero);


            }
            set
            {
                _Shipping = value;
                OnPropertyChanged();
                SumPrice();
                CalculateTotalPrice();
            }
        }

        private int _shippingTaxCode;
        public int ShippingTaxCode
        {
            get
            {
                return _shippingTaxCode;
            }
            set
            {
                _shippingTaxCode = value;
                OnPropertyChanged();
                SumPrice();
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
                    return Math.Round(_totalDiscount, 2, MidpointRounding.AwayFromZero);

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

        private decimal _openBalance;

        public decimal OpenBalance
        {
            get
            {
                if (IsSaveDatabase)
                    return _openBalance;
                else
                    return Math.Round(_openBalance, 2, MidpointRounding.AwayFromZero);

            }
            set
            {
                _openBalance = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SoItemVeiwModel> _soItems;
        public ObservableCollection<SoItemVeiwModel> SoItems
        {
            get { return _soItems; }
            set
            {
                _soItems = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<RefundItemsViewModel> _refundItems;
        public ObservableCollection<RefundItemsViewModel> RefundItems
        {
            get { return _refundItems; }
            set
            {
                _refundItems = value;
                OnPropertyChanged();
            }
        }
        public List<Province> Provinces { get; set; }

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



        private List<TaxRate> _taxRates;

        public List<TaxRate> TaxRates
        {
            get { return _taxRates; }
            set
            {
                _taxRates = value;
            }
        }

        public void CalculateTotalPrice()
        {
            if (AllowToCalculate)
            {
                _soTotalPrice = _subtotal + _Shipping + _handling;
                foreach (var VARIABLE in _taxes)
                {
                    _soTotalPrice += VARIABLE.TaxAmount;
                }


                OnPropertyChanged(nameof(Tax));
                OnPropertyChanged(nameof(SoTotalPrice));
            }

        }
        public void CalculateTotalRefundPrice()
        {
            if (AllowToCalculate)
            {
                Refund.RefundTotalPrice = Refund.SubtotalPrice + Refund.Shipping - Refund.Discount;
                foreach (var VARIABLE in Refund.TaxesRefunds)
                {
                    Refund.RefundTotalPrice += VARIABLE.TaxAmount;
                }

                OnPropertyChanged(nameof(Refund.RefundTotalPrice));
            }

        }
        public void CalculateTax(decimal price, int taxCode)
        {
            if (AllowToCalculate)
            {
                var ratecode = TaxRates.FirstOrDefault(p => p.Id == taxCode);
                var x = ratecode.Code.Split(' ');
                var xx = x[0].Split('/');
                decimal priceTax = 0;
                if (xx.Count() == 1)
                {
                    priceTax = Math.Round(price * ratecode.Rate / 100, 2, MidpointRounding.AwayFromZero);
                    var xxx = Taxes.FirstOrDefault(p => p.Code.Trim().CompareTo(ratecode.Code.Trim()) == 0);
                    if (xxx == null)
                    {
                        Taxes.Add(new Tax() { Rate = ratecode.Rate, Amount = price, TaxAmount = priceTax, Code = ratecode.Code });
                    }
                    else
                    {
                        xxx.Amount += price;
                        xxx.TaxAmount += priceTax;
                    }
                }
                else
                {
                    int IdRateCode;

                    IdRateCode = TaxRates.FirstOrDefault(p => p.Code.ToLower().Replace(" ", "").CompareTo((xx[0]).ToLower().Replace(" ", "")) == 0).Id;
                    CalculateTax(price, IdRateCode);

                    IdRateCode = TaxRates.FirstOrDefault(p => p.Code.ToLower().Replace(" ", "").CompareTo((xx[1] + x[1]).ToLower().Replace(" ", "")) == 0).Id;
                    CalculateTax(price, IdRateCode);


                }
                //if (tax == null)
                //{
                //    SaleOrderViewModel.Taxes.Add(new );
                //}
            }
        }

        public void CalculateRefundTax(decimal price, string taxCodeName)
        {
            if (AllowToCalculate)
            {
                var x = taxCodeName.Split(' ');
                var xx = x[0].Split('/');
                decimal priceTax = 0;
                decimal rate = 0;
                string tempcode = "";
                foreach (var item in xx)
                {
                    if (x.Count() == 2)
                    {
                        if (xx.Count() == 2&& item.Trim().CompareTo("GST")==0)
                            tempcode = item.Trim();
                        else
                            tempcode = item.Trim() + ' ' + x[1].Trim();
                    }
                    else if (x.Count() == 1)
                    {
                        tempcode = item.Trim();
                    }
                    else
                    {

                    }
                    rate = Taxes.FirstOrDefault(p => p.Code.Trim().CompareTo(tempcode) == 0).Rate;
                    priceTax = Math.Round(price * rate / 100, 2, MidpointRounding.AwayFromZero);
                    var xxx = Refund.TaxesRefunds.FirstOrDefault(p => p.Code.Trim().CompareTo(tempcode) == 0);
                    if (xxx == null)
                    {
                        Refund.TaxesRefunds.Add(new TaxRefund() { Rate = rate, Amount = price, TaxAmount = priceTax, Code = tempcode});
                    }
                    else
                    {
                        xxx.Amount += price;
                        xxx.TaxAmount += priceTax;
                    }

                }
            }
        }

        public void SumPrice()
        {
            if (AllowToCalculate)
            {
                _subtotal = 0;
                _soTotalPrice = 0;
                _totalDiscount = 0;
                _taxes.Clear();
                foreach (var item in SoItems)
                {
                    _subtotal += item.TotalPrice;
                    _totalDiscount = (item.Quantity * item.Price - item.TotalPrice) + _totalDiscount;
                    if (item.TaxCode != 0)
                        CalculateTax(item.TotalPrice, item.TaxCode);
                }
                if (_Shipping != 0)
                    CalculateTax(_Shipping, _shippingTaxCode);
                if (_handling != 0)
                    CalculateTax(_handling, _handlingTaxCode);
                OnPropertyChanged(nameof(Subtotal));
                OnPropertyChanged(nameof(TotalDiscount));
                CalculateTotalPrice();
            }

        }
        public void SumRefundPrice()
        {
            if (AllowToCalculate)
            {
                Refund.SubtotalPrice = 0;
                Refund.RefundTotalPrice = 0;
                Refund.TaxesRefunds.Clear();
                foreach (var item in RefundItems)
                {
                    Refund.SubtotalPrice += item.TotalPrice;
                    if (item.TaxCode != 0)
                        CalculateRefundTax(item.TotalPrice, item.TaxCodeName);
                }
                if (!String.IsNullOrWhiteSpace(Refund.ShippingTaxCode))
                    CalculateRefundTax(Refund.Shipping, Refund.ShippingTaxCode);
                CalculateTotalRefundPrice();
            }

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


        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                _priceTerm = Math.Round(_price * _termPercent + _price, 2, MidpointRounding.AwayFromZero);
                if (_discount != 0)
                    _totalPrice = _quantity * _priceTerm - (_quantity * _priceTerm * _discount / 100);
                else
                    _totalPrice = _quantity * _priceTerm;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PriceTerm));
                OnPropertyChanged(nameof(TotalPrice));


            }
        }

        private decimal _termPercent = 0;
        public decimal TermPercent
        {
            get { return _termPercent; }
            set
            {
                _termPercent = value / 100;
                _priceTerm = Math.Round(_price * _termPercent + _price, 2, MidpointRounding.AwayFromZero);
                if (_discount != 0)
                    _totalPrice = _quantity * _priceTerm - (_quantity * _priceTerm * _discount / 100);
                else
                    _totalPrice = _quantity * _priceTerm;

                OnPropertyChanged();
                OnPropertyChanged(nameof(PriceTerm));
                OnPropertyChanged(nameof(TotalPrice));
            }

        }
        private decimal _priceTerm = 0;
        public decimal PriceTerm
        {
            get { return _priceTerm; }
            set { _priceTerm = value; }
        }




        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                if (_discount == 0)
                    _totalPrice = _quantity * _priceTerm;
                else
                    _totalPrice = _quantity * _priceTerm - (_quantity * _priceTerm * _discount / 100);

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
                    _totalPrice = _quantity * _priceTerm - (_quantity * _priceTerm * _discount / 100);
                else
                    _totalPrice = _quantity * _priceTerm;
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

        private int _taxCode;
        public int TaxCode
        {
            get { return _taxCode; }
            set
            {
                _taxCode = value;
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

    public class RefundItemsViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int Refund_fk { get; set; }
        public ProductMaster ProductMaster { get; set; }
        public int? AbleReturn { get; set; }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                _totalPrice = Math.Round(value * Price, 2, MidpointRounding.ToEven);
            }
        }
        public int TaxCode { get; set; }
        public string TaxCodeName { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }

        private decimal _totalPrice = 0;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        public decimal OpenBalance { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public decimal SalesTax { get; set; }
        public decimal Total { get; set; }
    }

    public class CustomersInvoiceViewModel
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string Description { get; set; }
        private int? _invoiceNumber = -1;
        public int? InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                _invoiceNumber = value;
                if (_invoiceDate != null)
                    Description = $"Invoice # {_invoiceNumber.Value.ShowInvoceNumber()} ({_invoiceDate.Value.ToShortDateString()})";
                // OnPropertyChanged();

            }
        }
        private DateTime? _invoiceDate;
        public DateTime? InvoiceDate
        {
            get { return _invoiceDate; }
            set
            {
                _invoiceDate = value;
                Description = $"Invoice # {_invoiceNumber.Value.ShowInvoceNumber()} ({_invoiceDate.Value.ToShortDateString()})";
                // OnPropertyChanged();

            }
        }
        public DateTime? DueDate { get; set; }
        public decimal OrginalAmount { get; set; }
        public decimal OpenBalance { get; set; }
        public decimal Payment { get; set; }
    }
}
