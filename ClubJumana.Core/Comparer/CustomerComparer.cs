using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class CustomerComparer : IEqualityComparer<Customer>
    {
        public bool Equals(Customer x, Customer y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }



            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id && x.CompanyName == y.CompanyName && x.Gender == y.Gender && x.BirthDate == y.BirthDate &&
                   x.ContactName == y.ContactName && x.ContactLastName == y.ContactName && x.Address1 == y.Address1 &&
                   x.Address2 == y.Address2 && x.Website == y.Website && x.PostalCode == y.PostalCode &&
                   x.Phone1 == y.Phone1 && x.Email == y.Email && x.ImageName == y.ImageName && x.Note == y.Note &&
                   x.CreatedBy_fk == y.CreatedBy_fk && x.City == y.City &&
                   x.LastSaleDate == y.LastSaleDate && x.Active == y.Active && x.RowVersion == y.RowVersion;
        }

        public int GetHashCode(Customer obj)
        {
            if (obj == null)
            {
                return 0;
            }
            int IDHashCode = obj.Id.GetHashCode();

            return IDHashCode;
        }
    }
}
