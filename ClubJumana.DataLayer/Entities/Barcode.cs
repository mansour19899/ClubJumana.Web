﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class Barcode
    {
        public int Id { get; set; }

        [StringLength(15)]
        public string BarcodeNumber { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public bool Active { get; set; }
        public Variant Variant { get; set; }

    }
}
