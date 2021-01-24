using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.DataLayer.Entities
{
   public class TaxRefund
    {
        public int Id { get; set; }
        [StringLength(15)]
        public string Code { get; set; }
        public decimal Rate { get; set; }
        private decimal _amount;
        public decimal Amount
        {
            get
            {
                return Math.Round(_amount, 2, MidpointRounding.AwayFromZero);
            }
            set
            {
                _amount = value;
            }
        }
        private decimal _taxAmount;
        public decimal TaxAmount
        {
            get
            {
                return Math.Round(_taxAmount, 2, MidpointRounding.AwayFromZero);
            }
            set
            {
                _taxAmount = value;
            }
        }
        public int RefundFK { get; set; }
        public Refund Refund { get; set; }
        public DateTime RowVersion { get; set; }
    }
}
