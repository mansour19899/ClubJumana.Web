using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace BackupClubJummana.ComparerQB
{
    class CustomerComparerQB : IEqualityComparer<Customer>
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


            return x.Id == y.Id ;
        }

        public int GetHashCode(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
