using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class CompanyComparer : IEqualityComparer<Company>
    {
        public bool Equals(Company x, Company y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }


            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Id == y.Id && x.CompanyName == y.CompanyName && x.Manufacture == y.Manufacture && x.Website == y.Website && x.Email == y.Email && x.StreetAddress == y.StreetAddress && x.AddressLine2 == y.AddressLine2 && x.City == y.City && x.StateProvinceRegion == y.StateProvinceRegion && x.ZipPostlCode == y.ZipPostlCode && x.CountryFK == y.CountryFK && x.Phone == y.Phone && x.FAX == y.FAX;

        }

        public int GetHashCode(Company obj)
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
