using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Manufacture { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvinceRegion { get; set; }
        public string ZipPostlCode { get; set; }
        public int CountryFK { get; set; }
        public string Phone { get; set; }
        public string FAX { get; set; }
        public ICollection<Product> Products { get; set; }
        public Country Country { get; set; }
    }
}
