using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sku_code { get; set; }
        public string StyleNum_code { get; set; }
        public ICollection<CategoriesSubCategory> CategoriesSubCategories { get; set; }
    }
}
