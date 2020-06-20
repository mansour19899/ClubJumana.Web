using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class PurchaseOrderComparer : IEqualityComparer<PurchaseOrder>
    {
        public bool Equals(PurchaseOrder x, PurchaseOrder y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(PurchaseOrder obj)
        {
            throw new NotImplementedException();
        }
    }
}
