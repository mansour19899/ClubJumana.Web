using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class MasterCarton
    {
        public int Id { get; set; }
        public string ITF14 { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Lenght { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public ICollection<InnerMasterCarton> InnerMasterCartons { get; set; }

    }
}
