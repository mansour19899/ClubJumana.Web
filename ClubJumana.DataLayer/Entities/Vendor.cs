using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJumana.DataLayer.Entities
{
    public class Vendor
    {
        public int Id { get; set; }

        [StringLength(40)]
        public  string Name { get; set; }
        [StringLength(100)]
        public  string Address1 { get; set; }
        [StringLength(100)]
        public  string Address2 { get; set; }
        [StringLength(100)]
        public  string Address3 { get; set; }
        [StringLength(20)]
        public  string PostalCode { get; set; }
        [StringLength(20)]
        public  string Country { get; set; }
        [StringLength(100)]
        public  string Title { get; set; }
        [StringLength(40)]
        public  string FirstName { get; set; }
        [StringLength(40)]
        public  string LastName { get; set; }
        [StringLength(20)]
        public  string Phone1 { get; set; }
        [StringLength(20)]
        public  string Phone2 { get; set; }
        [StringLength(40)]
        public  string Email { get; set; }
        [StringLength(20)]
        public  string Acountsharp { get; set; }
        public  string PaymentTerms { get; set; }
        public  string TradeDiscountPercent { get; set; }
        public  string Currency { get; set; }
        public  string LeadTime { get; set; }
        public  string Info1 { get; set; }
        public  string Info2 { get; set; }
        public  string Note { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }

    }
}
