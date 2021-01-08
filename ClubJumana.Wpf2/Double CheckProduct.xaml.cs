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
                Search();
            }
        }

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            
            Search();
        }


        private void Search()
        {
            VariantViewModel variant;
            if (rbUPC.IsChecked == true)
            {
                variant = VariantViewModels.Where(p => p.Barcode != null).Where(p => p.Barcode.BarcodeNumber.Trim().CompareTo(txtSearch.Text.Trim()) == 0).FirstOrDefault();
                if (variant == null)
                    MessageBox.Show("Barcode Not Exist");
                else
                {
                    var ttt = _productInformationService.GiveMeVariantById(variant.Id);
                    ttt.Product.StyleNumber = ttt.Product.StyleNumber.Trim();
                    DataContext = ttt;
                }
            }
            else if (rbSKU.IsChecked==true)
            {
                variant = VariantViewModels.Where(p => p.SKU != null).Where(p => p.SKU.Trim().CompareTo(txtSearch.Text.Trim()) == 0).FirstOrDefault();
                if (variant == null)
                    MessageBox.Show("SKU Not Exist");
                else
                {
                    var ttt= _productInformationService.GiveMeVariantById(variant.Id);
                    ttt.Product.StyleNumber = ttt.Product.StyleNumber.Trim();
                    DataContext = ttt;
                }
                   
            }
            else
            {
                MessageBox.Show("Error 143");
            }
            
            txtSearch.Clear();
        }
    }
}
