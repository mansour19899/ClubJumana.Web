﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
 public   class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<CategoriesSubCategory> CategoriesSubCategories { get; set; }
    }
}
