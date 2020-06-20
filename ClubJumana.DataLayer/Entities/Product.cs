using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Product
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string StyleNumber { get; set; }
        [StringLength(100)]
        public string ProductTittle { get; set; }
        public int? BrandFK { get; set; }
        public int? MaterialFK { get; set; }
        public int? CompanyFK { get; set; }
        public int? CountryOfOrginFK { get; set; }
        public string DescribeMaterial { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public  Brand Brand { get; set; }
        public  Company Company { get; set; }
        public  Country CountryOfOrgin { get; set; }
        public Material Material { get; set; }

        public ICollection<Variant> Variants { get; set; }
    }
}
