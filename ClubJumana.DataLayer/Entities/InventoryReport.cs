using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class InventoryReport
    {
        public int Id { get; set; }
        public int ProductMasterFK { get; set; }
        public string Description { get; set; }
        public int Change { get; set; }
        public int OldBalance { get; set; }
        public int NewBalance { get; set; }
        public ProductMaster ProductMaster { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
    }
}
