using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int VariantFK { get; set; }

        public Variant Variant { get; set; }
    }
}
