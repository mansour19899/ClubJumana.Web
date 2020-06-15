using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClubJumana.Core.Convertors;
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
        private ProductInformationViewModel InfoProduct;
        private List<Country> countriesList;
        private CostCenter cost;
        private string Path;
        private int SelectedIndexVariant=0;
        private int ImageSelected = 0;
        private int countImageVariant = 0;

        bool AllowScanBarcodeCon = false;
        Boolean SwitchSelectedlist = true;
        public SearchProduct()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _productInformationService = new ProductInformationService();
            SelectedList = new List<VariantViewModel>();
            TempList = new List<VariantViewModel>();
            Path = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\VariantsImage\\";
        }

        private void SearchProduct_OnLoaded(object sender, RoutedEventArgs e)
        {
            cmbCategory.ItemsSource = _repositoryService.AllCategoriesList().ToList();
            cmbCompany.ItemsSource = _repositoryService.AllCompaniesList().ToList();
            cmbProductType.ItemsSource = _repositoryService.AllProductTypeList().ToList();
            cmbSubCategory.ItemsSource = _repositoryService.AllSubCategoriesList().ToList();
            cmbEditVariantColor.ItemsSource = _repositoryService.AllColourList().ToList();

            countriesList= _repositoryService.AllCountriesList().ToList();
            cmbCountry.ItemsSource = countriesList;

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
                        lvProducts.ItemsSource = VaraintList.Where(p => p.Barcode.BarcodeNumber.Trim().CompareTo(txtSearch.Text.Trim()) == 0).ToList();
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
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (VariantViewModel)lvProducts.ItemContainerGenerator.ItemFromContainer(dep);

            ShowProductInformation(wer.Product.Id);
        }

        private void ShowProductInformation(int Id)
        {
            SelectedProduct = _productInformationService.GiveMeProductWithId(Id);
            InfoProduct = new ProductInformationViewModel(SelectedProduct);
            //lvVariant.ItemsSource = InfoProduct.List;
            this.DataContext = InfoProduct;
           ShowImageVariant(0);
           btnNextImageVariant.Visibility = Visibility.Hidden;
           btnPerviosImageVariant.Visibility = Visibility.Hidden;
            GrSearch.Visibility = Visibility.Hidden;
            GrdInformationProduct.Visibility = Visibility.Visible;
        }

        private void AddSku(object sender, RoutedEventArgs e)
        {

            Button b = sender as Button;
            Variant variant = b.CommandParameter as Variant;

            if (variant.ColourFK == 0)
            {
                MessageBox.Show("First assign Color for Variant");
            }
            else
            {

                if (variant.Sku.CompareTo("Add SKU") == 0)
                {
                    string messageBoxText = "Do you want to add Sku Number?";
                    string caption = "Add Sku";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Question;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            var tet = _productInformationService.GiveMeSku(variant.ProductType.CategoriesSubCategory.Category.Sku_code,
                                variant.ProductType.CategoriesSubCategory.SubCategory.Code, variant.ProductType.Code, variant.Colour.Code);
                            _productInformationService.AddSku(variant.Id, tet);
                            MessageBox.Show(tet);
                            break;
                        case MessageBoxResult.No:

                            break;

                    }
                }
                else
                {
                    Clipboard.SetText(variant.Sku);
                    MessageBox.Show(variant.Sku + " Copied");
                }
            }







        }

        private void AddBarcode(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Variant variant = b.CommandParameter as Variant;

            if (variant.Barcode.BarcodeNumber.CompareTo("Add Barcode") == 0)
            {
                string messageBoxText = "Do you want to add Barcode Number?";
                string caption = "Add Barcode";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        _productInformationService.AddBarcode(variant.Id);
                        MessageBox.Show("Barcode Added" + variant.Id);
                        break;
                    case MessageBoxResult.No:

                        break;

                }
            }
            else
            {
                Clipboard.SetText(variant.Barcode.BarcodeNumber);
                MessageBox.Show(variant.Barcode.BarcodeNumber + " Copied");
            }
            
        }

        private void EditVariant(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Variant variantSelected = b.CommandParameter as Variant;
            InfoProduct.VariantSelected = variantSelected;
            var ttt = variantSelected;
            GrdEditProduct.Visibility = Visibility.Visible;
        }

        private void BtnInformationPrice(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Variant variantSelected = b.CommandParameter as Variant;
            cost = new CostCenter();

            cost.FobPrice = Convert.ToDecimal(variantSelected.FobPrice);
            cost.WholeSaleA = variantSelected.WholesaleA.ToString();
            cost.WholeSaleB = variantSelected.WholesaleB.ToString();
            cost.RetailPrice = variantSelected.RetailPrice.ToString();
            var country = _repositoryService.GiveMeCountryByID(38);
            if (country.ExChangeRate == null)
                cost.ExchangeRate = -1;
            else
                cost.ExchangeRate = country.ExChangeRate.Value;
            if (variantSelected.Product.CountryOfOrgin.Duty != null)
                cost.Duty = variantSelected.Product.CountryOfOrgin.Duty.Value;
            else
                cost.Duty = -1;
            InfoProduct.CostCenter = cost;
            GrdCostCenter.Visibility = Visibility.Visible;
        }



        #region CostCenter

        private void txtMainPrice_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
            ControlKeyDown(e.Key, txtMainPrice);
        }
        private void txtCostRate_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
            ControlKeyDown(e.Key, txtCostRate);

        }
        private void TxtExChangeRate_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
            ControlKeyDown(e.Key, txtExChangeRate);
        }


        private void TxtMainPrice_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtMainPrice.SelectAll();
        }

        private void TxtMainPrice_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtMainPrice.SelectAll();
        }

        private void TxtCostRate_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtCostRate.SelectAll();
        }

        private void TxtCostRate_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtCostRate.SelectAll();
        }

        private void TxtExChangeRate_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtExChangeRate.SelectAll();
        }

        private void TxtExChangeRate_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtExChangeRate.SelectAll();
        }

        #endregion

        private void ControlKeyDown(Key keyPress, TextBox textBox)
        {
            if (keyPress == Key.Enter || keyPress == Key.Tab)
            {
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            }
            else if (keyPress == Key.Escape)
            {
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
            else
            {

            }
        }

        public bool SetNumeric(object sender, KeyEventArgs e)
        {
            bool result = false;

            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal) || (e.Key == Key.Tab))
            { result = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.OemPeriod))
            { result = false; }
            else
            { result = true; }

            if (e.Key == Key.OemPeriod && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result = true;
            }
            if (e.Key == Key.Decimal && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result = true;
            }
            var hi = sender as TextBox;
            var hii = hi.Text;
            var count = hi.Text.Split('.');

            if (count.Count() > 1)
            {
                if (count[1].Count() > 3 && e.Key != Key.Tab)
                {
                    result = true;
                }
            }
            return result;
        }


        private void BtnShowMoreInformation_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            if (GrdCostShowMore.Visibility == Visibility.Visible)
            {
                GrdCostShowMore.Visibility = Visibility.Collapsed;
            }
            else
            {
                GrdCostShowMore.Visibility = Visibility.Visible;
                ScrollViewerCostCenter.ScrollToVerticalOffset(500);
            }

        }

        private void BtnCloseCostCenter_OnClick(object sender, RoutedEventArgs e)
        {
            GrdCostCenter.Visibility = Visibility.Hidden;
            GrdCostShowMore.Visibility = Visibility.Collapsed;
        }

        private void CmbCountry_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cost != null)
            {
                var t = countriesList.FirstOrDefault(p => p.Id == cmbCountry.SelectedIndex + 1);
                if (t != null)
                    cost.Country = t;
            }
        }


        private void BtnLogo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (GrdInformationProduct.Visibility == Visibility.Visible)
            {
                GrdInformationProduct.Visibility = Visibility.Hidden;
                GrSearch.Visibility = Visibility.Visible;
            }
            else
            {
                GrdInformationProduct.Visibility = Visibility.Visible;
                GrSearch.Visibility = Visibility.Hidden;
            }
        }


        private void BtnUpdateVariant_OnClick(object sender, RoutedEventArgs e)
        {
            _productInformationService.UpdateVariant(InfoProduct.VariantSelected);
            InfoProduct.List.FirstOrDefault(p => p.Id == InfoProduct.VariantSelected.Id).Colour.Name =
                cmbEditVariantColor.Text;
            //lvVariant.ItemsSource = null;
            //lvVariant.ItemsSource = InfoProduct.List;
            GrdEditProduct.Visibility = Visibility.Hidden;

        }

        private void BtnCloseGrdEditVariant_OnClick(object sender, RoutedEventArgs e)
        {
            GrdEditProduct.Visibility = Visibility.Hidden;
        }

        private void BtnAddPicture_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (lvVariant.SelectedItems.Count == 0)
            {
                MessageBox.Show("First Choose Variant");
            }
            else
            {
                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".png";
                dlg.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    var Path = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\VariantsImage\\";
                    DateTime date = DateTime.Now;
                    string ImageName = date.Year.ToString() + date.Month.ToString().NumtoStr(2) + date.Day.ToString().NumtoStr(2) + date.Hour.ToString().NumtoStr(2) + date.Minute.ToString().NumtoStr(2) + date.Second.ToString().NumtoStr(2);
                    var ext = System.IO.Path.GetExtension(dlg.FileName);
                    string FullNameImage = ImageName + ext;
                    var DestentionPath = Path + FullNameImage;
                    File.Copy(dlg.FileName, DestentionPath);
                    var tt = lvVariant.SelectedItems[0] as Variant;
                    _productInformationService.AddImageVariant(tt.Id, FullNameImage);
                    imgVariant.Background = new ImageBrush(new BitmapImage(new Uri(DestentionPath)));



                    // Open document 
                    //string filename = dlg.FileName;
                    //ManageFileFolder ff = new ManageFileFolder();
                    //string pic = ff.copy(filename);
                    //UploadedPicturs.Add(pic);
                    //imgMain.Source = new BitmapImage(new Uri(pic));
                    //var yt = pic.Split('\\').Last();
                    //if (connect.AddImage(SelectedProduct.Id, yt))
                    //    UploadPictures();
                    //MessageBox.Show("pic added");

                }
            }


        }



        private void LvVariant_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            SelectedIndexVariant= lvVariant.SelectedIndex;
            
            if (SelectedIndexVariant != -1)
            {
                countImageVariant = InfoProduct.List[SelectedIndexVariant].Images.Count;
                ShowImageVariant(SelectedIndexVariant);
                ImageSelected = 0;
                btnNextImageVariant.Visibility = Visibility.Visible;
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
                btnNextFullImage.Visibility = Visibility.Visible;
                btnPreviousFullImage.Visibility = Visibility.Hidden;

            }

            else
            {
                btnNextImageVariant.Visibility = Visibility.Hidden;
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
                btnNextFullImage.Visibility = Visibility.Hidden;
                btnPreviousFullImage.Visibility = Visibility.Hidden;
            }

        }


        private void ShowImageVariant(int index,int imgSelect=0)
        {
            var images = InfoProduct.List[index].Images;
            if (images.Count == 0)
            {
                imgVariant.Background = new ImageBrush(new BitmapImage(new Uri(Path + "NoImage.JPG")));
                btnNextImageVariant.Visibility = Visibility.Hidden;
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
            }
            else
            {
                var imageName = InfoProduct.List[index].Images.ElementAt(imgSelect).ImageName;
                imgVariant.Background = new ImageBrush(new BitmapImage(new Uri(Path + imageName)));
                imgFullImage.Background = new ImageBrush(new BitmapImage(new Uri(Path + imageName)));
            }
        }

        private void BtnNextImageVariant_OnClick(object sender, RoutedEventArgs e)
        {
            ImageSelected++;
            ShowImageVariant(SelectedIndexVariant, ImageSelected);
            if (ImageSelected == countImageVariant-1)
            {

                btnNextImageVariant.Visibility = Visibility.Hidden;
                btnNextFullImage.Visibility = Visibility.Hidden;
            }
            if (ImageSelected == 1)
            {
                btnPerviosImageVariant.Visibility = Visibility.Visible;
                btnPreviousFullImage.Visibility = Visibility.Visible;
            }
        }


        private void BtnPreviousImageVariant_OnClick(object sender, RoutedEventArgs e)
        {
            ImageSelected--;
            ShowImageVariant(SelectedIndexVariant, ImageSelected);
            if (ImageSelected == countImageVariant - 2)
            {

                btnNextImageVariant.Visibility = Visibility.Visible;
                btnNextFullImage.Visibility = Visibility.Visible;
            }

            if (ImageSelected == 0)
            {
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
                btnPreviousFullImage.Visibility = Visibility.Hidden;
            }
        }

        private void ImgVariant_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            GrdShowImage.Visibility = Visibility.Visible;
        }

        private void ImgFullImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            GrdShowImage.Visibility = Visibility.Hidden;
        }
    }
}
