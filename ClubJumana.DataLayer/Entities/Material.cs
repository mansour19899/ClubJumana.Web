using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class Material
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
