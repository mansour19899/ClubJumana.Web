using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClubJumana.Core.Convertors;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.Core.Services;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Entities.Users;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;

namespace ClubJumana.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PurchaseOrder SelectedPurchaseOrder;
        private PoViewModel SelectedPo;
        private AsnViewModel SelectedAsn;
        private GrnViewModel SelectedGrn;
        private ProductMaster selectedProductMaster;
        private Mode Mode = Mode.PO;
        private State state = State.Save;
        private List<Vendor> vendors;
        private List<Warehouse> warehouses;
        public static User user;
        private SnackbarMessageQueue myMessageQueue;
        public ObservableCollection<ItemsOfPurchaseOrderViewModel> itemsOfPurchaseOrderViewModel;
        public List<ItemsOfPurchaseOrderViewModel> RemoveItemsOfPurchaseOrderViewModel;
        private bool ModeAddItem = false;
        private bool IsViewDetail = false;

        private PerchaseOrderService _purchaseOrderService;
        private RepositoryService _repositoryService;
        private UserService _userService;
        private ProductService _productService;
        private PurchaseOrderConvertor convertor;
        public MainWindow()
        {
            InitializeComponent();
            _purchaseOrderService = new PerchaseOrderService();
            _repositoryService = new RepositoryService();
            _userService = new UserService();
            _productService=new ProductService();
            RemoveItemsOfPurchaseOrderViewModel = new List<ItemsOfPurchaseOrderViewModel>();
            vendors = _repositoryService.AllVendor().ToList();
            warehouses = _repositoryService.AllWarehouse().ToList();
            user = _userService.LoginUser();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            lvProducts.ItemsSource = _repositoryService.AllProductList().ToList();
            itemsOfPurchaseOrderViewModel = new ObservableCollection<ItemsOfPurchaseOrderViewModel>();
            dgItems.ItemsSource = itemsOfPurchaseOrderViewModel;

            myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(4000));
            SnackbarResult.MessageQueue = myMessageQueue;

        }

        void HidePanel()
        {
            GrdNewPurchersOrder.Visibility = Visibility.Hidden;
            //   GrdSale.Visibility = Visibility.Hidden;
            GrdPurchesOrderList.Visibility = Visibility.Hidden;
            GrdProductList.Visibility = Visibility.Hidden;
            GrdTotalCharges.Visibility = Visibility.Hidden;
            GrdProductInformation.Visibility = Visibility.Hidden;
            //GrdSaleOrder.Visibility = Visibility.Hidden;
            //GrdOrder.Visibility = Visibility.Hidden;


        }
        void ShowSideBar(Mode mode, int sub = 1)
        {
            switch (mode)
            {
                case Mode.PO:
                    if (sub == 2)
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Visible;
                        btnRemove.Visibility = Visibility.Visible;
                        btnDone.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Collapsed;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Collapsed;
                    }
                    break;
                case Mode.Asn:
                    if (sub == 2)
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Visible;
                        btnSave.Visibility = Visibility.Visible;
                        btnRemove.Visibility = Visibility.Visible;
                        btnDone.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Visible;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Collapsed;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Collapsed;
                    }

                    break;
                case Mode.Grn:
                    if (sub == 2)
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Visible;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                        btnAddItem.Visibility = Visibility.Collapsed;
                        btnSave.Visibility = Visibility.Collapsed;
                        btnRemove.Visibility = Visibility.Collapsed;
                        btnDone.Visibility = Visibility.Collapsed;
                    }

                    break;
                case Mode.Sale:
                    btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                    btnAddItem.Visibility = Visibility.Visible;
                    btnSave.Visibility = Visibility.Visible;
                    btnRemove.Visibility = Visibility.Visible;
                    btnDone.Visibility = Visibility.Visible;
                    break;
                case Mode.productInformation:
                    btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                    btnAddItem.Visibility = Visibility.Collapsed;
                    btnSave.Visibility = Visibility.Visible;
                    btnRemove.Visibility = Visibility.Collapsed;
                    btnDone.Visibility = Visibility.Collapsed;
                    break;
                case Mode.Order:
                    btnNewPurchaseOrder.Visibility = Visibility.Collapsed;
                    btnAddItem.Visibility = Visibility.Collapsed;
                    btnSave.Visibility = Visibility.Visible;
                    btnRemove.Visibility = Visibility.Collapsed;
                    btnDone.Visibility = Visibility.Visible;
                    break;
                case Mode.Nothong:

                    break;
            }
        }

        private void LvPurchaseOrder_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (PurchaseOrder)lvPurchaseOrder.ItemContainerGenerator.ItemFromContainer(dep);
            convertor = new PurchaseOrderConvertor();
            SelectedPurchaseOrder = _purchaseOrderService.GivePurchaseOrderById(wer.Id);
            SelectedPo = convertor.ConvertToPO(SelectedPurchaseOrder, vendors, warehouses);
            SelectedAsn = convertor.ConvertToAsn(SelectedPurchaseOrder, vendors, warehouses);
            SelectedGrn = convertor.ConvertToGrn(SelectedPurchaseOrder, vendors, warehouses);
            itemsOfPurchaseOrderViewModel.Clear();

            itemsOfPurchaseOrderViewModel = convertor.CovertItemsOfPurchaseOrderViewModels(SelectedPurchaseOrder, Mode);
            dgItems.ItemsSource = itemsOfPurchaseOrderViewModel;

            state = State.Update;
            lblSave.Content = "Update";
            IsViewDetail = true;
            switch (Mode)
            {
                case Mode.PO:
                    DataContext = SelectedPo;
                    break;
                case Mode.Asn:
                    DataContext = SelectedAsn;
                    break;
                case Mode.Grn:
                    DataContext = SelectedGrn;
                    break;
                default:
                    break;
            }


            HidePanel();
            ShowSideBar(Mode, 2);
            GrdNewPurchersOrder.Visibility = Visibility.Visible;
        }

        private void BtnNewPurchaseOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (Mode)
            {
                case Mode.PO:
                    SelectedPo = new PoViewModel();
                    SelectedPo.VendorsList = vendors;
                    SelectedPo.WarehousesList = warehouses;
                    DataContext = SelectedPo;
                    break;
                case Mode.Asn:
                    SelectedAsn = new AsnViewModel();
                    SelectedAsn.VendorsList = vendors;
                    SelectedAsn.WarehousesList = warehouses;
                    DataContext = SelectedAsn;
                    break;
                default:
                    MessageBox.Show("Error 1377");
                    break;
            }


            itemsOfPurchaseOrderViewModel.Clear();
            HidePanel();
            ShowSideBar(Mode, 2);
            GrdNewPurchersOrder.Visibility = Visibility.Visible;

        }
        private void BtnPurchaseOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lblSave.Content = "Save";
            lblDone.Content = "Done";

            if (IsViewDetail)
            {
                if (Mode == Mode.PO)
                {

                    lvPurchaseOrder.ItemsSource = _repositoryService.AllPurchaseOrder().ToList();
                    HidePanel();
                    GrdPurchesOrderList.Visibility = Visibility.Visible;
                    ShowSideBar(Mode.PO);
                    IsViewDetail = false;
                }
                else if (Mode == Mode.Asn || Mode == Mode.Grn)
                {
                    Mode = Mode.PO;
                    lblNewPo.Content = "New PO";
                    DataContext = SelectedPo;
                    itemsOfPurchaseOrderViewModel = convertor.CovertItemsOfPurchaseOrderViewModels(SelectedPurchaseOrder, Mode);
                    dgItems.ItemsSource = itemsOfPurchaseOrderViewModel;
                    ShowSideBar(Mode.PO, 2);
                }
                else
                {

                }
            }
            else
            {
                Mode = Mode.PO;
                lblNewPo.Content = "New PO";
                lvPurchaseOrder.ItemsSource = _repositoryService.AllPurchaseOrder().ToList();
                HidePanel();
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                ShowSideBar(Mode.PO);

            }
        }
        private void BtnAsnVorchers_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lblSave.Content = "Save";
            lblDone.Content = "Done";
            if (IsViewDetail)
            {

                switch (Mode)
                {
                    case Mode.Asn:
                        {
                            lvPurchaseOrder.ItemsSource = _repositoryService.AsnPurchaseOrder().ToList();
                            HidePanel();
                            GrdPurchesOrderList.Visibility = Visibility.Visible;
                            IsViewDetail = false;
                            ShowSideBar(Mode.Asn);

                            break;
                        }
                    case Mode.PO:
                    case Mode.Grn:
                        {
                            Mode = Mode.Asn;
                            lblNewPo.Content = "New GIT";
                            DataContext = SelectedAsn;
                            itemsOfPurchaseOrderViewModel = convertor.CovertItemsOfPurchaseOrderViewModels(SelectedPurchaseOrder, Mode);
                            dgItems.ItemsSource = itemsOfPurchaseOrderViewModel;
                            ShowSideBar(Mode.Asn, 2);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Error 534");
                            break;
                        }
                }
            }
            else
            {
                Mode = Mode.Asn;
                lblNewPo.Content = "New GIT";
                lvPurchaseOrder.ItemsSource = _repositoryService.AsnPurchaseOrder().ToList();
                HidePanel();
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                ShowSideBar(Mode.Asn);
            }

        }

        private void BtnGrn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lblSave.Content = "Save";
            lblDone.Content = "Done";
            if (IsViewDetail)
            {
                if (Mode == Mode.Grn)
                {

                    lvPurchaseOrder.ItemsSource = _repositoryService.GrnPurchaseOrder().ToList();
                    HidePanel();
                    GrdPurchesOrderList.Visibility = Visibility.Visible;
                    IsViewDetail = false;
                    ShowSideBar(Mode.Grn);
                }
                else if (Mode == Mode.PO || Mode == Mode.Asn)
                {
                    Mode = Mode.Grn;
                    DataContext = SelectedGrn;
                    itemsOfPurchaseOrderViewModel = convertor.CovertItemsOfPurchaseOrderViewModels(SelectedPurchaseOrder, Mode);
                    dgItems.ItemsSource = itemsOfPurchaseOrderViewModel;
                    ShowSideBar(Mode.Grn, 2);
                }

                else
                {

                }

            }
            else
            {
                Mode = Mode.Grn;

                lvPurchaseOrder.ItemsSource = _repositoryService.GrnPurchaseOrder().ToList();
                HidePanel();
                GrdPurchesOrderList.Visibility = Visibility.Visible;
                ShowSideBar(Mode.Grn);

            }
        }

        private void BtnSaleOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnProduct_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mode = Mode.productInformation;
            IsViewDetail = false;
            ModeAddItem = false;
            HidePanel();
            ShowSideBar(Mode);
            GrdProductList.Visibility = Visibility.Visible;
        }

        private void BtnOrders_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnSave_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == Mode.productInformation)
            {
                SaveAndUpdateProductInformation(true);
            }
            else if (Mode == Mode.Sale)
            {
                //SaveSaleOrder();
            }
            else if (Mode == Mode.Order)
            {
                if (lblSave.Content == "Update")
                {
                    //btnAddItem.Visibility = Visibility.Collapsed;
                    //btnRemove.Visibility = Visibility.Collapsed;
                    //dgSoItems.IsReadOnly = true;
                    //lblSave.Content = "Edit";
                }
                else
                {
                    //dgSoItems.IsReadOnly = false;
                    //lblSave.Content = "Update";
                    //btnAddItem.Visibility = Visibility.Visible;
                    //btnRemove.Visibility = Visibility.Visible;
                }

            }
            else
            {
                SaveAndUpdate();

            }



        }
        private void BtnDone_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SaveAndUpdate(true);
        }

        void SaveAndUpdate(bool done = false)
        {
            string message = "";
            bool IsSuccessSavedOrUpdated = false;
            switch (Mode)
            {
                case Mode.PO:
                    SelectedPo.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated= _purchaseOrderService.AddOrUpdatePoViewModel(SelectedPo, itemsOfPurchaseOrderViewModel.Concat(RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Asn:
                    SelectedAsn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated=_purchaseOrderService.AddOrUpdateAsnViewModel(SelectedAsn, itemsOfPurchaseOrderViewModel.Concat(RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Grn:
                    SelectedGrn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated= _purchaseOrderService.AddOrUpdateGrnViewModel(SelectedGrn, itemsOfPurchaseOrderViewModel.Concat(RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
            }

            if(IsSuccessSavedOrUpdated)
            {
                if (done)
                {
                    message = "Purchase Order Done";
                }
                else if (SelectedAsn!=null)
                {

                    if(SelectedAsn.Id==0)
                        message = "Goods Transit Saved";

                }
                else if (SelectedPo!=null)
                {
                    if (SelectedPo.Id == 0)
                        message = "Purchase Order Saved";
                }
                else
                {
                    message = "Purchase Order Updated";
                }

                myMessageQueue.Enqueue(message);
            }


        }
        private void BtnAddItem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ModeAddItem = true;
            lvProducts.ItemsSource = _repositoryService.AllProductList().ToList();
            HidePanel();
            GrdProductList.Visibility = Visibility.Visible;

        }

        private void LvProducts_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (ProductMaster)lvProducts.ItemContainerGenerator.ItemFromContainer(dep);

            if (ModeAddItem)
            {
                if (Mode == Mode.Sale || Mode == Mode.Order)
                {
                    //decimal price = 0;
                    //price = (rdoWholesale.IsChecked == true) ? wer.WholesalePrice.Value : wer.RetailPrice.Value;
                    //if (!listSoItems.Any(p => p.ProductMaster_fk == wer.Id))
                    //    listSoItems.Add(new SoItem()
                    //    {
                    //        ProductMaster = wer,
                    //        ProductMaster_fk = wer.Id,
                    //        Quantity = 0,
                    //        TotalPrice = 0m
                    //        ,
                    //        Price = price,
                    //        Cost = wer.Cost.Value
                    //    });

                    //HidePanel();
                    //GrdSaleOrder.Visibility = Visibility.Visible;
                }
                else
                {
                    //if (!ItemsOfPurchaseOrderViewModel.Any(p => p.ProductMaster_fk== wer.Id))
                    //{
                    itemsOfPurchaseOrderViewModel.Add(new ItemsOfPurchaseOrderViewModel()
                    {
                        Id = 0,
                        ProductMaster_fk = wer.Id,
                        ProductMaster = wer,
                        Price = wer.FobPrice.Value,
                        Quantity = 0,
                        TotalItemPrice = 0,
                        Checked = true
                    });
                    //}


                    //FillDataGrid(itemsList);
                    HidePanel();
                    GrdNewPurchersOrder.Visibility = Visibility.Visible;
                    //StepAddItem++;
                }


                ModeAddItem = false;

            }
            else
            {
                selectedProductMaster = wer;
                this.DataContext = selectedProductMaster;
                GroupBoxInventory_OnMouseDown(null, null);
                lblSave.Content = "Edit";
                lblSaleProduct.Content = "---------------";
                lblIncomeProduct.Content = "---------------";
                HidePanel();

                ShowSideBar(Mode.productInformation);
                GrdProductInformation.Visibility = Visibility.Visible;
            }
        }



        private void DgItems_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            decimal SumPrice = 0;
            DataGridColumn col1 = e.Column;
            DataGridRow row1 = e.Row;
            ItemsOfPurchaseOrderViewModel t = e.Row.DataContext as ItemsOfPurchaseOrderViewModel;

            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            int col_index = col1.DisplayIndex;

            var SelectItem = itemsOfPurchaseOrderViewModel.FirstOrDefault(p => p.Id == t.Id);
            string header = col1.Header as string;

            if (header.CompareTo("Price") == 0)
            {
                var Price = Convert.ToDecimal(((TextBox)e.EditingElement).Text);
                SelectItem.TotalItemPrice = Price * SelectItem.Quantity;
            }
            else
            {
                var QyT = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                SelectItem.TotalItemPrice = QyT * SelectItem.Price;
            }


            SumPrice = SelectItem.TotalItemPrice;

            foreach (var VARIABLE in itemsOfPurchaseOrderViewModel)
            {
                if (VARIABLE.Id != SelectItem.Id)
                {
                    SumPrice += VARIABLE.TotalItemPrice;
                }

            }

            switch (Mode)
            {
                case Mode.PO:
                    SelectedPo.TotalPrice = SumPrice;
                    SelectedPo.SubtotalPrice = SumPrice;
                    txtSubtotoalPrice.Text = SumPrice.ToString();
                    txtTotalPrice.Text = SumPrice.ToString();
                    break;
                case Mode.Asn:
                    SelectedAsn.TotalPrice = SumPrice;
                    SelectedAsn.SubtotalPrice = SumPrice;
                    txtSubtotoalPrice.Text = SumPrice.ToString();
                    txtTotalPrice.Text = SumPrice.ToString();
                    break;
            }


        }

        private void BtnRemove_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            RemoveItemsOfPurchaseOrderViewModel = dgItems.SelectedItems.Cast<ItemsOfPurchaseOrderViewModel>().ToList();
            foreach (var VARIABLE in RemoveItemsOfPurchaseOrderViewModel)
            {
                VARIABLE.IsDeleted = true;
                itemsOfPurchaseOrderViewModel.Remove(VARIABLE);
            }
        }


     

        private void BtnOkCharges_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedGrn.TotalPrice = SelectedGrn.SubtotalPrice + SelectedGrn.TotalCharges;
           itemsOfPurchaseOrderViewModel= SelectedGrn.CalculateCost(itemsOfPurchaseOrderViewModel);
          // dgItems.ItemsSource = itemsOfPurchaseOrderViewModel;
            GrdTotalCharges.Visibility = Visibility.Hidden;
        }

        private void btnTotalCharges_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == Mode.Grn)
                GrdTotalCharges.Visibility = Visibility.Visible;
            else
            {
                MessageBox.Show("You can Import Charges when Grn Mode");
            }
        }


        #region TotalCharge

        private void TxtFreight_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
           txtFreight.SelectAll();
        }

        private void TxtDiscountPercent_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtDiscountPercent.SelectAll();
        }

        private void TxtDiscountDollers_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtDiscountDollers.SelectAll();
        }

        private void TxtInsurance_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtInsurance.SelectAll();
        }

        private void TxtCustomsDuty_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtCustomsDuty.SelectAll();
        }

        private void TxtHandling_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtHandling.SelectAll();
        }

        private void TxtForwarding_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtFreight.SelectAll();
        }

        private void TxtLandTransport_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
           txtLandTransport.SelectAll();
        }
        private void TxtOthers_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtOthers.SelectAll();
        }




        private void TxtFreight_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtFreight.SelectAll();
        }

        private void TxtDiscountPercent_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtDiscountPercent.SelectAll();
        }

        private void TxtDiscountDollers_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
           txtDiscountDollers.SelectAll();
        }

        private void TxtInsurance_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtInsurance.SelectAll();
        }

        private void TxtCustomsDuty_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtCustomsDuty.SelectAll();
        }

        private void TxtHandling_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtHandling.SelectAll();
        }

        private void TxtForwarding_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtForwarding.SelectAll();
        }

        private void TxtLandTransport_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtLandTransport.SelectAll();
        }
        private void TxtOthers_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtOthers.SelectAll();
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

        private void TxtFreight_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtDiscountPercent_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtDiscountDollers_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtInsurance_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtCustomsDuty_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtHandling_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtForwarding_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtLandTransport_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtOthers_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        #endregion

        private void GrdProductInformation_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (lblSave.Content.ToString() == "Update")
            {
                SaveAndUpdateProductInformation();
            }
        }
        void SaveAndUpdateProductInformation(bool True = false)
        {
            List<TextBox> list = new List<TextBox>()
            {
                txtStyleNumberProduct,txtSkuProduct,txtUpcProduct,txtColorProduct,txtSizeProduct,txtMadeInProduct
                ,txtAluProduct,txtFobPriceProduct,txtCostProduct,txtWholesaleProduct,txtRetailPriceProduct,txtReceiptPriceProduct
            };

            if (!True)
            {
                foreach (var VARIABLE in list)
                {
                    VARIABLE.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                    MakeReadOnly();
                }
            }
            else
            {
                if (lblSave.Content.ToString() == "Edit")
                {
                    lblSave.Content = "Update";
                    foreach (var VARIABLE in list)
                    {
                        VARIABLE.IsReadOnly = false;
                    }

                    txtAluProduct.IsReadOnly = true;
                }
                else if (lblSave.Content.ToString() == "Update")
                {
                    foreach (var VARIABLE in list)
                    {
                        VARIABLE.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    }

                    _productService.AddOrUpdateProduct(selectedProductMaster);
                    myMessageQueue.Enqueue("Product Updated");
                    MakeReadOnly();
                    lblSave.Content = "Edit";
                }
            }


            void MakeReadOnly()
            {
                foreach (var VARIABLE in list)
                {
                    VARIABLE.IsReadOnly = true;
                }
            }

        }
        private void GroupBoxInventory_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var InventoryList = _productService.GetAllInformationInventoryProduct(selectedProductMaster.Id);
            lblInventoryProduct.Content = InventoryList.ElementAt(0);
            lblMainWarehouseProduct.Content = InventoryList.ElementAt(1);
            lblStore1Product.Content = InventoryList.ElementAt(2);
            lblStore2Product.Content = InventoryList.ElementAt(3);

        }
    }
}
