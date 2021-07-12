using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class TaxRate
    {
        public int Id { get; set; }
        public int TaxRateId { get; set; } = 0;
        public int TaxCodeId { get; set; } = 0;
        [StringLength(15)]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public bool Active { get; set; }
       
        [Timestamp]
        public DateTime RowVersion { get; set; }

    }
}
