using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubJumana.DataLayer.Entities.Users;
using Org.BouncyCastle.Crypto.Tls;

namespace ClubJumana.DataLayer.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone1 { get; set; }
        public string Mobile { get; set; }
        public string FaxNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }

        public decimal BalanceLCY { get; set; }
        public decimal BalanceDueLCY { get; set; }
        public decimal CreditLimitLCY { get; set; }
        public decimal TotalSales { get; set; }
        public decimal CostsLCY { get; set; }

        private string _imageName;
        public string ImageName
        {
            get => string.IsNullOrEmpty(_imageName) ? "\\Images\\NoImage.png" : $"\\Images\\Customers\\{_imageName}";
            set => _imageName = value;
        }
        public string Note { get; set; }
        public int CreatedBy_fk { get; set; }
        public int? ProvinceFK { get; set; }
        public int? CountryFK { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public bool Active { get; set; }

        public ICollection<SaleOrder> SaleOrders { get; set; }
        public  Users.User User { get; set; }
        public Country Country { get; set; }
        public Province Province { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

        //--------------------------- Noy map --------------------------

        public string FullName
        {
            get { return ContactName + " " + ContactLastName; }
        }
    }
}
