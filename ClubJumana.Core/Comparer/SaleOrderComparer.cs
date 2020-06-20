using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class SaleOrderComparer : IEqualityComparer<SaleOrder>
    {
        public bool Equals(SaleOrder x, SaleOrder y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(SaleOrder obj)
        {
            throw new NotImplementedException();
        }
    }
}
