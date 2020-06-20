using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class ProvinceComparer : IEqualityComparer<Province>
    {
        public bool Equals(Province x, Province y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }



            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id && x.Name == y.Name && x.HST == y.HST && x.GST == y.GST &&
                   x.QST == y.QST && x.Active == y.Active ;

        }

        public int GetHashCode(Province obj)
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
