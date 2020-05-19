using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Entities.Users;
using JetBrains.Annotations;

namespace ClubJumana.Core.DTOs
{
    public class PurchaseOrderViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int? Vendor_fk { get; set; }
        public string Associate { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public virtual decimal? TotalPrice { get; set; }
        public decimal? SubtotalPrice { get; set; }
        public int? ItemsCount { get; set; }
        public int? ToWarehouse_fk { get; set; }
        public int? FromWarehouse_fk { get; set; }
        public int? ApproveUser_fk { get; set; }
        public bool CreatedInvoice { get; set; }
        public ICollection<Item> Items { get; set; }
        public Vendor Vendor { get; set; }
        public User UserCreate { get; set; }
        public Warehouse ToWarehouse { get; set; }
        public Warehouse FromWarehouse { get; set; }
        public List<Vendor> VendorsList { get; set; }
        public List<Warehouse> WarehousesList { get; set; }
    }
    public class PoViewModel:PurchaseOrderViewModel
    {
        
    }
    public class AsnViewModel : PurchaseOrderViewModel
    {

    }
    public class GrnViewModel : PurchaseOrderViewModel , INotifyPropertyChanged
    {
        private decimal _totalPrice;
        public override decimal? TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value.Value;
                OnPropertyChanged();
            }
        }


        private decimal _freight;
        public decimal Freight
        {
            get { return _freight; }
            set
            {
                _freight = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }

        private decimal _discountPercent;
        public decimal DiscountPercent
        {
            get { return _discountPercent; }
            set
            {
                _discountPercent = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }
        private string _percent;
        public string Percent
        {
            get { return _percent; }
            set
            {
                _percent = value.Replace("-", "").Replace("%", "");

                if (String.IsNullOrWhiteSpace(_percent))
                    _percent = "0";
              
                _discountPercent = Math.Round(Convert.ToDecimal(_percent) * SubtotalPrice.Value / 100, 2, MidpointRounding.ToEven)*-1;
                _totalCharge = _freight+_discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                _percent = _percent + " %";
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
                OnPropertyChanged(nameof(DiscountPercent));
            }
        }
        private decimal _discountDollers;
        public decimal DiscountDollers
        {
            get { return _discountDollers; }
            set
            {
                _discountDollers = (value>0)? (-1 * value) :value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }
        private decimal _insurance;
        public decimal Insurance
        {
            get { return _insurance; }
            set
            {
                _insurance = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }
        private decimal _customsDuty;
        public decimal CustomsDuty
        {
            get { return _customsDuty; }
            set
            {
                _customsDuty = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }
        private decimal _handling;
        public decimal Handling
        {
            get { return _handling; }
            set
            {
                _handling = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }
        private decimal _forwarding;
        public decimal Forwarding
        {
            get { return _forwarding; }
            set
            {
                _forwarding = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }
        private decimal _landTransport;
        public decimal LandTransport
        {
            get { return _landTransport; }
            set
            {
                _landTransport = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }
        private decimal _others;

        public decimal Others
        {
            get { return _others; }
            set
            {
                _others = value;
                _totalCharge = _freight + _discountPercent + _discountDollers + _insurance + _customsDuty + _handling + _forwarding + _landTransport + _others;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCharges));
            }
        }

        private decimal _totalCharge;

        public decimal TotalCharges
        {
            get { return _totalCharge; }
            set => _totalCharge = value;
        }

        public ObservableCollection<ItemsOfPurchaseOrderViewModel> CalculateCost(
            ObservableCollection<ItemsOfPurchaseOrderViewModel> items)
        {
            decimal x = 0;
            foreach (var VARIABLE in items)
            {

                x = (decimal) ((TotalCharges/ SubtotalPrice * VARIABLE.Price) +
                               VARIABLE.Price);
                VARIABLE.Cost = Math.Round(x, 2, MidpointRounding.ToEven);
            }

            return items;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TotalFreightsViewModel 
    {
        public decimal? Freight { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string Percent { get; set; }
        public decimal? DiscountDollers { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? CustomsDuty { get; set; }
        public decimal? Handling { get; set; }
        public decimal? Forwarding { get; set; }
        public decimal? LandTransport { get; set; }
        public decimal? Others { get; set; }
        public decimal? TotalCharges { get; set; }


    }

    public class ItemsOfPurchaseOrderViewModel : INotifyPropertyChanged
    {
        public ItemsOfPurchaseOrderViewModel()
        {
            
        }
        public int Id { get; set; }
        public int Po_fk { get; set; }
        public int ProductMaster_fk { get; set; }
        public int PreviousQuantity { get; set; }
        private int _quantity = 0;

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                _TotalItemPrice = _quantity * _price;
                Diffrent = value - PreviousQuantity;
                if (Diffrent != 0)
                {
                    Alert = true;
                }
                else
                {
                    Alert = false;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalItemPrice));

            }
        }

        private decimal _price=0;

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                _TotalItemPrice = _quantity * _price;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalItemPrice));
            }
        }
        private decimal _cost = 0;
        public decimal Cost
        {
            get => _cost;
            set
            {
                _cost = value;

                OnPropertyChanged();
            }

        }

        private decimal _TotalItemPrice;

        public decimal TotalItemPrice
        {
            get { return _TotalItemPrice; }
            set { _TotalItemPrice = value; }
        }
        public int? Diffrent { get; set; }
        public bool? Alert { get; set; }
        public string Note { get; set; }
        public bool? Checked { get; set; }
        private bool _isChanged = false;
        public bool IsChanged {
            get { return _isChanged; }
            set { _isChanged = value; }
        } 
        public bool IsDeleted { get; set; }
        public ProductMaster ProductMaster { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _isChanged = true;
        }
    }
}