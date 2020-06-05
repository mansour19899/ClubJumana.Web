using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public decimal? Duty { set; get; }
        public decimal? ExChangeRate { set; get; }

        public string Currency { get; set; }
        public string CurrencyName { get; set; }
        public string DigitalCode { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
