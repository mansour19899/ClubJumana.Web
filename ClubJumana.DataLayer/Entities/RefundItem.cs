﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class RefundItem
    {
        public int Id { get; set; }
        public int Refund_fk { get; set; }
        public int ProductMaster_fk { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string TaxCodeName { get; set; }
        public Refund Refund { get; set; }
        public ProductMaster ProductMaster { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
    }
}
