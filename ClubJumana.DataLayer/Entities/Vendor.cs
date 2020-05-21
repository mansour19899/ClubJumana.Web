using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJumana.DataLayer.Entities
{
    public class Vendor
    {
        public int Id { get; set; }

        public  string Name { get; set; }
        public  string Address1 { get; set; }
        public  string Address2 { get; set; }
        public  string Address3 { get; set; }
        public  string PostalCode { get; set; }
        public  string Country { get; set; }
        public  string Title { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Phone1 { get; set; }
        public  string Phone2 { get; set; }
        public  string Email { get; set; }
        public  string Acountsharp { get; set; }
        public  string PaymentTerms { get; set; }
        public  string TradeDiscountPercent { get; set; }
        public  string Currency { get; set; }
        public  string LeadTime { get; set; }
        public  string Info1 { get; set; }
        public  string Info2 { get; set; }
        public  string Note { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }

    }
}
