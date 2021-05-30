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
using MaterialDesignThemes.Wpf;

namespace ClubJumana.Wpf2
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private AddVariantInformationViewModel addVariant;
        private RepositoryService _repositoryService;
        private ProductInformationService _productInformationService;
        List<ScrollViewer> panels;
        private List<Country> countriesList;
        private List<Category> categoriesList;
        private List<SubCategory> subCategoriesList;
        private List<CategoriesSubCategory> categoriesSubCategoriesList;
        private List<ProductType> productTypeslist;
        private List<Brand> brandslist;
        private List<Colour> coloursList;
        private List<Material> materialsList;
        private SnackbarMessageQueue myMessageQueue;
        int step = 0;

        public AddProduct()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _productInformationService = new ProductInformationService();

            myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(4000));
            SnackbarResult.MessageQueue = myMessageQueue;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            countriesList = _repositoryService.AllCountriesList().ToList();
            categoriesList = _repositoryService.AllCategoriesList().ToList();
            subCategoriesList = _repositoryService.AllSubCategoriesList().ToList();
            categoriesSubCategoriesList = _repositoryService.AllCategoriesSubCategoryList().ToList();
            productTypeslist = _repositoryService.AllProductTypeList().ToList();
            brandslist = _repositoryService.AllBrandList().ToList();
            coloursList = _repositoryService.AllColourList().ToList();
            materialsList = _repositoryService.AllMaterialList().ToList();

            panels = new List<ScrollViewer>() { Scr1, Scr2, Scr3 /*, Scr4, Scr5 */};
            cmbCountry.ItemsSource = countriesList;
            cmbCountryOfOrgin.ItemsSource = countriesList;
            cmbCategory.ItemsSource = categoriesList;
            cmbSubCategory.ItemsSource = subCategoriesList;
            cmbProductType.ItemsSource = productTypeslist;
            cmbBrand.ItemsSource = brandslist;
            cmbColors.ItemsSource = coloursList;
            cmbMaterial.ItemsSource = materialsList;

            addVariant = new AddVariantInformationViewModel();

            this.DataContext = addVariant;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            panels.ElementAt(step).Visibility = Visibility.Hidden;
            step = step - 1;
            if (step == -1)
            {
                this.Close();
            }
            else
            {
                panels.ElementAt(step).Visibility = Visibility.Visible;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(addVariant.company.CompanyName))
            {
                if (step == 2)
                {
                    if (cmbProductType.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select Product Type");
                    }
                    else
                    {
                        string messageBoxText = "Do you want to add another variant?";
                        string caption = "Add Variant";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Question;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                lblHeader.Content = "yes";
                                AddVariant();
                                txtGsm.IsEnabled = false;
                                break;
                            case MessageBoxResult.No:
                                lblHeader.Content = "no";
                                AddVariant();
                                panels.ElementAt(2).Visibility = Visibility.Hidden;
                                PeriveweAndPrepareForAdd();
                                lblReview.Visibility = Visibility.Visible;
                                lblSuccessOfAddProduct.Visibility = Visibility.Hidden;
                                lblAskQoustionInReview.Content = "Are You Sure For Add?";
                                GrAddInformation.Visibility = Visibility.Hidden;
                                GrReview.Visibility = Visibility.Visible;
                                step = 4;
                                break;

                        }

                    }


                }
                else
                {
                    panels.ElementAt(step).Visibility = Visibility.Hidden;
                    step = step + 1;
                    panels.ElementAt(step).Visibility = Visibility.Visible;
                }

            }
            else
            {
                MessageBox.Show("Enter Company Name");
                addVariant.company.CompanyName = "";
            }

            void AddVariant()
            {
                addVariant.Variants.Add(new Variant()
                {
                    ColourFK = addVariant.Variant.ColourFK,
                    Colour = new Colour() { Name = cmbColors.Text },
                    ProductTypeFK = addVariant.Variant.ProductTypeFK,
                    ProductType = new ProductType() { Name = cmbProductType.Text },
                    FobPrice = addVariant.Variant.FobPrice,
                    WholesaleA = addVariant.Variant.WholesaleA,
                    WholesaleB = addVariant.Variant.WholesaleB,
                    PublishRate = addVariant.Variant.PublishRate,
                    RetailPrice = addVariant.Variant.RetailPrice,
                    Width = addVariant.Variant.Width,
                    length = addVariant.Variant.length,
                    Size = addVariant.Variant.Size,
                    Note = addVariant.Variant.Note,
                    Data1 = addVariant.Variant.Data1,

                });
            }

            var t = addVariant;
        }

        private void PeriveweAndPrepareForAdd()
        {
            if (cmbProductType.SelectedValue == null || cmbSubCategory.SelectedValue == null)
            {
                lblReview1.Content = "Check Category - Sub Category or Product Type";
                lblReview2.Content = "";
                lblReview1.Foreground = Brushes.DarkRed;
                btnYesForAdd.IsEnabled = false;
            }
            else
            {
                lblReview1.Foreground = Brushes.Black;
                btnYesForAdd.IsEnabled = true;

                var tCategory =Convert.ToInt32(cmbCategory.SelectedValue)  ;
                var tSubCategory = Convert.ToInt32(cmbSubCategory.SelectedValue) ;
                addVariant.Product.StyleNumber =
                    _productInformationService.GiveMeStyleNumber(tCategory,
                        tSubCategory);
                StringBuilder Review = new StringBuilder(String.Format(
                    $" StyleNumber : {addVariant.Product.StyleNumber}  \n\n Category : {cmbCategory.Text}" +
                    $" \n\n SubCategory : {cmbSubCategory.Text} \n\n GSM : {addVariant.Variants.FirstOrDefault().Data1}" +
                    $" \n\n Material : {cmbMaterial.Text} " +
                    $"\n\n\n\n Descibe Material :\n{addVariant.Product.DescribeMaterial}"));
                lvVariants.ItemsSource = null;
                lvVariants.Items.Refresh();
                lvVariants.ItemsSource = addVariant.Variants;


                lblReview1.Content = Review;
                lblReview2.Foreground = Brushes.Black;
                lblReview2.Content = "";

            }
        }





        #region MyRegion

        private void txtLenght_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtLenght_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }


        private void txtWidth_KeyUp(object sender, KeyEventArgs e)
        {

        }




        #endregion

        private void BtnYesForAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (lblReview.Visibility == Visibility.Visible)
            {
                _productInformationService.AddTowel(addVariant);
                lblReview.Visibility = Visibility.Hidden;
                lblSuccessOfAddProduct.Visibility = Visibility.Visible;
                lblAskQoustionInReview.Content = "Do you want to add another product?";
            }
            else
            {
                step = 1;
                Scr2.Visibility = Visibility.Visible;
                GrReview.Visibility = Visibility.Hidden;
                addVariant.Variants.Clear();
                txtGsm.IsEnabled = true;
                GrAddInformation.Visibility = Visibility.Visible;
            }

        }

        private void BtnNoForAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (lblReview.Visibility == Visibility.Visible)
            {
                step = 1;
                Scr2.Visibility = Visibility.Visible;
                if (addVariant.Variants.Count == 1)
                {
                    addVariant.Variants.Clear();
                }

                GrReview.Visibility = Visibility.Hidden;
                GrAddInformation.Visibility = Visibility.Visible;
            }
            else
            {
                this.Close();
            }

        }

        private void LvVariants_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string messageBoxText = "Do you want to delete variant?";
            string caption = "Delete Variant";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    addVariant.Variants.RemoveAt(lvVariants.SelectedIndex);
                    lvVariants.ItemsSource = null;
                    lvVariants.ItemsSource = addVariant.Variants;

                    break;
                case MessageBoxResult.No:

                    break;

            }
        }

        private void CmbCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedIndex != 0)
            {
                var index = Convert.ToInt16(cmbCategory.SelectedValue);
                var list = categoriesSubCategoriesList.Where(p => p.CategoryFK == index || p.CategoryFK == 0)
                    .Select(p => p.SubCategory).ToList();
                cmbSubCategory.ItemsSource = list;
                cmbProductType.SelectedIndex = 0;
                cmbSubCategory.SelectedIndex = 0;
                cmbProductType.IsEnabled = false;
                cmbSubCategory.IsEnabled = true;
                addVariant.Variants.Clear();

            }
            else
            {

                cmbSubCategory.IsEnabled = false;
                cmbProductType.IsEnabled = false;
                cmbSubCategory.SelectedIndex = 0;
                cmbProductType.SelectedIndex = 0;
            }
        }

        private void CmbSubCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSubCategory.SelectedIndex != 0)
            {
                int index = Convert.ToInt16(cmbCategory.SelectedValue);
                int index1 = Convert.ToInt16(cmbSubCategory.SelectedValue);
                var index2 =
                    categoriesSubCategoriesList.SingleOrDefault(p =>
                        p.CategoryFK == index && p.SubCategoryFK == index1);
                if (index2 != null)
                {
                    var tt = productTypeslist.Where(p => p.CategorysubcategoreisFK == index2.Id || p.Id == 0)
                        .Select(p => p).ToList();
                    cmbProductType.ItemsSource = tt;
                    cmbProductType.SelectedIndex = 0;
                    cmbProductType.IsEnabled = true;
                    addVariant.Variants.Clear();
                    txtGsm.IsEnabled = true;
                }


            }
            else
            {
                cmbProductType.IsEnabled = false;
                cmbProductType.SelectedIndex = 0;
            }
        }

        private void TxtCompany_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var ExsitCompany = _productInformationService.ExistCompany(txtCompany.Text);

            if (ExsitCompany != null)
            {
                var result = MessageBox.Show("This company already exists. Do you want to complate?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        addVariant.company = ExsitCompany;

                        break;
                    case MessageBoxResult.No:
                        txtCompany.Text = "";
                        break;
                }
            }
        }

        private void LblSize_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            addVariant.Variant.Size = addVariant.Variant.Width.ToString() + " X " + addVariant.Variant.length.ToString();
            txtSize.Text = addVariant.Variant.Size;
        }

        private void TxtWidth_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var txt = sender as TextBox;
            txt.SelectAll();

        }

        private void TxtWidth_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            var txt = sender as TextBox;
            txt.SelectAll();
        }

        private void TxtSize_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter & string.IsNullOrWhiteSpace(txtSize.Text))
            {
                addVariant.Variant.Size = addVariant.Variant.Width.ToString() + "x" + addVariant.Variant.length.ToString();
                txtSize.Text = addVariant.Variant.Size;
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

        private void BtnAddColor_OnClick(object sender, RoutedEventArgs e)
        {
            txtColorNamdForAdd.Clear();
            txtPantoneNumberForAdd.Clear();
            GrdAddColor.Visibility = Visibility.Visible;

        }

        private void BtnCloseGrdAddColor_OnClick(object sender, RoutedEventArgs e)
        {
            GrdAddColor.Visibility = Visibility.Hidden;
        }

        private void BtnOkAddColor_OnClick(object sender, RoutedEventArgs e)
        {
            var t = _productInformationService.AddColour(txtColorNamdForAdd.Text, txtPantoneNumberForAdd.Text);
            
            GrdAddColor.Visibility = Visibility.Hidden;
            myMessageQueue.Enqueue("Color Added.");
            addVariant.Variant.ColourFK = t.Id;
            coloursList = _repositoryService.AllColourList().ToList(); 
            cmbColors.ItemsSource = coloursList;
            cmbColors.Items.Refresh();
            var index = coloursList.FindIndex(p => p.Id == t.Id );
            cmbColors.SelectedIndex = index;

        }

        private void TxtPantoneNumberForAdd_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtPantoneNumberForAdd.Text))
            {
                var color = _productInformationService.IsExistColorByPantonNumber(txtPantoneNumberForAdd.Text);
                if (color != null)
                {
                    MessageBox.Show("This Color Before Added");
                    cmbColors.SelectedValue = color.Id;
                    GrdAddColor.Visibility = Visibility.Hidden;
                    txtColorNamdForAdd.Clear();
                    txtPantoneNumberForAdd.Clear();
                }



            }
        }
    }
}
