using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ClubJumana.DataLayer.Entities;
using JetBrains.Annotations;

namespace ClubJumana.Core.DTOs
{
   public class AddTowelInformationViewModel : INotifyPropertyChanged
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
            set
            {
                _company = value;
                OnPropertyChanged();
            }
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
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
