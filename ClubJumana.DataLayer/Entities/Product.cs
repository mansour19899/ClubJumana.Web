using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
    class Product
    {
        public int Id { get; set; }
        public string StyleNumber { get; set; }
        public int? BrandFK { get; set; }
        public int? MaterialFK { get; set; }
        public int? CompanyFK { get; set; }
        public int? CountryOfOrginFK { get; set; }
        public string DescribeMaterial { get; set; }

        


    }
}
