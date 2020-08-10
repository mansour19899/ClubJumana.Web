using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ClubJumana.Core.Convertors;
using ClubJumana.DataLayer.Entities;
using JetBrains.Annotations;

namespace ClubJumana.Core.DTOs
{
    public class AddVariantInformationViewModel : INotifyPropertyChanged
    {
        public AddVariantInformationViewModel()
        {
            _company = new Company();
            _variant = new Variant();
            _product = new Product();
            _variant.ProductType = new ProductType();
            _variant.ProductType.CategoriesSubCategory = new CategoriesSubCategory();
            Variants = new List<Variant>();
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

        private Variant _variant;
        public Variant Variant
        {
            get { return _variant; }
            set { _variant = value; }
        }

        private List<Variant> _variants;

        public List<Variant> Variants
        {
            get { return _variants; }
            set
            {
                _variants = value;
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

    public class VariantViewModel: INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _sku;

        public string SKU
        {
            get { return _sku; }
            set
            {
                _sku = value; 
                OnPropertyChanged();
            }
        }
        public int? ProductFK { get; set; }
        public int? ColourFK { get; set; }
        public int? BarcodeFK { get; set; }
        public int? ProductTypeFK { get; set; }
        public decimal? FobPrice { get; set; }
        public decimal? WholesaleA { get; set; }
        public decimal? WholesaleB { get; set; }
        public decimal? RetailPrice { get; set; }
        private string _size;

        public string Size
        {
            get { return _size; }
            set
            {
                _size = value; 
                OnPropertyChanged();
            }
        }

        public Product Product { get; set; }
        private Colour _colour;

        public Colour Colour
        {
            get { return _colour; }
            set
            {
                _colour = value; 
                OnPropertyChanged();
            }
        }

        private Barcode _barcode;

        public Barcode Barcode
        {
            get { return _barcode; }
            set
            {
                _barcode = value;
                OnPropertyChanged();
            }
        }

        public ProductType ProductType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProductInformationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Variant> List { get; set; }

        public ProductInformationViewModel(Product product)
        {
            Id = product.Id;
            StyleNumber = product.StyleNumber;
            Category = product.Variants.ElementAt(0).ProductType.CategoriesSubCategory.Category.Name;
            Subcategory = product.Variants.ElementAt(0).ProductType.CategoriesSubCategory.SubCategory.Name;
            ProductTittle = product.ProductTittle;
            if (product.CountryOfOrgin != null)
                CountryOfOrigin = product.CountryOfOrgin.Name;
            if (product.Brand != null)
                Brand = product.Brand.Name;
            if (product.Variants.ElementAt(0).Data1 != null)
                GSM = product.Variants.ElementAt(0).Data1.ToString();
            if (product.Company != null)
                Company = product.Company.CompanyName;
            if (product.Material != null)
                Material = product.Material.MaterialName;
            DescriabeMaterial = product.DescribeMaterial;
            List = new ObservableCollection<Variant>(product.Variants);
            foreach (var VARIABLE in List)
            {
                if (VARIABLE.Sku == null)
                    VARIABLE.Sku = "Add SKU";
                if (VARIABLE.BarcodeFK == null)
                    VARIABLE.Barcode = new Barcode() { BarcodeNumber = "Add Barcode" };
            }
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

        private Variant _variantSelected;

        public Variant VariantSelected
        {
            get { return _variantSelected; }
            set
            {
                _variantSelected = value;
                OnPropertyChanged();
            }
        }




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

        private CostCenter _costCenterAnalyze;

        public CostCenter CostCenterAnalyze
        {
            get { return _costCenterAnalyze; }
            set
            {
                _costCenterAnalyze = value;
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


    public class SearchProductViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<VariantViewModel> _variantViewModels;

        public ObservableCollection<VariantViewModel> LvProductItemSource
        {
            get { return _variantViewModels; }
            set
            {
                _variantViewModels = value; 
                OnPropertyChanged();
            }
        }

        private ProductInformationViewModel _informationViewModel;

        public ProductInformationViewModel Info
        {
            get { return _informationViewModel; }
            set
            {
                _informationViewModel = value;
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
