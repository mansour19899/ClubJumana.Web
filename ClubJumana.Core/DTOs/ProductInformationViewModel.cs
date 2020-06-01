using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.DTOs
{
   public class AddTowelInformationViewModel
    {
        public AddTowelInformationViewModel()
        {
            _company=new Company();
            _towel=new Towel();
            _product=new Product();
            _towel.ProductType=new ProductType();
            _towel.ProductType.CategoriesSubCategory=new CategoriesSubCategory();
            Towels=new List<Towel>();
        }

        private Company _company;

        public Company company
        {
            get { return _company; }
            set { _company = value; }
        }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        private Towel _towel;
        public Towel Towel
        {
            get { return _towel; }
            set { _towel = value; }
        }

        public List<Towel> Towels { get; set; }
    }
}
