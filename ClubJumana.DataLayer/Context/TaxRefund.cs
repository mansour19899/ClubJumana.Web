using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.DataLayer.Context
{
   public class TaxRefund
    {
        public int Id { get; set; }
        [StringLength(15)]
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public int RefundFK { get; set; }
        public Refund Refund { get; set; }
        public DateTime RowVersion { get; set; }
    }
}
