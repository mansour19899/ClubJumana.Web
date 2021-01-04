using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Tax
    {
        public int Id { get; set; }
        [StringLength(15)]
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public int SalesOrderFK { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

        public SaleOrder SaleOrder { get; set; }
    }
}
