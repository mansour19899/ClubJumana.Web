using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class InnerMasterCarton
    {
        public int Id { get; set; }
        public int InnerFK { get; set; }
        public int MasterCartonFK { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public int InnerQuntity { get; set; }

        public virtual Inner Inner { get; set; }
        public virtual MasterCarton MasterCarton { get; set; }
    }
}
