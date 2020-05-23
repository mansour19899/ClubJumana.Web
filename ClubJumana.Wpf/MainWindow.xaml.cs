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
        public List<SoItemVeiwModel> RemoveSoItemVeiwModels;
        private bool ModeAddItem = false;
        private bool IsViewDetail = false;
        private ObservableCollection<SoItemVeiwModel> listSoItems;
        private SaleOrderViewModel saleOrder;
        private List<Customer> CustomersList;
        private ObservableCollection<RefundItemsViewModel> refundItemsList;
        private Refund newRefund;

        private PerchaseOrderService _purchaseOrderService;
        private RepositoryService _repositoryService;
        private UserService _userService;
        private ProductService _productService;
        private SaleOrderService _saleOrderService;
        private PurchaseOrderConvertor convertor;
        public MainWindow()
        {
            InitializeComponent();
            _purchaseOrderService = new PerchaseOrderService();
            _repositoryService = new RepositoryService();
            _userService = new UserService();
            _productService = new ProductService();
            _saleOrderService = new SaleOrderService();
            RemoveItemsOfPurchaseOrderViewModel = new List<ItemsOfPurchaseOrderViewModel>();
            RemoveSoItemVeiwModels = new List<SoItemVeiwModel>();
            vendors = _repositoryService.AllVendor().ToList();
            warehouses = _repositoryService.AllWarehouse().ToList();
            user = _userService.LoginUser();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            lvProducts.ItemsSource = _repositoryService.AllProductList().ToList();
            itemsOfPurchaseOrderViewModel = new ObservableCollection<ItemsOfPurchaseOrderViewModel>();
            dgItems.ItemsSource = itemsOfPurchaseOrderViewModel;

            CustomersList = _repositoryService.AllCustomers().ToList();
            lvCustomerLookup.ItemsSource = CustomersList;


            myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(4000));
            SnackbarResult.MessageQueue = myMessageQueue;

        }

        void HidePanel()
        {
            GrdNewPurchersOrder.Visibility = Visibility.Hidden;
            //GrdSale.Visibility = Visibility.Hidden;
            GrdPurchesOrderList.Visibility = Visibility.Hidden;
            GrdProductList.Visibility = Visibility.Hidden;
            GrdTotalCharges.Visibility = Visibility.Hidden;
            GrdProductInformation.Visibility = Visibility.Hidden;
            GrdSaleOrder.Visibility = Visibility.Hidden;
            GrdOrder.Visibility = Visibility.Hidden;


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
            _saleOrderService = new SaleOrderService();
            saleOrder = new SaleOrderViewModel();
            listSoItems = new ObservableCollection<SoItemVeiwModel>();
            dgSoItems.ItemsSource = listSoItems;
            dgSoItems.IsReadOnly = false;
            saleOrder.TaxArea_fk = 2;
            saleOrder.SoItems = listSoItems;
            saleOrder.User = _userService.LoginUser();
            saleOrder.IsSaveDatabase = false;
            saleOrder.Warehouse = _repositoryService.AllWarehouse().ToList().Skip(1).FirstOrDefault();
            saleOrder.OrderedDate = DateTime.Today;
            this.DataContext = saleOrder;
            lblSave.Content = "Save";
            lblDone.Content = "Create Invoice";
            Mode = Mode.Sale;
            List<Province> pro = _repositoryService.AllProvinces().ToList();
            cmbTaxAreaSo.ItemsSource = pro;
            cmbTaxAreaSo.SelectedIndex = 1;
            PrepareTaxitem(pro.ElementAt(1));
            HidePanel();
            ShowSideBar(Mode.Sale);
            GrdSaleOrder.Visibility = Visibility.Visible;
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
            HidePanel();
            Mode = Mode.Order;
            ShowSideBar(Mode);
            lvOrders.ItemsSource = _repositoryService.AllOrders().ToList();
            List<Province> pro = _repositoryService.AllProvinces().ToList();
            cmbTaxAreaSo.ItemsSource = pro;
            lblSave.Content = "Edit";
            lblDone.Content = "Create Invoice";
            GrdOrder.Visibility = Visibility.Visible;
        }

        private void BtnSave_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == Mode.productInformation)
            {
                SaveAndUpdateProductInformation(true);
            }
            else if (Mode == Mode.Sale)
            {
                _saleOrderService.SaveAndUpdateSaleOrder(saleOrder);
            }
            else if (Mode == Mode.Order)
            {
                if (lblSave.Content == "Update")
                {
                    btnAddItem.Visibility = Visibility.Collapsed;
                    btnRemove.Visibility = Visibility.Collapsed;
                    dgSoItems.IsReadOnly = true;
                    lblSave.Content = "Edit";
                    saleOrder.SoItems = listSoItems.Concat(RemoveSoItemVeiwModels).ToList();
                    _saleOrderService.SaveAndUpdateSaleOrder(saleOrder);
                }
                else
                {
                    dgSoItems.IsReadOnly = false;
                    lblSave.Content = "Update";
                    btnAddItem.Visibility = Visibility.Visible;
                    btnRemove.Visibility = Visibility.Visible;
                }

            }
            else
            {
                SaveAndUpdate();

            }



        }
        private void BtnAddItem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ModeAddItem = true;
            lvProducts.ItemsSource = _repositoryService.AllProductList().ToList();
            HidePanel();
            GrdProductList.Visibility = Visibility.Visible;

        }
        private void BtnRemove_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            switch (Mode)
            {
                case Mode.PO:
                case Mode.Asn:
                case Mode.Grn:
                    RemoveItemsOfPurchaseOrderViewModel.AddRange(dgItems.SelectedItems.Cast<ItemsOfPurchaseOrderViewModel>().ToList());
                    foreach (var VARIABLE in RemoveItemsOfPurchaseOrderViewModel)
                    {
                        VARIABLE.IsDeleted = true;
                        itemsOfPurchaseOrderViewModel.Remove(VARIABLE);
                    }
                    break;
                case Mode.Order:
                    RemoveSoItemVeiwModels.AddRange(dgSoItems.SelectedItems.Cast<SoItemVeiwModel>().ToList());
                    foreach (SoItemVeiwModel model in RemoveSoItemVeiwModels)
                    {
                        model.IsDeleted = true;
                        listSoItems.Remove(model);
                    }
                    break;
            }


        }


        private void BtnDone_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (Mode)
            {
                case Mode.Sale:
                case Mode.Order:
                    _saleOrderService.CreateInvoice(saleOrder);
                    lblPrint.Content = "Print Invoice";
                    lblSendEmail.Content = "Send Invoice";
                    MessageBox.Show("Invoice Created");
                    break;
                case Mode.PO:
                case Mode.Asn:
                case Mode.Grn:
                    SaveAndUpdate(true);
                    break;
                default:
                    break;
            }

        }

        private void BtnSendEmail_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == Mode.Order || Mode == Mode.Sale)
                _saleOrderService.SendEmailOrPrint(saleOrder);
            MessageBox.Show("Email Sended");
        }

        private void BtnPrint_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == Mode.Order || Mode == Mode.Sale)
                _saleOrderService.SendEmailOrPrint(saleOrder, true);
            MessageBox.Show("Printing");

        }
        private void BtnRefund_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var SelectedForRefund = dgSoItems.SelectedItems.Cast<SoItemVeiwModel>().ToList();
            newRefund=new Refund();
            refundItemsList=new ObservableCollection<RefundItemsViewModel>();

            foreach (var model in SelectedForRefund)
            {
                refundItemsList.Add(new RefundItemsViewModel()
                {
                    StyleNumber = model.ProductMaster.StyleNumber,
                    UPC = model.ProductMaster.UPC,
                    ProductMaster_fk = model.ProductMaster_fk,
                    AbleReturn = model.Quantity- model.QuantityRefunded,
                    Cost = model.Cost,
                    Price = model.Price
                } );
            }

            dgRefundItem.ItemsSource = refundItemsList;
            
            GrdTotalCharges.Visibility = Visibility.Visible;
        }

        void SaveAndUpdate(bool done = false)
        {
            string message = "";
            bool IsSuccessSavedOrUpdated = false;
            switch (Mode)
            {
                case Mode.PO:
                    SelectedPo.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdatePoViewModel(SelectedPo, itemsOfPurchaseOrderViewModel.Concat(RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Asn:
                    SelectedAsn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdateAsnViewModel(SelectedAsn, itemsOfPurchaseOrderViewModel.Concat(RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Grn:
                    SelectedGrn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdateGrnViewModel(SelectedGrn, itemsOfPurchaseOrderViewModel.Concat(RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
            }

            if (IsSuccessSavedOrUpdated)
            {
                if (done)
                {
                    message = "Purchase Order Done";
                }
                else if (SelectedAsn != null)
                {

                    if (SelectedAsn.Id == 0)
                        message = "Goods Transit Saved";

                }
                else if (SelectedPo != null)
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
                    decimal price = 0;
                    price = (rdoWholesale.IsChecked == true) ? wer.WholesalePrice.Value : wer.RetailPrice.Value;
                    if (!listSoItems.Any(p => p.ProductMaster_fk == wer.Id))
                        listSoItems.Add(new SoItemVeiwModel()
                        {
                            ProductMaster = wer,
                            ProductMaster_fk = wer.Id,
                            Quantity = 0,
                            TotalPrice = 0m
                            ,
                            Price = price,
                            Cost = wer.Cost.Value
                        });

                    HidePanel();
                    GrdSaleOrder.Visibility = Visibility.Visible;
                }
                else
                {
                    if (!itemsOfPurchaseOrderViewModel.Any(p => p.ProductMaster_fk == wer.Id))
                    {
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
                    }


                    //FillDataGrid(itemsList);
                    HidePanel();
                    GrdNewPurchersOrder.Visibility = Visibility.Visible;
                    //StepAddItem++;
                }


                ModeAddItem = false;

            }
            else
            {
                //Edit The Code Later
                selectedProductMaster = new ProductMaster()
                {
                    Id = wer.Id,
                    Name = wer.Name,
                    VendorCode = wer.VendorCode,
                    StyleNumber = wer.StyleNumber,
                    SKU = wer.SKU,
                    UPC = wer.UPC,
                    Size = wer.Size,
                    Color = wer.Color,
                    MadeIn = wer.MadeIn,
                    Cost = wer.Cost,
                    FobPrice = wer.FobPrice,
                    RetailPrice = wer.RetailPrice,
                    WholesalePrice = wer.WholesalePrice,
                    SalePrice = wer.SalePrice,
                    SaleStartDate = wer.SaleStartDate,
                    SaleEndDate = wer.SaleEndDate,
                    Margin = wer.Margin,
                    LastUpdateInventory = wer.LastUpdateInventory,
                    Income = wer.Income,
                    Outcome = wer.Outcome,
                    Image = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\ProductImages\\" + wer.Image,
                    Active = wer.Active,
                    Note = wer.Note,
                    GoodsReserved = wer.GoodsReserved,
                    StockOnHand = wer.StockOnHand,
                };
                this.DataContext = selectedProductMaster;
                GroupBoxInventory_OnMouseDown(null, null);
                lblSave.Content = "Edit";
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

        private void BtnOkCharges_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedGrn.TotalPrice = SelectedGrn.SubtotalPrice + SelectedGrn.TotalCharges;
            itemsOfPurchaseOrderViewModel = SelectedGrn.CalculateCost(itemsOfPurchaseOrderViewModel);
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

            lblStockOnHand.Content = InventoryList["StockOnHand"];
            lblReserved.Content = InventoryList["Reserved"];
            lblTempBalance.Content = InventoryList["TempBalance"];
            lblSaleProduct.Content = InventoryList["Sale"];
            lblMainWarehouseProduct.Content = InventoryList["MainWarehouse"];
            lblStore1Product.Content = InventoryList["Store1"];
            lblStore2Product.Content = InventoryList["Store2"];

        }

        #region SaleOrder

        private void PrepareTaxitem(Province province)
        {
            saleOrder.TaxName = "";
            var taxrate = new List<decimal>();
            if (province != null)
            {
                if (province.HST != 0 && province.HST != null)
                {
                    saleOrder.TaxName = saleOrder.TaxName + "HST";
                    taxrate.Add(province.HST.Value);
                    cmbTaxAreaSo.ToolTip = saleOrder.TaxName + " : " + (province.HST.Value * 100).ToString() + " %";
                }

                if (province.GST != 0 && province.GST != null)
                {
                    saleOrder.TaxName = saleOrder.TaxName + "GST";
                    taxrate.Add(province.GST.Value);
                    cmbTaxAreaSo.ToolTip = saleOrder.TaxName + " : " + (province.GST.Value * 100).ToString() + " %";
                }

                if (province.QST != 0 && province.QST != null)
                {
                    saleOrder.TaxName = saleOrder.TaxName + ",QST";
                    taxrate.Add(province.QST.Value);
                    cmbTaxAreaSo.ToolTip = saleOrder.TaxName + " : " + (province.GST.Value * 100).ToString() + " %  ," + (province.QST.Value * 100).ToString() + " %";
                }
                saleOrder.TaxRate = taxrate;
            }


        }

        private void SetTextboxNumeric(TextCompositionEventArgs e, TextBox txt)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
                e.Handled = true;
            if (txt.Text.Count(x => x == '.') > 0)
            {
                if (e.Text == ".")
                    e.Handled = true;
                else if (txt.Text.Split('.')[1].Length > 3)
                {
                    e.Handled = true;
                }
            }
        }
        private void ConvertRetailWholesale(bool retail = false)
        {
            if (listSoItems != null)
            {
                saleOrder.Subtotal = 0;
                saleOrder.SoTotalPrice = 0;
                saleOrder.TotalDiscount = 0;
                if (retail)
                {
                    foreach (var VARIABLE in listSoItems)
                    {
                        VARIABLE.Price = VARIABLE.ProductMaster.RetailPrice.Value;
                        saleOrder.Subtotal = VARIABLE.TotalPrice + saleOrder.Subtotal;
                        saleOrder.TotalDiscount = (VARIABLE.Quantity * VARIABLE.Price - VARIABLE.TotalPrice) + saleOrder.TotalDiscount;
                    }
                }
                else
                {
                    foreach (var VARIABLE in listSoItems)
                    {
                        VARIABLE.Price = VARIABLE.ProductMaster.WholesalePrice.Value;
                        saleOrder.Subtotal = VARIABLE.TotalPrice + saleOrder.Subtotal;
                        saleOrder.TotalDiscount = (VARIABLE.Quantity * VARIABLE.Price - VARIABLE.TotalPrice) + saleOrder.TotalDiscount;
                    }
                }

            }
        }

        private void TxtCustomerLookup_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var resalt = CustomersList.Where(p => p.FirstName.ToLower().Contains(txtCustomerLookup.Text.ToLower())
                                                  || p.LastName.ToLower().Contains(txtCustomerLookup.Text.ToLower())
                                                  || p.Phone1.ToLower().Contains(txtCustomerLookup.Text.ToLower()));
            if (resalt.Count() == 0)
                lvCustomerLookup.ItemsSource = new List<Customer>() { new Customer() { FirstName = "Not Find Any Customer" } };
            else
                lvCustomerLookup.ItemsSource = resalt;

            lvCustomerLookupBorder.Visibility = Visibility.Visible;
        }

        private void TxtCustomerLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                txtCustomerLookup.Text = "";
                lvCustomerLookupBorder.Visibility = Visibility.Hidden;
                dpiShipDateSo.Focus();
            }
        }

        private void LvCustomerLookupBorder_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (lvCustomerLookup.SelectedIndex != -1)
            {
                saleOrder.Customer = lvCustomerLookup.SelectedItem as Customer;
            }
            else
            {
                saleOrder.Customer = lvCustomerLookup.ItemsSource.Cast<Customer>().FirstOrDefault();
            }
            txtCustomerLookup.Text = "";
            lvCustomerLookupBorder.Visibility = Visibility.Hidden;
            dpiShipDateSo.Focus();
        }
        private void LvCustomerLookup_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (Customer)lvCustomerLookup.ItemContainerGenerator.ItemFromContainer(dep);
            saleOrder.Customer = wer;
            txtCustomerLookup.Text = "";
            lvCustomerLookupBorder.Visibility = Visibility.Hidden;
            dpiShipDateSo.Focus();
        }

        private void RdoWholesale_OnChecked(object sender, RoutedEventArgs e)
        {
            ConvertRetailWholesale();
        }

        private void RdoRetail_OnChecked(object sender, RoutedEventArgs e)
        {
            ConvertRetailWholesale(true);
        }

        private void DgSoItems_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            DataGridColumn col1 = e.Column;
            DataGridRow row1 = e.Row;
            // SoItem t = e.Row.DataContext as SoItem;
            string header = col1.Header as string;

            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            int col_index = col1.DisplayIndex;

            switch (header)
            {
                case "Style Number":
                    break;
                case "Quantity":
                    var qytStr = ((TextBox)e.EditingElement).Text;
                    if (qytStr.Count(x => x == '.') > 0)
                    {
                        MessageBox.Show("Enter Integer Number Please");
                        listSoItems.ElementAt(row_index).Quantity = 0;
                        break;
                    }
                    int Qyt = (!string.IsNullOrWhiteSpace(qytStr)) ? Convert.ToInt32(qytStr) : 0;
                    if (Qyt < 0)
                        Qyt = Qyt * -1;
                    listSoItems.ElementAt(row_index).Quantity = Qyt;
                    break;
                case "Discount":
                    if (listSoItems.ElementAt(row_index).Quantity == 0)
                    {
                        MessageBox.Show("First Input Quantity");
                        listSoItems.ElementAt(row_index).Discount = 0;
                    }
                    else
                    {
                        var discountStr = (((TextBox)e.EditingElement).Text).Replace("%", "");
                        if (discountStr.Count(x => x == '.') > 1)
                        {
                            MessageBox.Show("Enter Correct Number");
                            listSoItems.ElementAt(row_index).Discount = 0;
                            break;
                        }
                        else
                        {
                            decimal Discount = (!string.IsNullOrWhiteSpace(discountStr)) ? Convert.ToDecimal(discountStr) : 0m;
                            listSoItems.ElementAt(row_index).Discount = Discount;
                        }

                    }
                    break;

            }

            saleOrder.Subtotal = 0;
            saleOrder.SoTotalPrice = 0;
            saleOrder.TotalDiscount = 0;
            foreach (var item in listSoItems)
            {
                saleOrder.Subtotal = item.TotalPrice + saleOrder.Subtotal;
                saleOrder.TotalDiscount = (item.Quantity * item.Price - item.TotalPrice) + saleOrder.TotalDiscount;
            }
        }

        private void DgSoItems_OnKeyDown(object sender, KeyEventArgs e)
        {
            bool result = false;

            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal) || (e.Key == Key.Tab))
            { result = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.OemPeriod))
            { result = false; }
            else
            { result = true; }

            e.Handled = result;
        }

        private void TxtFreightSo_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SetTextboxNumeric(e, txtFreightSo);
        }

        private void TxtFreightSo_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtFreightSo.SelectAll();
        }

        private void TxtFreightSo_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtFreightSo.SelectAll();
        }

        private void TxtHandlingSo_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SetTextboxNumeric(e, txtHandlingSo);
        }

        private void TxtHandlingSo_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            txtHandlingSo.SelectAll();
        }

        private void TxtHandlingSo_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtHandlingSo.SelectAll();
        }

        private void CmbTaxAreaSo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PrepareTaxitem(cmbTaxAreaSo.SelectedItem as Province);
        }


        #endregion


        private void LvOrders_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (SaleOrder)lvOrders.ItemContainerGenerator.ItemFromContainer(dep);

            saleOrder = _saleOrderService.GiveSaleOrderById(wer.Id);
            dgSoItems.IsReadOnly = true;
            listSoItems = new ObservableCollection<SoItemVeiwModel>(saleOrder.SoItems);
            cmbTaxAreaSo.SelectedValue = saleOrder.TaxArea.Id;
            PrepareTaxitem((Province)cmbTaxAreaSo.SelectedItem);
            dgSoItems.ItemsSource = listSoItems;
            this.DataContext = saleOrder;
            HidePanel();
            ShowSideBar(Mode.Order);
            GrdSaleOrder.Visibility = Visibility.Visible;
        }


        private void BtnCancelRefund_OnClick(object sender, RoutedEventArgs e)
        {
            GrdTotalCharges.Visibility = Visibility.Hidden;
        }

        private void BtnSubmitRefund_OnClick(object sender, RoutedEventArgs e)
        {

          
            newRefund.SaleOrder_fk = saleOrder.Id;
            newRefund.RefundTotalPrice = 0;
            newRefund.Tax = 0;
            newRefund.SubtotalPrice = 0;
            //Edit Code Later
            newRefund.WarehouseId = 2;
            newRefund.RefundItems = _saleOrderService.CovertToRefundItem(refundItemsList);
            _saleOrderService.AddRefund(newRefund);
            MessageBox.Show("salam");
        }

        private void DgRefundItem_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            decimal sum = 0;
            foreach (var model in refundItemsList)
            {
                sum += model.TotalPrice;
            }

            newRefund.SubtotalPrice = sum;
            lblSubtotalRefund.Text ="Subtotal : " +sum.ToString();
            lblTaxRefund.Text = "Tax : " + newRefund.Tax.ToString();
            lblTotalPriceRefund.Text = "Total Price : " + newRefund.RefundTotalPrice.ToString();


        }
    }
}
