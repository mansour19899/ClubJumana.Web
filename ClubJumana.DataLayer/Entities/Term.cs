using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Term
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int DueDateCalculation { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public bool Active { get; set; } = true;
    }
}
