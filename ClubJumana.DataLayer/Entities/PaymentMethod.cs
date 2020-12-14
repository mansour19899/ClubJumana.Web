using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
