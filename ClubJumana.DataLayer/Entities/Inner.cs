using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Inner
    {
        public int Id { get; set; }
        public int ProductMasterFK { get; set; }
        public string ITF14 { get; set; }
        public int Quantity { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public ICollection<InnerMasterCarton> InnerMasterCartons { get; set; }
        public ProductMaster ProductMaster { get; set; }

    }
}
