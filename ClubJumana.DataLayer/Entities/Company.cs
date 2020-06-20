using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Company
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string CompanyName { get; set; }
        [StringLength(100)]
        public string Manufacture { get; set; }
        [StringLength(100)]
        public string Website { get; set; }
        [StringLength(40)]
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvinceRegion { get; set; }
        [StringLength(20)]
        public string ZipPostlCode { get; set; }
        public int CountryFK { get; set; }
        [StringLength(40)]
        public string Phone { get; set; }
        [StringLength(40)]
        public string FAX { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public ICollection<Product> Products { get; set; }
        public Country Country { get; set; }
    }
}
