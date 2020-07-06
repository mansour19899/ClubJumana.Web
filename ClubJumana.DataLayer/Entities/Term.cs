using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Term
    {
        public int Id { get; set; }

        [StringLength(15)]
        public string Code { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int DueDateCalculation { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
