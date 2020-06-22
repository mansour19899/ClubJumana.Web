using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class TablesVersion
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public Int64 RowVersion { get; set; }
        public bool NeedToUpdate { get; set; } = false;
    }
}
