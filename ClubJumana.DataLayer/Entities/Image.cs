using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Image
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string ImageName { get; set; }
        public int VariantFK { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

        public Variant Variant { get; set; }
    }
}
