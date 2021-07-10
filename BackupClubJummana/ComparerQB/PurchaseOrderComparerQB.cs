﻿using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace BackupClubJummana.ComparerQB
{
    class PurchaseOrderComparerQB : IEqualityComparer<PurchaseOrder>
    {
        public bool Equals(PurchaseOrder x, PurchaseOrder y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            

            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id && x.LastUpdateTime == y.LastUpdateTime;
        }

        public int GetHashCode(PurchaseOrder obj)
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
