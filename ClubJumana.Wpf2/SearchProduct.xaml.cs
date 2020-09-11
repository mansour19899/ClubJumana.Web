using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;

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
        private ObservableCollection<VariantViewModel> ListForLvProduct;
        List<VariantViewModel> TempList;
        List<VariantViewModel> SelectedList;
        Product SelectedProduct;
        private ProductInformationViewModel InfoProduct;
        private List<Country> countriesList;
        private List<ProductType> ProductTypeList;
        private CostCenter cost;
        private CostCenter costAnalyze;
        private string Path;
        private int SelectedIndexVariant = 0;
        private int ImageSelected = 0;
        private int countImageVariant = 0;
        private string ErrorConection = "";
        Boolean SwitchSelectedlist = true;
        private bool IsConnectToServer = true;
        private int IndexOfLvProduct = 0;
        private SnackbarMessageQueue myMessageQueue;
        private string FobPriceCostCenter = "";
        private bool IsChangeStarVariant = false;

        private SearchProductViewModel viewModel;
        public SearchProduct(bool IsConnetedToServer = false)
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _productInformationService = new ProductInformationService();
            SelectedList = new List<VariantViewModel>();
            TempList = new List<VariantViewModel>();
            Path = AppDomain.CurrentDomain.BaseDirectory + "Images\\VariantsImage\\";

            viewModel = new SearchProductViewModel();
            ListForLvProduct = new ObservableCollection<VariantViewModel>();
        }

        private void SearchProduct_OnLoaded(object sender, RoutedEventArgs e)
        {
            // ShowErrorMassegeToConectionInternet();

            cmbCategory.ItemsSource = _repositoryService.AllCategoriesList().ToList();
            cmbCompany.ItemsSource = _repositoryService.AllCompaniesList().ToList();
            ProductTypeList = _repositoryService.AllProductTypeList().ToList();
            cmbProductType.ItemsSource = ProductTypeList;
            cmbSubCategory.ItemsSource = _repositoryService.AllSubCategoriesList().ToList();
            cmbEditVariantColor.ItemsSource = _repositoryService.AllColourList().ToList();
            countriesList = _repositoryService.AllCountriesList().ToList();
            cmbCountry.ItemsSource = countriesList;

            VaraintList = _productInformationService.AllVariantList();

            cmbType.ItemsSource = new[]{
                new { Id = 1, Name = "---------" },
                new { Id = 2, Name = "Style Number" },
                new { Id = 3, Name = "Barcode" },
                new { Id = 4, Name = "SKU" },
                new { Id = 5, Name = "Tittle" },
                new { Id = 6, Name = "Favorite List" }
            }.ToList();

            cmbType.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            cmbCompany.SelectedIndex = 0;
            cmbProductType.SelectedIndex = 0;
            cmbSubCategory.SelectedIndex = 0;
            //Test(AppDomain.CurrentDomain.BaseDirectory + "Images\\VariantsImage\\" + "Excel Formula.txt", "/VariantsImage");
            //_repositoryService.DownloadFileFromFTP(AppDomain.CurrentDomain.BaseDirectory + "Images\\VariantsImage\\" + "Excel Formula.txt", "/VariantsImage");
            //_repositoryService.UploadFileToFTP(AppDomain.CurrentDomain.BaseDirectory + "Images\\VariantsImage\\"+ "Excel Formula.txt", "/VariantsImage/");

            viewModel.Info = InfoProduct;
            ListForLvProduct = new ObservableCollection<VariantViewModel>(VaraintList);
            viewModel.LvProductItemSource = ListForLvProduct;
            this.DataContext = viewModel;

            myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(4000));
            SnackbarResult.MessageQueue = myMessageQueue;
            this.Topmost = false;
        }

        private void CheckConectionToServe()
        {
            if (CheckInternetConnection.IsConnectedToServer() == true)
            {
                if (!Core.Consts.Consts.OnlineModeOnly)
                    _repositoryService.UpdateLocalDb();
                IsConnectToServer = true;
                ErrorConection = "";
                //txtMagicStyle.Foreground = new SolidColorBrush(Colors.Navy);
            }
            else
            {
                IsConnectToServer = false;
                if (CheckInternetConnection.IsConnectedToInternet() == true)
                    ErrorConection = "Mode is Offline.Please Check Server";
                else
                    ErrorConection = "Mode is Offline.Check your internet connection";
            }

            MessageBox.Show(ErrorConection);
        }

        private void ShowErrorMassegeToConectionInternet()
        {
            if (ErrorConection != "")
            {
                txtMagicStyle.Foreground = new SolidColorBrush(Color.FromRgb(191, 155, 48));
                MessageBox.Show(ErrorConection);
            }

        }

        private void TxtMagicStyle_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //CheckConectionToServe();
            //ShowErrorMassegeToConectionInternet();
            //myMessageQueue.Enqueue("Club Jummana");
            if (cmbType.SelectedIndex == 5)
                UpdateStarList();
            else
                cmbType.SelectedIndex = 5;

        }

        void SetFilter()
        {
            var t = TempList;
            if (SwitchSelectedlist == false)
                t = SelectedList;
            ListForLvProduct = new ObservableCollection<VariantViewModel>(t);
            viewModel.LvProductItemSource = ListForLvProduct;
            if (cmbCategory.SelectedIndex > 0)
            {
                var x = Convert.ToInt16(cmbCategory.SelectedValue);
                t = t.Where(p => p.ProductType.CategoriesSubCategory.CategoryFK == x).ToList();
                ListForLvProduct = new ObservableCollection<VariantViewModel>(t);
                viewModel.LvProductItemSource = ListForLvProduct;
            }
            if (cmbSubCategory.SelectedIndex > 0)
            {
                var xx = Convert.ToInt16(cmbSubCategory.SelectedValue);
                ListForLvProduct = new ObservableCollection<VariantViewModel>(t);
                viewModel.LvProductItemSource = ListForLvProduct;
            }
            if (cmbProductType.SelectedIndex > 0)
            {
                var xxx = Convert.ToInt16(cmbProductType.SelectedValue);
                t = t.Where(p => p.ProductType.Id == xxx).ToList();
                ListForLvProduct = new ObservableCollection<VariantViewModel>(t);
                viewModel.LvProductItemSource = ListForLvProduct;
            }
            if (cmbCompany.SelectedIndex > 0)
            {
                var xxxx = Convert.ToInt16(cmbCompany.SelectedValue);
                t = t.Where(p => p.Product.CompanyFK == xxxx).ToList();
                ListForLvProduct = new ObservableCollection<VariantViewModel>(t);
                viewModel.LvProductItemSource = ListForLvProduct;
            }
            if (t != null)
                lblCountResult.Content = "Results :  " + t.Count();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                int x = cmbType.SelectedIndex;

                switch (x)
                {
                    case 0:
                        if (VaraintList.Count == 0)
                            VaraintList = _productInformationService.AllVariantList();
                        ListForLvProduct = new ObservableCollection<VariantViewModel>(VaraintList);
                        viewModel.LvProductItemSource = ListForLvProduct;
                        break;
                    case 1:

                        var t = VaraintList
                            .Where(p => p.Product.StyleNumber.Trim().Contains(txtSearch.Text.Trim())).ToList();
                        ListForLvProduct = new ObservableCollection<VariantViewModel>(t);
                        viewModel.LvProductItemSource = ListForLvProduct;
                        break;
                    case 2:
                        var variant = VaraintList.Where(p => p.Barcode != null).Where(p => p.Barcode.BarcodeNumber.Trim().CompareTo(txtSearch.Text.Trim()) == 0).FirstOrDefault();
                        if (variant == null)
                            myMessageQueue.Enqueue("Barcode Not Exist");
                        else
                            ShowProductInformation(variant.ProductFK.Value);
                        txtSearch.Clear();
                        break;
                    case 3:

                        var tt = VaraintList.Where(p => p.SKU != null)
                            .Where(p => p.SKU.Trim().Contains(txtSearch.Text.Trim())).ToList();
                        ListForLvProduct = new ObservableCollection<VariantViewModel>(tt);
                        viewModel.LvProductItemSource = ListForLvProduct;

                        break;
                    default:
                        MessageBox.Show("Hi");
                        break;
                }

                TempList = ListForLvProduct.ToList();
                lblCountResult.Content = "Results :  " + ListForLvProduct.Count();
                SwitchSelectedlist = true;
                SetFilter();
                BtnShowSelectedlist.Content = "Show Selected List";
                btnAddToList.Content = "Add to List";
            }
        }

        private void TxtBarcodeMode_OnKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                var variant = VaraintList.Where(p => p.Barcode != null).Where(p => p.Barcode.BarcodeNumber.Trim().CompareTo(txtBarcodeMode.Text.Trim()) == 0).FirstOrDefault();
                if (variant == null)
                    myMessageQueue.Enqueue("Barcode Not Exist");
                else
                    ShowProductInformation(variant.ProductFK.Value);
                txtBarcodeMode.Clear();
            }
        }

        private void BtnClubJummanaLogo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtBarcodeMode.Focus();
            myMessageQueue.Enqueue("Barcode Mode Activated"); ;
        }
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = Convert.ToInt16(cmbType.SelectedValue);

            switch (x)
            {
                case 1:
                    TempList.Clear();
                    //if (VaraintList.Count == 0)
                    VaraintList = _productInformationService.AllVariantList();
                    TempList = VaraintList;
                    ListForLvProduct = new ObservableCollection<VariantViewModel>(TempList);
                    viewModel.LvProductItemSource = ListForLvProduct;
                    txtSearch.Text = "";
                    lblCountResult.Content = TempList.Count();
                    break;
                case 2:
                    txtSearch.Clear();
                    break;
                case 3:
                    txtSearch.Clear();
                    break;
                case 4:
                    txtSearch.Clear();
                    break;
                case 6:
                    UpdateStarList();
                    break;
                default:
                    MessageBox.Show("Error 121");
                    break;
            }
            txtSearch.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SwitchSelectedlist)
            {
                SwitchSelectedlist = false;
                ListForLvProduct = new ObservableCollection<VariantViewModel>(SelectedList.ToList());
                viewModel.LvProductItemSource = ListForLvProduct;
                BtnShowSelectedlist.Content = "Show Main List";
                btnAddToList.Content = "Remove to List";
                SetFilter();
            }
            else
            {
                SwitchSelectedlist = true;
                ListForLvProduct = new ObservableCollection<VariantViewModel>(TempList);
                viewModel.LvProductItemSource = ListForLvProduct;
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

                ListForLvProduct = new ObservableCollection<VariantViewModel>(SelectedList.ToList());
                viewModel.LvProductItemSource = ListForLvProduct;
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
            SelecIndexOfClick(wer.Id);
        }

        private async Task SelecIndexOfClick(int Id)
        {
            await Task.Run(() =>
            {
                IndexOfLvProduct = ListForLvProduct.ToList().FindIndex(p => p.Id == Id);
                this.Dispatcher.Invoke(() => lvProducts.SelectedIndex = IndexOfLvProduct);
            });

        }


        private void ShowProductInformation(int Id)
        {
            SelectedProduct = _productInformationService.GiveMeProductWithId(Id);
            InfoProduct = new ProductInformationViewModel(SelectedProduct);
            viewModel.Info = InfoProduct;
            ShowImageVariant(0);
            cmbEditVariantProductType.ItemsSource = ProductTypeList.Where(p => p.CategorysubcategoreisFK == InfoProduct.List.ElementAt(0).ProductType.CategorysubcategoreisFK);
            txtRemarksVariant.Text = "";
            grdNoteVariant.Visibility = Visibility.Hidden;
            btnNextImageVariant.Visibility = Visibility.Hidden;
            btnPerviosImageVariant.Visibility = Visibility.Hidden;
            btnNextFullImage.Visibility = Visibility.Hidden;
            btnPreviousFullImage.Visibility = Visibility.Hidden;
            GrSearch.Visibility = Visibility.Hidden;
            GrdInformationProduct.Visibility = Visibility.Visible;
            ImageSelected = 0;
        }

        private void AddSku(object sender, RoutedEventArgs e)
        {

            Button b = sender as Button;
            Variant variant = b.CommandParameter as Variant;

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
                        if (variant.ProductType.CategoriesSubCategory == null)
                            variant = _productInformationService.GiveMeVariantById(variant.Id);
                        var tet = _productInformationService.GiveMeSku(variant.ProductType.CategoriesSubCategory.Category.Sku_code,
                            variant.ProductType.CategoriesSubCategory.SubCategory.Code, variant.ProductType.Code);
                        _productInformationService.AddSku(variant.Id, tet);
                        InfoProduct.List.FirstOrDefault(p => p.Id == variant.Id).Sku = tet;
                        lvVariant.Items.Refresh();
                        var ee = ListForLvProduct.FirstOrDefault(p => p.Id == variant.Id);
                        if (ee != null)
                            ee.SKU = tet;
                        break;
                    case MessageBoxResult.No:

                        break;

                }
            }
            else
            {
                Clipboard.SetText(variant.Sku);
                myMessageQueue.Enqueue("SKU Copied");
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
                        var varaintt = _productInformationService.GiveMeVariantById(variant.Id);
                        if (varaintt.Barcode != null)
                        {
                            InfoProduct.List.FirstOrDefault(p => p.Id == variant.Id).Barcode = varaintt.Barcode;
                            lvVariant.Items.Refresh();
                            var ee = ListForLvProduct.FirstOrDefault(p => p.Id == variant.Id);
                            if (ee != null)
                            {
                                ee.Barcode = varaintt.Barcode;
                            }
                        }
                        break;
                    case MessageBoxResult.No:

                        break;

                }
            }
            else
            {
                Clipboard.SetText(variant.Barcode.BarcodeNumber);
                myMessageQueue.Enqueue("Barcode copied");
                // MessageBox.Show(variant.Barcode.BarcodeNumber + " Copied");
            }

        }

        private void EditVariant(object sender, RoutedEventArgs e)
        {
            if (IsConnectToServer)
            {
                Button b = sender as Button;
                Variant variantSelected = b.CommandParameter as Variant;
                ProductTypeForAddVariant.Visibility = Visibility.Collapsed;
                InfoProduct.VariantSelected = InfoProduct.List.FirstOrDefault(p => p.Id == variantSelected.Id);
                InfoProduct.VariantSelected.ColourFK = InfoProduct.VariantSelected.Colour.Id;
                cmbEditVariantColor.SelectedValue = InfoProduct.VariantSelected.Colour.Id;
                HideAddColorPart();
                GrdEditVariant.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Mode Is Offline");
            }
        }

        private void BtnInformationPrice(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Variant variantSelected = b.CommandParameter as Variant;
            cost = new CostCenter();
            costAnalyze = new CostCenter();

            if (variantSelected.Product.CountryOfOrginFK == null)
            {
                MessageBox.Show("Please Set Country Of Orgin");
            }
            else if (variantSelected.FobPrice == null)
            {
                MessageBox.Show("Please Set F.O.B Price");
            }
            else
            {
                var country = _repositoryService.GiveMeCountryByID(38);
                cost.Country = country;
                if (country.ExChangeRate == null)
                {
                    cost.ExchangeRate = -1;
                    costAnalyze.ExchangeRate = -1;
                }

                else
                {
                    cost.ExchangeRate = country.ExChangeRate.Value;
                    costAnalyze.ExchangeRate = country.ExChangeRate.Value;
                }

                if (variantSelected.Product.CountryOfOrgin.Duty != null)
                {
                    cost.Duty = variantSelected.Product.CountryOfOrgin.Duty.Value;
                    costAnalyze.Duty = variantSelected.Product.CountryOfOrgin.Duty.Value;
                }

                else
                {
                    cost.Duty = -1;
                    costAnalyze.Duty = -1;
                }

                costAnalyze.FobPrice = 0.01m;
                FobPriceCostCenter = Convert.ToDecimal(variantSelected.FobPrice).ToString();
                cost.FobPrice = Convert.ToDecimal(variantSelected.FobPrice);
                cost.WholeSaleA = variantSelected.WholesaleA.ToString();
                cost.WholeSaleB = variantSelected.WholesaleB.ToString();
                cost.RetailPrice = variantSelected.RetailPrice.ToString();
                InfoProduct.CostCenter = cost;
                InfoProduct.CostCenterAnalyze = costAnalyze;
                GrdCostCenter.Visibility = Visibility.Visible;
            }

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
            if (e.Key == Key.Enter)
                InfoProduct.CostCenter.Duty = Convert.ToDecimal(txtCostRate.Text);
        }
        private void TxtExChangeRate_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
            ControlKeyDown(e.Key, txtExChangeRate);
            if (e.Key == Key.Enter)
                InfoProduct.CostCenter.ExchangeRate = Convert.ToDecimal(txtExChangeRate.Text);
        }
        private void TxtMainPrice_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCostRate.Text))
                ControlKeyDown(Key.Escape, txtMainPrice);
            else
                ControlKeyDown(Key.Enter, txtMainPrice);
        }

        private void TxtCostRate_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCostRate.Text))
                ControlKeyDown(Key.Escape, txtCostRate);
            else
            {
                ControlKeyDown(Key.Enter, txtCostRate);
                InfoProduct.CostCenter.Duty = Convert.ToDecimal(txtCostRate.Text);
            }


        }

        private void TxtExChangeRate_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExChangeRate.Text))
                ControlKeyDown(Key.Escape, txtExChangeRate);
            else
            {
                ControlKeyDown(Key.Enter, txtExChangeRate);
                InfoProduct.CostCenter.ExchangeRate = Convert.ToDecimal(txtExChangeRate.Text);
            }


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
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {

                }
                else
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
                //lvProducts.SelectedIndex = IndexOfLvProduct;
                if (cmbType.SelectedIndex == 2)
                    txtSearch.Focus();
                if (cmbType.SelectedIndex == 5 && IsChangeStarVariant)
                    UpdateStarList();
            }
            else
            {
                GrdInformationProduct.Visibility = Visibility.Visible;
                GrSearch.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateStarList()
        {
            ListForLvProduct = new ObservableCollection<VariantViewModel>(_productInformationService.AllStarVariantList());
            viewModel.LvProductItemSource = ListForLvProduct;
            txtSearch.Text = "";
            lblCountResult.Content = "Count :" + ListForLvProduct.Count();
            IsChangeStarVariant = false;
        }
        private void BtnUpdateVariant_OnClick(object sender, RoutedEventArgs e)
        {

            try
            {
                if (cmbEditVariantColor.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Choose the Color");
                }
                else if (cmbEditVariantProductType.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Choose the Product Type");
                }
                else
                {
                    var x = InfoProduct.VariantSelected.Id;
                    bool IsAddColor = false;
                    if (!String.IsNullOrWhiteSpace(txtColorNameInEditVariant.Text))
                    {
                        var t = _productInformationService.AddColour(txtColorNameInEditVariant.Text, txtPantonInEditVariant.Text);
                        InfoProduct.VariantSelected.ColourFK = t.Id;
                        IsAddColor = true;
                        txtColorNameInEditVariant.Clear();
                        txtPantonInEditVariant.Clear();
                    }

                    InfoProduct.VariantSelected.ColourFK = (int)cmbEditVariantColor.SelectedValue;
                    _productInformationService.AddOrUpdateVariant(InfoProduct.VariantSelected, InfoProduct.Id,CheckBoxUpdateSameVariants.IsChecked.Value);
                    if (x == InfoProduct.VariantSelected.Id)
                    {
                        int IndexOfVariant = InfoProduct.List.ToList().FindIndex(p => p.Id == InfoProduct.VariantSelected.Id);
                        int CurrentColor = InfoProduct.List[IndexOfVariant].Colour.Id;
                        InfoProduct.List[IndexOfVariant].Colour.Name = (IsAddColor) ? txtColorNameInEditVariant.Text : cmbEditVariantColor.Text;
                        InfoProduct.List[IndexOfVariant].Colour.Id = InfoProduct.VariantSelected.ColourFK.Value;
                        InfoProduct.List[IndexOfVariant].ColourFK = InfoProduct.VariantSelected.Colour.Id;

                        //InfoProduct.List.FirstOrDefault(p => p.Id == InfoProduct.VariantSelected.Id).Colour.Name =
                        //    (IsAddColor) ? txtColorNameInEditVariant.Text : cmbEditVariantColor.Text;
                        //InfoProduct.List.FirstOrDefault(p => p.Id == InfoProduct.VariantSelected.Id).ColourFK = InfoProduct.VariantSelected.ColourFK;

                        lvVariant.Items.Refresh();
                        if (CurrentColor != Convert.ToInt16(cmbEditVariantColor.SelectedValue))
                        {
                            ListForLvProduct.FirstOrDefault(p => p.Id == InfoProduct.VariantSelected.Id).Colour = new Colour() { Name = (IsAddColor) ? txtColorNameInEditVariant.Text : cmbEditVariantColor.Text };
                            VaraintList.FirstOrDefault(p => p.Id == InfoProduct.VariantSelected.Id).Colour = new Colour() { Name = (IsAddColor) ? txtColorNameInEditVariant.Text : cmbEditVariantColor.Text };
                        }

                    }
                    else
                    {
                        var newVariant = _productInformationService.GiveMeVariantById(InfoProduct.VariantSelected.Id);
                        newVariant.Sku = "Add SKU";
                        newVariant.Barcode = new Barcode();
                        newVariant.Barcode.BarcodeNumber = "Add Barcode";
                        InfoProduct.List.Add(newVariant);
                        VaraintList.Add(new VariantViewModel()
                        {
                            ColourFK = newVariant.ColourFK,
                            Colour = newVariant.Colour,
                            Size = newVariant.Size,
                            ProductType = newVariant.ProductType,
                            Product = newVariant.Product,
                            Id = newVariant.Id,
                            Data1 = newVariant.Data1
                        });
                        ListForLvProduct.Add(new VariantViewModel()
                        {
                            ColourFK = newVariant.ColourFK,
                            Colour = newVariant.Colour,
                            Size = newVariant.Size,
                            ProductType = newVariant.ProductType,
                            Product = newVariant.Product,
                            Id = newVariant.Id,
                            Data1 = newVariant.Data1
                        });

                    }

                    if (CheckBoxUpdateSameVariants.IsChecked == true)
                    {
                        int IndexOfVariantt = InfoProduct.List.ToList().FindIndex(p => p.Id == InfoProduct.VariantSelected.Id);
                        foreach (var variant in InfoProduct.List)
                        {
                            if(variant.ProductTypeFK == InfoProduct.List[IndexOfVariantt].ProductTypeFK&& variant.Size.Trim().CompareTo(InfoProduct.List[IndexOfVariantt].Size.Trim()) == 0)
                            {
                                variant.WholesaleA = InfoProduct.List[IndexOfVariantt].WholesaleA;
                                variant.WholesaleB = InfoProduct.List[IndexOfVariantt].WholesaleB;
                                variant.FobPrice = InfoProduct.List[IndexOfVariantt].FobPrice;
                                variant.RetailPrice = InfoProduct.List[IndexOfVariantt].RetailPrice;
                            }
                        }
                    }

                    cmbEditVariantColor.ItemsSource = _repositoryService.AllColourList().ToList();
                    cmbEditVariantColor.Items.Refresh();
                    GrdEditVariant.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception exception)
            {
                var x = exception;
                CheckConectionToServe();
                if (IsConnectToServer == true)
                    MessageBox.Show("Error 110");
                else
                {
                    ShowErrorMassegeToConectionInternet();
                }
            }


        }



        private void BtnCloseGrdEditVariant_OnClick(object sender, RoutedEventArgs e)
        {
            GrdEditVariant.Visibility = Visibility.Hidden;
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

                    UploadImage(DestentionPath);



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

        private async Task UploadImage(string DestentionPath)
        {
            try
            {
                await Task.Run(() =>
                {
                    _repositoryService.UploadFileToFTP(DestentionPath, "/VariantsImage/");
                    //MessageBox.Show("Image Uploaded");
                });
            }
            catch (Exception e)
            {
                MessageBox.Show("Error In upload Image");
            }

        }


        private void LvVariant_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SelectedIndexVariant = lvVariant.SelectedIndex;

            if (SelectedIndexVariant != -1)
            {
                countImageVariant = InfoProduct.List[SelectedIndexVariant].Images.Count;
                ShowImageVariant(SelectedIndexVariant);
                grdNoteVariant.Visibility = Visibility.Visible;
                ShowNoteVariant();
                ShowStateStarOfVariant();
                ImageSelected = 0;
            }

            else
            {
                btnNextImageVariant.Visibility = Visibility.Hidden;
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
                btnNextFullImage.Visibility = Visibility.Hidden;
                btnPreviousFullImage.Visibility = Visibility.Hidden;
            }

        }

        private void ShowNoteVariant()
        {
            txtRemarksVariant.Text = InfoProduct.List[SelectedIndexVariant].Note;
        }
        private void ShowStateStarOfVariant()
        {
            if (InfoProduct.List[SelectedIndexVariant].IsStar)
                btnIsSetStarForVariant.Background = Brushes.Goldenrod;
            else
                btnIsSetStarForVariant.Background = new SolidColorBrush(Color.FromRgb(96, 125, 139));

        }

        private void ShowImageVariant(int index, int imgSelect = 0)
        {
            var images = InfoProduct.List[index].Images;
            if (images.Count == 0)
            {
                imgVariant.Background = new ImageBrush(new BitmapImage(new Uri(Path + "not-found.JPG")));
                btnNextImageVariant.Visibility = Visibility.Hidden;
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
                btnNextFullImage.Visibility = Visibility.Hidden;
                btnPreviousFullImage.Visibility = Visibility.Hidden;
            }
            else if (images.Count == 1)
            {
                btnNextImageVariant.Visibility = Visibility.Hidden;
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
                btnNextFullImage.Visibility = Visibility.Hidden;
                btnPreviousFullImage.Visibility = Visibility.Hidden;
                ShowImage();
            }
            else if (images.Count - 1 == ImageSelected)
            {
                btnNextImageVariant.Visibility = Visibility.Hidden;
                btnNextFullImage.Visibility = Visibility.Hidden;
                ShowImage();
            }
            else if (ImageSelected == 0)
            {
                btnPerviosImageVariant.Visibility = Visibility.Hidden;
                btnPreviousFullImage.Visibility = Visibility.Hidden;
                btnNextImageVariant.Visibility = Visibility.Visible;
                btnNextFullImage.Visibility = Visibility.Visible;
                ShowImage();
            }
            else
            {
                btnNextImageVariant.Visibility = Visibility.Visible;
                btnNextFullImage.Visibility = Visibility.Visible;
                btnPerviosImageVariant.Visibility = Visibility.Visible;
                btnPreviousFullImage.Visibility = Visibility.Visible;
                ShowImage();
            }

            void ShowImage()
            {
                var imageName = InfoProduct.List[index].Images.ElementAt(imgSelect).ImageName;
                try
                {
                    imgVariant.Background = new ImageBrush(new BitmapImage(new Uri(Path + imageName)));
                    imgFullImage.Background = new ImageBrush(new BitmapImage(new Uri(Path + imageName)));
                }
                catch (Exception e)
                {
                    myMessageQueue.Enqueue("Could Not Find ImageFile.Please Update");
                    imgVariant.Background = new ImageBrush(new BitmapImage(new Uri(Path + "not-found.JPG")));

                }


            }
        }

        private void BtnNextImageVariant_OnClick(object sender, RoutedEventArgs e)
        {

            ++ImageSelected;
            ShowImageVariant(SelectedIndexVariant, ImageSelected);
        }


        private void BtnPreviousImageVariant_OnClick(object sender, RoutedEventArgs e)
        {
            --ImageSelected;
            ShowImageVariant(SelectedIndexVariant, ImageSelected);
        }

        private void ImgVariant_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            GrdShowImage.Visibility = Visibility.Visible;
        }

        private void ImgFullImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            GrdShowImage.Visibility = Visibility.Hidden;
        }

        private void BtnSizeEditProduct_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //InfoProduct.VariantSelected.Size = InfoProduct.VariantSelected.length.ToString() + "X" +
            //                                   InfoProduct.VariantSelected.Width.ToString();
            //MessageBox.Show(InfoProduct.VariantSelected.Size);
        }

        private void BtnAddNewVariant_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsConnectToServer)
            {
                ProductTypeForAddVariant.Visibility = Visibility.Visible;
                InfoProduct.VariantSelected = new Variant();
                HideAddColorPart();
                GrdEditVariant.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Mode Is Offline");
            }
        }


        private void BtnAddColorInEditVariant_OnClick(object sender, RoutedEventArgs e)
        {
            RowcmbColor.IsEnabled = false;
            RowColorName.Visibility = Visibility.Visible;
            RowPantone.Visibility = Visibility.Visible;
        }

        private void HideAddColorPart()
        {
            RowcmbColor.IsEnabled = true;
            RowColorName.Visibility = Visibility.Collapsed;
            RowPantone.Visibility = Visibility.Collapsed;
        }
        private void TxtColorNameInEditVariant_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtColorNameInEditVariant.Text))
            {
                var color = _productInformationService.IsExistColorByPantonNumber(txtColorNameInEditVariant.Text);
                if (color != null)
                {
                    MessageBox.Show("This Color Before Added");
                    cmbEditVariantColor.SelectedValue = color.Id;
                    RowcmbColor.IsEnabled = true;
                    txtColorNameInEditVariant.Clear();
                    txtPantonInEditVariant.Clear();
                    RowColorName.Visibility = Visibility.Collapsed;
                    RowPantone.Visibility = Visibility.Collapsed;
                }

            }
        }

        private void BtnUpdateNoteVariant_OnClick(object sender, RoutedEventArgs e)
        {
            var id = InfoProduct.List[SelectedIndexVariant].Id;
            _productInformationService.UpdateVariantNote(id, txtNoteVariant.Text);
            txtRemarksVariant.Text = txtNoteVariant.Text;
            GrdEditNoteVariant.Visibility = Visibility.Hidden;
            myMessageQueue.Enqueue("Variant Updated.");
            InfoProduct.List[SelectedIndexVariant].Note = txtNoteVariant.Text;




        }

        private void BtnCloseGrdEditNoteVariant_OnClick(object sender, RoutedEventArgs e)
        {
            GrdEditNoteVariant.Visibility = Visibility.Hidden;
        }


        private void BtnEditNoteVariant_OnClick(object sender, RoutedEventArgs e)
        {
            txtNoteVariant.Text = txtRemarksVariant.Text;
            GrdEditNoteVariant.Visibility = Visibility.Visible;
            txtNoteVariant.Focus();
            txtNoteVariant.Select(txtNoteVariant.Text.Length, 0);
        }

        private void BtnCloseGrdEditProduct_OnClick(object sender, RoutedEventArgs e)
        {
            GrdEditProduct.Visibility = Visibility.Hidden;
        }

        private void LblStylenumber_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GrdEditProduct.Visibility = Visibility.Visible;
        }

        private void BtnUpdateProduct_OnClick(object sender, RoutedEventArgs e)
        {
            _productInformationService.UpdateProduct(InfoProduct);
            GrdEditProduct.Visibility = Visibility.Hidden;
            myMessageQueue.Enqueue(InfoProduct.StyleNumber + " Updated.");

        }

        private void BtnAddProductToProductMaster_OnClick(object sender, RoutedEventArgs e)
        {
            //_productInformationService.TransferProductsToProductMaster(SelectedList);
            int res = _productInformationService.ExcelForQuickbooks(SelectedList);
            if (res == -10)
                MessageBox.Show("Please Close Excel File");
            else if (res == -1)
                MessageBox.Show("Please Check Barcodes");
            else
            {
                myMessageQueue.Enqueue("Excel File Created.");

            }
        }

        private void LblFobPriceInCostCenter_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(FobPriceCostCenter);
        }


        private void BtnIsSetStarForVariant_OnClick(object sender, RoutedEventArgs e)
        {
            var variant = lvVariant.SelectedItems[0] as Variant;

            if (btnIsSetStarForVariant.Background == Brushes.Goldenrod)
            {
                btnIsSetStarForVariant.Background = new SolidColorBrush(Color.FromRgb(96, 125, 139));
                _productInformationService.SetStar(variant.Id, false);
            }

            else
            {
                btnIsSetStarForVariant.Background = Brushes.Goldenrod;
                _productInformationService.SetStar(variant.Id);
            }

            IsChangeStarVariant = true;
        }

        private void TxtWholeCashCAD_OnLostFocus(object sender, RoutedEventArgs e)
        {
            InfoProduct.CostCenter.Calculate();
        }
    }
}
