using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ClubJumana.DataLayer.Entities
{
    public class SoItem
    {
        public int Id { get; set; }
        public int So_fk { get; set; }
        public int ProductMaster_fk { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int? QuantityRefunded { get; set; } = 0;
        public bool? IsAbaleToRefund { get; set; } = true;
        public decimal Price { get; set; }
        public decimal PriceTerm { get; set; }
        public decimal TotalPrice { get; set; }

        public SaleOrder SaleOrder { get; set; }
        public ProductMaster ProductMaster { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

    }
}
