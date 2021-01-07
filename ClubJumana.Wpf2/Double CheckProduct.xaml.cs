using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Services;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Wpf2
{
    /// <summary>
    /// Interaction logic for Double_CheckProduct.xaml
    /// </summary>
    public partial class Double_CheckProduct : Window
    {
        public List<VariantViewModel> VariantViewModels;
        private ProductInformationService _productInformationService;
        public Double_CheckProduct()
        {
            InitializeComponent();
            _productInformationService=new ProductInformationService();
        }

        private void Double_CheckProduct_OnLoaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void TxtSearch_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var variant = VariantViewModels.Where(p => p.Barcode != null).Where(p => p.Barcode.BarcodeNumber.Trim().CompareTo(txtSearch.Text.Trim()) == 0).FirstOrDefault();
                if (variant == null)
                    MessageBox.Show("Barcode Not Exist");
                else
                    DataContext = _productInformationService.GiveMeVariantById(variant.Id);
                txtSearch.Clear();
            }
        }
    }
}
