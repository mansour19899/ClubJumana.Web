﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class CategoriesSubCategory
    {
        public int Id { get; set; }
        public int CategoryFK { get; set; }
        public int SubCategoryFK { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }

        public ICollection<ProductType> ProductTypes { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
