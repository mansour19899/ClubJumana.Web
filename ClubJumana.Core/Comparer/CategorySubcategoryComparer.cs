using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Comparer
{
    class CategorySubcategoryComparer : IEqualityComparer<CategoriesSubCategory>
    {
        public bool Equals(CategoriesSubCategory x, CategoriesSubCategory y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }


            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Id == y.Id && x.CategoryFK == y.CategoryFK && x.SubCategoryFK == y.SubCategoryFK;
        }

        public int GetHashCode(CategoriesSubCategory obj)
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
