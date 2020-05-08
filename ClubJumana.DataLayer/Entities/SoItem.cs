using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ClubJumana.DataLayer.Entities
{
    public class SoItem:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int So_fk { get; set; }
        public int ProductMaster_fk { get; set; }
        public decimal Cost { get; set; }

        private decimal _discount=0;

        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                if(_discount==0)
                    _totalPrice = _quantity * _price;
                else
                    _totalPrice = _quantity * _price - (_quantity * _price * _discount / 100);

                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));


            }
        }

        private int _quantity=0;

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


        public SaleOrder SaleOrder { get; set; }
        public ProductMaster ProductMaster { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
