using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }



            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id && x.StyleNumber == y.StyleNumber && x.ProductTittle == y.ProductTittle && x.BrandFK == y.BrandFK &&
                   x.MaterialFK == y.MaterialFK && x.CompanyFK == y.CompanyFK && x.CountryOfOrginFK == y.CountryOfOrginFK &&
                   x.DescribeMaterial == y.DescribeMaterial && x.RowVersion == y.RowVersion;
        }

        public int GetHashCode(Product obj)
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
