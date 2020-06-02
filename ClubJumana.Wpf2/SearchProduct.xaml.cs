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
        List<Product> TempList;
        List<Product> SelectedList;


        Boolean SwitchSelectedlist = true;
        public SearchProduct()
        {
            InitializeComponent();
            _repositoryService=new RepositoryService();
            SelectedList=new List<Product>();
        }

        private void SearchProduct_OnLoaded(object sender, RoutedEventArgs e)
        {
            cmbCategory.ItemsSource = _repositoryService.AllCategoriesList().ToList();
            cmbCompany.ItemsSource = _repositoryService.AllCompaniesList().ToList();
            cmbProductType.ItemsSource = _repositoryService.AllProductTypeList().ToList();
            cmbSubCategory.ItemsSource = _repositoryService.AllSubCategoriesList().ToList();

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
                t = t.Where(p => p.ProductType.CategoriesSubCategory.Category_Id_fk == x).ToList();
                lvProducts.ItemsSource = t;
            }
            if (cmbSubCategory.SelectedIndex > 0)
            {
                var xx = Convert.ToInt16(cmbSubCategory.SelectedValue);
                lvProducts.ItemsSource = t.Where(p => p.ProductType.CategoriesSubCategory.SubCategory_Id_fk == xx);
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
                t = t.Where(p => p.Company_Id_fk == xxxx).ToList();
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

        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Label_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void cmbSubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Label_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {

        }

        private void cmbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Label_MouseDoubleClick_3(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
