using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Product
    {
        public int Id { get; set; }
        public string StyleNumber { get; set; }
        public string ProductTittle { get; set; }
        public int? BrandFK { get; set; }
        public int? MaterialFK { get; set; }
        public int? CompanyFK { get; set; }
        public int? CountryOfOrginFK { get; set; }
        public string DescribeMaterial { get; set; }
        public byte[] RowVersion { get; set; }
        public  Brand Brand { get; set; }
        public  Company Company { get; set; }
        public  Country CountryOfOrgin { get; set; }
        public Material Material { get; set; }

        public ICollection<Towel> Towels { get; set; }
        public ICollection<Beding> Bedings { get; set; }
    }
}
