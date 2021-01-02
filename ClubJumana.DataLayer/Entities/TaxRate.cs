using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class TaxRate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
       
        [Timestamp]
        public DateTime RowVersion { get; set; }

    }
}
