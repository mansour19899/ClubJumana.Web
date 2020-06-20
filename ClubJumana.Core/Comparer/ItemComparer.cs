using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class ItemComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }



            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id && x.Po_fk == y.Po_fk && x.ProductMaster_fk == y.ProductMaster_fk && x.PoQuantity == y.PoQuantity &&
                   x.AsnQuantity == y.AsnQuantity && x.GrnQuantity == y.GrnQuantity && x.PoPrice == y.PoPrice &&
                   x.AsnPrice == y.AsnPrice && x.Cost == y.Cost && x.PoItemsPrice == y.PoItemsPrice &&
                   x.AsnItemsPrice == y.AsnItemsPrice && x.Diffrent == y.Diffrent && x.Alert == y.Alert && x.Note == y.Note &&
                   x.Checked == y.Checked && x.IsDeleted == y.IsDeleted &&
                   x.RowVersion == y.RowVersion ;
        }

        public int GetHashCode(Item obj)
        {
            throw new NotImplementedException();
        }
    }
}
