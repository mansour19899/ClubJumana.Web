﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class Barcode
    {
        public int Id { get; set; }
        public string BarcodeNumber { get; set; }
        public bool Active { get; set; }

       public Towel Towel { get; set; }
       public Beding Beding { get; set; }

    }
}
