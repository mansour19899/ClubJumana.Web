﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategorysubcategoreisFK { get; set; }
        public CategoriesSubCategory CategoriesSubCategory { get; set; }
        public ICollection<Variant> Variants { get; set; }
    }
}
