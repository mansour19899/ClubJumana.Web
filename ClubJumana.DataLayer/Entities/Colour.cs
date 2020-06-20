using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class Colour
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(40)]
        public string PantoneNumber { get; set; }
        [StringLength(5)]
        public string Code { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

        public ICollection<Variant> Variants { get; set; }

    }
}
