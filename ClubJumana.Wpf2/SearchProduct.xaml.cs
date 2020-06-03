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
    /// Interaction logic for SearchProduct.xaml
    /// </summary>
    public partial class SearchProduct : Window
    {
        private RepositoryService _repositoryService;
        private ProductInformationService _productInformationService;
        private List<VariantViewModel> VaraintList;
        List<VariantViewModel> TempList;
        List<VariantViewModel> SelectedList;
        Product SelectedProduct;

        bool AllowScanBarcodeCon = false;
        Boolean SwitchSelectedlist = true;
        public SearchProduct()
        {
            InitializeComponent();
            _repositoryService=new RepositoryService();
            _productInformationService=new ProductInformationService();
            SelectedList=new List<VariantViewModel>();
            TempList=new List<VariantViewModel>();
        }

        private void SearchProduct_OnLoaded(object sender, RoutedEventArgs e)
        {
            cmbCategory.ItemsSource = _repositoryService.AllCategoriesList().ToList();
            cmbCompany.ItemsSource = _repositoryService.AllCompaniesList().ToList();
            cmbProductType.ItemsSource = _repositoryService.AllProductTypeList().ToList();
            cmbSubCategory.ItemsSource = _repositoryService.AllSubCategoriesList().ToList();
            
            VaraintList = _productInformationService.AllVariantList();

            cmbType.ItemsSource = new[]{
                new { Id = 1, Name = "---------" },
                new { Id = 2, Name = "Style Number" },
                new { Id = 3, Name = "Barcode" },
                new { Id = 4, Name = "SKU" },
                new { Id = 5, Name = "Tittle" }
            }.ToList();

            cmbType.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            cmbCompany.SelectedIndex = 0;
            cmbProductType.SelectedIndex = 0;
            cmbSubCategory.SelectedIndex = 0;
        }
        void SetFilter()
        {
            var t = TempList;
            if (SwitchSelectedlist == false)
                t = SelectedList;
            lvProducts.ItemsSource = t;
            if (cmbCategory.SelectedIndex > 0)
            {
                var x = Convert.ToInt16(cmbCategory.SelectedValue);
                t = t.Where(p => p.ProductType.CategoriesSubCategory.CategoryFK == x).ToList();
                lvProducts.ItemsSource = t;
            }
            if (cmbSubCategory.SelectedIndex > 0)
            {
                var xx = Convert.ToInt16(cmbSubCategory.SelectedValue);
                lvProducts.ItemsSource = t.Where(p => p.ProductType.CategoriesSubCategory.SubCategoryFK == xx);
            }
            if (cmbProductType.SelectedIndex > 0)
            {
                var xxx = Convert.ToInt16(cmbProductType.SelectedValue);
                t = t.Where(p => p.ProductType.Id == xxx).ToList();
                lvProducts.ItemsSource = t;
            }
            if (cmbCompany.SelectedIndex > 0)
            {
                var xxxx = Convert.ToInt16(cmbCompany.SelectedValue);
                t = t.Where(p => p.Product.CompanyFK == xxxx).ToList();
                lvProducts.ItemsSource = t;
            }
            if (t != null)
                lblCountResult.Content = "Results :  " + t.Count();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string x = cmbType.SelectedValue.ToString();
                if (x.CompareTo("1") == 0)
                    lvProducts.ItemsSource = VaraintList;

                switch (x)
                {
                    case "1":
                        lvProducts.ItemsSource = VaraintList;
                        break;
                    case "2":
                        lvProducts.ItemsSource = VaraintList
                            .Where(p => p.Product.StyleNumber.Trim().Contains(txtSearch.Text.Trim())).ToList();
                        break;
                    case "3":
                        lvProducts.ItemsSource = VaraintList.Where(p=>p.Barcode.BarcodeNumber.Trim().CompareTo(txtSearch.Text.Trim())==0).ToList();
                        txtSearch.Clear();
                        break;
                    case "4":
                        //lvProducts.ItemsSource = VaraintList.Where(p => p.Sku.Trim().Contains(txtSearch.Text.Trim())).ToList();
                        break;
                    default:
                        MessageBox.Show("Hi");
                        break;
                }

                TempList = lvProducts.ItemsSource as List<VariantViewModel>;
                lblCountResult.Content = "Results :  " + TempList.Count();
                SwitchSelectedlist = true;
                SetFilter();
                BtnShowSelectedlist.Content = "Show Selected List";
                btnAddToList.Content = "Add to List";
            }
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = Convert.ToInt16(cmbType.SelectedValue);
            AllowScanBarcodeCon = false;
            switch (x)
            {
                case 1:
                    TempList.Clear();
                    TempList = VaraintList;
                    lvProducts.ItemsSource = TempList;
                    txtSearch.Text = "";
                    lblCountResult.Content = TempList.Count();
                    break;
                case 2:
                    //lvProducts.ItemsSource = connect.GiveListProductsWithSytyle(txtSearch.Text);
                    break;
                case 3:
                    txtSearch.Clear();
                    AllowScanBarcodeCon = true;
                    break;
                case 4:
                    MessageBox.Show("salam");
                    break;
                default:
                    MessageBox.Show("Hi");
                    break;
            }
            txtSearch.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SwitchSelectedlist)
            {
                SwitchSelectedlist = false;
                lvProducts.ItemsSource = SelectedList.ToList();
                BtnShowSelectedlist.Content = "Show Main List";
                btnAddToList.Content = "Remove to List";
                SetFilter();
            }
            else
            {
                SwitchSelectedlist = true;
                lvProducts.ItemsSource = TempList.ToList();
                SetFilter();
                BtnShowSelectedlist.Content = "Show Selected List";
                btnAddToList.Content = "Add to List";

            }
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void Label_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            cmbSubCategory.SelectedIndex = 0;
        }

        private void cmbSubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void Label_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            cmbProductType.SelectedIndex = 0;
        }

        private void cmbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void cmbCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void Label_MouseDoubleClick_3(object sender, MouseButtonEventArgs e)
        {
            cmbCompany.SelectedIndex = 0;
        }

        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            var itemSelected = lvProducts.SelectedItems;
            if (SwitchSelectedlist)
            {
                foreach (var item in itemSelected)
                {
                    if (SelectedList.Contains(item) == false)
                        SelectedList.Add(item as VariantViewModel);
                }
            }
            else
            {
                foreach (var item in itemSelected)
                {
                    SelectedList.Remove(item as VariantViewModel);
                }
                lvProducts.ItemsSource = SelectedList.ToList();
            }

            lvProducts.SelectedIndex = -1;
            lblCountSelectedList.Content = "IsSelected : " + SelectedList.Count();
        }

        private void lvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
