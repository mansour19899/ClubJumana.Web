using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }=DateTime.Now;
        public string ReferenceNo { get; set; }
        public int PaymentMethodFK { get; set; }
        public int DepositToFK { get; set; }
        public decimal AmountReceived { get; set; } = 0;
        public string Note { get; set; }
        public string Attachments { get; set; }
        public ICollection<PaymentInvoice> PaymentInvoices { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual DepositTo DepositTo { get; set; }
        public DateTime RowVersion { get; set; }
    }
}
