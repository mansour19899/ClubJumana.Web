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
        int step = 0;
        public AddProduct()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _productInformationService=new ProductInformationService();
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

            panels = new List<ScrollViewer>() { Scr1, Scr2, Scr3/*, Scr4, Scr5 */};
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
            var tt = addVariant.Product.MaterialFK;
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
                    ProductType = new ProductType() { Name = cmbProductType.Text},
                    FobPrice = addVariant.Variant.FobPrice,
                    WholesaleA = addVariant.Variant.WholesaleA,
                    WholesaleB = addVariant.Variant.WholesaleB,
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

                //string mac = GetMACAddress().ToString();
                //int categoryId = Convert.ToInt16(cmbCategory.SelectedValue.ToString());
                //int SubCategoryId = Convert.ToInt16(cmbSubCategory.SelectedValue.ToString());
                //int ProducttypeId = Convert.ToInt16(cmbProductType.SelectedValue.ToString());
                //int colorId = Convert.ToInt16(cmbColors.SelectedValue.ToString());

                //if (!IsExistCompany)
                //{
                //    Company company = new Company();
                //    company.Id = db.GiveMeCompanyId(mac);
                //    company.CompanyName = txtCompany.Text;
                //    company.Manufacture = txtManufacture.Text;
                //    company.Website = txtWebSite.Text;
                //    company.Email = txtEmail.Text;
                //    company.StreetAddress = txtStreetAddress.Text;
                //    company.AddressLine2 = txtAddressLine2.Text;
                //    company.City = txtCity.Text;
                //    company.StateProvinceRegion = txtStateProvineRegion.Text;
                //    company.ZipPostlCode = txtZipPostalCode.Text;
                //    company.Country_Id_fk = Convert.ToInt16(cmbCountry.SelectedValue.ToString());
                //    company.Phone = txtPhone.Text;
                //    company.FAX = txtFax.Text;

                //    db.AddCompany(company);
                //    //if (ttt)
                //    //    MessageBox.Show("Sucsess Save   " + company.CompanyName);
                //    //else
                //    //    MessageBox.Show("Error Db Save Company");
                //}




                //string lastSku = db.LastSku();
                //string LastStyleNumber = db.LastStyleNumber();

                //GenerateSkuStyle cd = new GenerateSkuStyle();
                //var tt = cd.Generate(categoryId, SubCategoryId, ProducttypeId, colorId, lastSku, LastStyleNumber);

                //product = new OwnProduct();
                //product.Id = db.GiveMeProductId(mac);
                //product.ProductType_Id_fk = ProducttypeId;
                //product.Sku = tt["Sku"];
                //product.StyleNumber = tt["StyleNumber"];
                //product.ProductTittle = txtProductTittle.Text;
                //product.Color_Id_fk = Convert.ToInt16(colorId);
                //product.Material_Id_fk = Convert.ToInt16(cmbMaterial.SelectedValue.ToString());
                //product.Brand_Id_fk = Convert.ToInt16(cmbBrand.SelectedValue.ToString());
                //product.CountryofOrgin_Id_fk = Convert.ToInt16(cmbCountryOfOrgin.SelectedValue.ToString());
                //product.Box = chkBox.IsChecked;
                //product.Bag = chkBag.IsChecked;
                //product.Wrap = chkWrap.IsChecked;
                //product.NoPackaging = chkNoPacking.IsChecked;
                //product.AirTransportation = chkAirTransportation.IsChecked;
                //product.ShipTransportation = chkShipTransportation.IsChecked;
                //product.Train = chkTrain.IsChecked;
                //product.Truck = chkTruck.IsChecked;

                //if (txtGsm.Text.Trim() != "")
                //    product.Gsm = Int16.Parse(txtGsm.Text);
                ////if (txtWidth.Text.Trim() != "")
                ////    product.Width = float.Parse(txtWidth.Text);
                ////if (txtLenght.Text.Trim() != "")
                ////    product.length = float.Parse(txtLenght.Text);
                ////if (txtDepth.Text.Trim() != "")
                ////    product.Depth = float.Parse(txtDepth.Text);
                ////if (txtHeight.Text.Trim() != "")
                ////    product.Height = float.Parse(txtHeight.Text);
                ////if (txtWeight.Text.Trim() != "")
                ////    product.Weight = float.Parse(txtWeight.Text);
                //product.SpecialPackingInstructions = txtSpecialPackaing.Text;
                //if (txtPrice.Text.Trim() != "")
                //    product.Price = float.Parse(txtPrice.Text);

                //product.Width = float.Parse(cvrt.WidthI.ToString());
                //product.length = float.Parse(cvrt.LenghtI.ToString());
                //product.Depth = float.Parse(cvrt.DepthI.ToString());
                //product.Height = float.Parse(cvrt.HeightI.ToString());
                //product.Weight = float.Parse(cvrt.WeightI.ToString());

                //product.Company_Id_fk = newCompanyId;
                //product.DescribeMaterial = txtDescribeMaterial.Text;
                addVariant.Product.StyleNumber =
                    _productInformationService.GiveMeStyleNumber(cmbCategory.SelectedIndex,
                        cmbSubCategory.SelectedIndex);
                StringBuilder Review = new StringBuilder(String.Format($" StyleNumber : {addVariant.Product.StyleNumber}  \n\n Category : {cmbCategory.Text}" +
                                                                       $" \n\n SubCategory : {cmbSubCategory.Text} \n\n GSM : {addVariant.Variants.FirstOrDefault().Data1}" +
                                                                       $" \n\n Material : {cmbMaterial.Text} " +
                                                                       $"\n\n\n\n Descibe Material :\n{addVariant.Product.DescribeMaterial}"));
                lvVariants.ItemsSource = addVariant.Variants;
                //foreach (var VARIABLE in addTowel.Towels)
                //{
                //    Review.AppendLine(string.Format($"Product Type: {cmbProductType.Text.Trim()} Color :{cmbColors.Text.Trim()} GSM : {addTowel.Towel.Gsm}   Price:{addTowel.Towel.Price}\n\n"));
                //}
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

        }

        private void txtGsm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWidth_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtWidth_KeyDown(object sender, KeyEventArgs e)
        {

        }


        #endregion

        private void BtnYesForAdd_OnClick(object sender, RoutedEventArgs e)
        {
            _productInformationService.AddTowel(addVariant);
            MessageBox.Show("Product Added");
        }

        private void BtnNoForAdd_OnClick(object sender, RoutedEventArgs e)
        {
            step = 1;
            Scr2.Visibility = Visibility.Visible;
            GrReview.Visibility = Visibility.Hidden;
            GrAddInformation.Visibility = Visibility.Visible;
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
                var list = categoriesSubCategoriesList.Where(p => p.CategoryFK == index || p.CategoryFK == 0).Select(p => p.SubCategory).ToList();
                cmbSubCategory.ItemsSource = list;
                cmbProductType.SelectedIndex = 0;
                cmbSubCategory.SelectedIndex = 0;
                cmbProductType.IsEnabled = false;
                cmbSubCategory.IsEnabled = true;
                addVariant.Variants.Clear();

            }
        }

        private void CmbSubCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSubCategory.SelectedIndex != 0)
            {
                int index = Convert.ToInt16(cmbCategory.SelectedValue);
                int index1 = Convert.ToInt16(cmbSubCategory.SelectedValue);
                var index2 = categoriesSubCategoriesList.SingleOrDefault(p => p.CategoryFK == index && p.SubCategoryFK == index1);
                if (index2 != null)
                {
                    var tt = productTypeslist.Where(p => p.CategorysubcategoreisFK == index2.Id||p.Id==0).Select(p => p).ToList();
                    cmbProductType.ItemsSource = tt;
                    cmbProductType.SelectedIndex = 0;
                    cmbProductType.IsEnabled = true;
                    addVariant.Variants.Clear();
                }


            }
        }

        private void TxtCompany_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var ExsitCompany = _productInformationService.ExistCompany(txtCompany.Text);

            if (ExsitCompany != null)
            {
                var result = MessageBox.Show("This company already exists. Do you want to complate?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
    }
}
