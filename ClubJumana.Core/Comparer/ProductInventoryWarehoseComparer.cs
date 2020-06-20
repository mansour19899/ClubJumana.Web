using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class ProductInventoryWarehoseComparer : IEqualityComparer<ProductInventoryWarehouse>
    {
        public bool Equals(ProductInventoryWarehouse x, ProductInventoryWarehouse y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(ProductInventoryWarehouse obj)
        {
            throw new NotImplementedException();
        }
    }
}
