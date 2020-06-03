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

        private List<Towel> _towels;

        public List<Towel> Towels
        {
            get { return _towels; }
            set
            {
                _towels = value;
                OnPropertyChanged();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   public class VariantViewModel
   {
       public int Id { get; set; }
       public string Sku { get; set; }
       public int? ProductFK { get; set; }
       public int? ColourFK { get; set; }
       public int? BarcodeFK { get; set; }
       public int? ProductTypeFK { get; set; }
       public float? Price { get; set; }
       public string Size { get; set; }
       public Product Product { get; set; }
       public Colour Colour { get; set; }
       public Barcode Barcode { get; set; }
       public ProductType ProductType { get; set; }

    }

}
