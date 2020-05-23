using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClubJumana.DataLayer.Entities.Users;
using JetBrains.Annotations;

namespace ClubJumana.DataLayer.Entities
{
    public class SaleOrder {
        public int Id { get; set; }
        //WholeSale Is True and Retail is False
        public bool Type { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public int? SalesOrderNumber { get; set; }
        public int? InvoiceNumber { get; set; }
        public int? Cashier_fk { get; set; }
        public int? Customer_fk { get; set; }
        public int? Warehouse_fk { get; set; }
        public int? ShipMethod_fk { get; set; }

        public decimal Subtotal { get; set; }

        public decimal SoTotalPrice { get; set; }

        public int? TaxArea_fk { get; set; }

        public decimal Tax { get; set; }

        public decimal Handling { get; set; }


        public decimal Freight { get; set; }


        public decimal TotalDiscount { get; set; }

        public string ShipToAddressName { get; set; }
        public string ShipToAddressNam1 { get; set; }
        public string ShipToAddressNam2 { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToPostalPhone1 { get; set; }
        public int? Quantity { get; set; } = 0;

        public bool IsDeleted { get; set; } = false;
        public byte[] RowVersion { get; set; }

        public ICollection<SoItem> SoItems { get; set; }
        public ICollection<Refund> Refunds { get; set; }
        public Users.User User { get; set; }

        private Customer _customer;
        public Customer Customer { get; set; }

        public Province TaxArea { get; set; }
        public Warehouse Warehouse { get; set; }

    }
}
