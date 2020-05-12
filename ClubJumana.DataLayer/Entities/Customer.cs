using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.DataLayer.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Company { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PostalCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        private string _imageName;
        public string ImageName
        {
            get => string.IsNullOrEmpty(_imageName) ? "\\Images\\NoImage.png" : $"\\Images\\Customers\\{_imageName}";
            set => _imageName = value;
        }
        public string Note { get; set; }
        public int CreatedBy_fk { get; set; }
        public DateTime? EditedDate { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public bool Active { get; set; }

        public ICollection<SaleOrder> SaleOrders { get; set; }
        public  Users.User User { get; set; }

        //--------------------------- Noy map --------------------------

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
