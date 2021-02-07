using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class UOM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

        public ICollection<Variant> Variants { get; set; }
        public ICollection<ProductMaster> ProductMasters { get; set; }
    }
}
