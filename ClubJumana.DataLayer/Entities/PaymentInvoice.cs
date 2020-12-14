using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
    public class PaymentInvoice
    {
        public int Id { get; set; }
        public int InvoiceFK { get; set; }
        public int PaymenteFK { get; set; }
        public decimal Amount { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual SaleOrder Invoice { get; set; }
    }
}
