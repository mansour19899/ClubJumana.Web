using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ClubJumana.Core.Convertors;
using ClubJumana.DataLayer.Entities;
using JetBrains.Annotations;

namespace ClubJumana.Core.DTOs
{
    public class AddTowelInformationViewModel : INotifyPropertyChanged
    {
        public AddTowelInformationViewModel()
        {
            _company = new Company();
            _towel = new Towel();
            _product = new Product();
            _towel.ProductType = new ProductType();
            _towel.ProductType.CategoriesSubCategory = new CategoriesSubCategory();
            Towels = new List<Towel>();
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
        public float? FobPrice { get; set; }
        public float? WholesaleA { get; set; }
        public float? WholesaleB { get; set; }
        public float? RetailPrice { get; set; }
        public string Size { get; set; }
        public Product Product { get; set; }
        public Colour Colour { get; set; }
        public Barcode Barcode { get; set; }
        public ProductType ProductType { get; set; }

    }

    public class ProductInformationViewModel : INotifyPropertyChanged
    {
        public ProductInformationViewModel(Product product)
        {
            Id = product.Id;
            StyleNumber = product.StyleNumber;
            Category = product.Towels.ElementAt(0).ProductType.CategoriesSubCategory.Category.Name;
            Subcategory = product.Towels.ElementAt(0).ProductType.CategoriesSubCategory.SubCategory.Name;
            ProductTittle = product.ProductTittle;
            if (product.CountryOfOrgin != null)
                CountryOfOrigin = product.CountryOfOrgin.Name;
            if (product.Brand != null)
                Brand = product.Brand.Name;
            GSM = product.Towels.ElementAt(0).Gsm.Value.ToString();
            if (product.Company != null)
                Company = product.Company.CompanyName;
            if (product.Material != null)
                Material = product.Material.MaterialName;
            DescriabeMaterial = product.DescribeMaterial;
            List = product.Towels.ToList();
        }
        public int Id { get; set; }
        public string StyleNumber { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string ProductTittle { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Brand { get; set; }
        public string GSM { get; set; }
        public string Company { get; set; }
        public string Material { get; set; }
        public string DescriabeMaterial { get; set; }

        public List<Towel> List { get; set; }

        private CostCenter _costCenter;

        public CostCenter CostCenter
        {
            get { return _costCenter; }
            set
            {
                _costCenter = value;
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

}
