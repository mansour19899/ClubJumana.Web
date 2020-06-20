using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class ColourComparer : IEqualityComparer<Colour>
    {
        public bool Equals(Colour x, Colour y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }


            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Id == y.Id && x.Name == y.Name && x.Code == y.Code && x.PantoneNumber == y.PantoneNumber;
        }

        public int GetHashCode(Colour obj)
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
