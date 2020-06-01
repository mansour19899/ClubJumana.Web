using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int TowelFK { get; set; }
        public int BedingFK { get; set; }

        public Towel Towel { get; set; }
        public Beding Beding { get; set; }
    }
}
