using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class BarcodeComparer : IEqualityComparer<Barcode>
    {
        public bool Equals(Barcode x, Barcode y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Id == y.Id && x.BarcodeNumber == y.BarcodeNumber && x.Active == y.Active;
        }

        public int GetHashCode(Barcode obj)
        {
            //If obj is null then return 0
            if (obj == null)
            {
                return 0;
            }
            //Get the ID hash code value
            int IDHashCode = obj.Id.GetHashCode();
            //Get the Name HashCode Value
            //int NameHashCode = obj.BarcodeNumber == null ? 0 : obj.BarcodeNumber.GetHashCode();
            //return IDHashCode ^ NameHashCode;
            return IDHashCode ;
        }
    }
}
