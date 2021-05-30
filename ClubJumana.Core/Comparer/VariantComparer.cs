using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class VariantComparer : IEqualityComparer<Variant>
    {
        public bool Equals(Variant x, Variant y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }



            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id && x.Sku == y.Sku && x.ProductFK == y.ProductFK && x.ColourFK == y.ColourFK &&
                   x.BarcodeFK == y.BarcodeFK && x.ProductTypeFK == y.ProductTypeFK && x.FobPrice == y.FobPrice &&
                   x.WholesaleA == y.WholesaleA && x.WholesaleB == y.WholesaleB && x.PublishRate == y.PublishRate && x.RetailPrice == y.RetailPrice &&
                   x.Width == y.Width && x.length == y.length && x.Size == y.Size && x.Note == y.Note &&
                   x.Data1 == y.Data1 && x.Data2 == y.Data2 && x.Data3 == y.Data3 && x.Data4 == y.Data4 && x.Data5 == y.Data5 &&
                   x.Data6 == y.Data6 && x.RowVersion == y.RowVersion;

        }

        public int GetHashCode(Variant obj)
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
