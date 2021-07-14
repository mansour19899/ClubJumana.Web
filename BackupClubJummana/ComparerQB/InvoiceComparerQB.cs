using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace BackupClubJummana.ComparerQB
{
    class InvoiceComparerQB : IEqualityComparer<SaleOrder>
    {
        public bool Equals(SaleOrder x, SaleOrder y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }


            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id &&  x.LastUpdateTime == y.LastUpdateTime;
        }

        public int GetHashCode(SaleOrder obj)
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
