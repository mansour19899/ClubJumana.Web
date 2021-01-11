using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace ClubJumana.DataLayer.Entities
{
   public class Payment : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }=DateTime.Now;
        public string ReferenceNo { get; set; }
        public int PaymentMethodFK { get; set; }
        public int DepositToFK { get; set; }
        private decimal _amountReceived=0;

        public decimal AmountReceived
        {
            get
            {
                return _amountReceived;
            }
            set
            {
                _amountReceived = value;
                OnPropertyChanged();
            }
        }
        private decimal _openBalance = 0;
        public decimal OpenBalance
        {
            get
            {
                return _openBalance;
            }
            set
            {
                _openBalance = value;
                OnPropertyChanged();
            }
        }
        public string Note { get; set; }
        public string Attachments { get; set; }
        public ICollection<PaymentInvoice> PaymentInvoices { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual DepositTo DepositTo { get; set; }
        public DateTime RowVersion { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
