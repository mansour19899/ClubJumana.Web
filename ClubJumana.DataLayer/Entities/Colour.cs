using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class Colour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PantoneNumber { get; set; }
        public string Code { get; set; }

        public ICollection<Variant> Variants { get; set; }

    }
}
