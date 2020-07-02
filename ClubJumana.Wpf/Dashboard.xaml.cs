using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using ClubJumana.Core.Convertors;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.Core.Generator;
using ClubJumana.Core.Services;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Entities.Users;
using ClubJumana.Wpf.UserControls;
using MaterialDesignThemes.Wpf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Path = System.Windows.Shapes.Path;

namespace ClubJumana.Wpf
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private RepositoryService _repositoryService;
        private PerchaseOrderService _purchaseOrderService;
        private UserService _userService;
        private PurchaseOrder SelectedPurchaseOrder;
        private PoViewModel SelectedPo;
        private AsnViewModel SelectedAsn;
        private GrnViewModel SelectedGrn;
        private List<Vendor> vendors;
        private List<Warehouse> warehouses;
        public Mode Mode = Mode.Nothong;
        public static User user;
        private List<PurchaseOrder> PurchaseOrdersList;
        private SnackbarMessageQueue myMessageQueue;
        private ucItemCard itemCard;

        public ucPurchasing Purchasing;
        private Test tt;
        public Dashboard()
        {
            InitializeComponent();

            _repositoryService = new RepositoryService();
            _purchaseOrderService = new PerchaseOrderService();
            _userService = new UserService();
            PurchaseOrdersList = new List<PurchaseOrder>();
            PurchaseOrdersList.AddRange(_repositoryService.AllPurchaseOrder().ToList());

            user = _userService.LoginUser();
            //TestBorder.Child=new ucTest();

            tt = new Test();
            tt.Customer = _repositoryService.AllCustomers().FirstOrDefault();
            tt.Vendor = _repositoryService.AllVendor().FirstOrDefault();
            tt.ProductMaster = _repositoryService.AllProductMasterList().FirstOrDefault();
            tt.PurchaseOrder = _repositoryService.AllPurchaseOrder().FirstOrDefault();

            var customerCard = new ucCustomerCard();
            var vendorCard = new ucVendorCard();
            itemCard = new ucItemCard();
            Purchasing = new ucPurchasing();

            vendorCard.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            itemCard.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            customerCard.BtnSaveOnClick += BtnSaveForCustomer_OnBtnSaveOnClick;
            Purchasing.BtnSaveOnClick += BtnSavePurchasing_OnBtnSaveOnClick;
            Purchasing.BtnAddItemOnClick += BtnAddItem_OnClick;
            Purchasing.BtnPostPurchasing += BtnSavePurchasing_OnBtnSaveOnClick;
            Purchasing.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            Purchasing.BtnPrintOrSend += BtnPrintOrSend_OnBtnPrintOrSendOnClick;
            itemCard.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            DataContext = tt;

            customerCard.DataContext = tt.Customer;
            vendorCard.DataContext = tt.Vendor;
            


            vendors = _repositoryService.AllVendor().ToList();
            warehouses = _repositoryService.AllWarehouse().ToList();


            myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(4000));
            SnackbarResult.MessageQueue = myMessageQueue;
        }

        private void Dashboard_OnLoaded(object sender, RoutedEventArgs e)
        {
            //var convertor = new PurchaseOrderConvertor();
            //// SelectedPurchaseOrder = _purchaseOrderService.GivePurchaseOrderById(9);
            //SelectedPurchaseOrder = new PurchaseOrder() { };
            //SelectedPurchaseOrder.Items = new List<Item>();
            //SelectedPo = convertor.ConvertToPO(SelectedPurchaseOrder, vendors, warehouses);
            //SelectedAsn = convertor.ConvertToAsn(SelectedPurchaseOrder, vendors, warehouses);
            //SelectedGrn = convertor.ConvertToGrn(SelectedPurchaseOrder, vendors, warehouses);

            //Purchasing = new ucPurchasing();
            //Purchasing.BtnSaveOnClick += BtnSavePurchasing_OnBtnSaveOnClick;
            //Purchasing.BtnAddItemOnClick += BtnAddItem_OnClick;
            ////Purchasing.ItemsOfPurchaseOrderViewModels = SelectedGrn.ItemsOfPurchaseOrderViewModels;
            //Purchasing.PoViewModel = SelectedPo;
            //Purchasing.AsnViewModel = SelectedAsn;
            //Purchasing.GrnViewModel = SelectedGrn;
            //Purchasing.Mode = Mode.PO;
            ////Purchasing.DataContext = SelectedGrn;
            //Bordermanagement.Child = Purchasing;


        }

        private void HideListview()
        {
            lvProductMaster.Visibility = Visibility.Hidden;
            lvPurchasing.Visibility = Visibility.Hidden;
        }
        private void HideMenuTop()
        {
            PurchasingMenue.Visibility = Visibility.Hidden;
            SalesMenue.Visibility = Visibility.Hidden;
        }
        private void BtnSaveForCustomer_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            MessageBox.Show(tt.Customer.FirstName);
            _repositoryService.AddAndUpdateCustomer(tt.Customer);
        }

        private void BtnSaveForVendor_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            MessageBox.Show(tt.Customer.FirstName);
            _repositoryService.AddAndUpdateVendor(tt.Vendor);
        }
        private void BtnSaveForItem_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            MessageBox.Show(tt.Customer.FirstName);
            _repositoryService.AddAndUpdateItem(tt.ProductMaster);
        }

        private void BtnSavePurchasing_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            if(Mode==Mode.Asn||Mode==Mode.Grn)
            Purchasing.CalculateCost();

            SaveAndUpdate(Purchasing.IsPosting);

         
        }

        private void BtnCloseSubPage_OnBtnCloseSubPageOnClick(object? sender, EventArgs e)
        {
            SubPage.Visibility = Visibility.Hidden;

        }
        private void BtnPrintOrSend_OnBtnPrintOrSendOnClick(object? sender, EventArgs e)
        {
            PrintOrSend();

        }

        void PrintOrSend()
        {

            var Path = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo newFile = new FileInfo(Path + "ExcelTemplate\\" + "PurchaseOrderSample.xlsx");
            //FileInfo newFile = new FileInfo(System.IO.Path.Combine(Path, @"\PurchaseOrderSample.xlsx"));
            string filee = Path + "Purchasing" + @"\" + DateTime.Today.ToShortDateString().Replace("/", "") + ".xlsx";
            FileInfo newFilee = new FileInfo(filee);

            ExcelPackage excel = new ExcelPackage(newFilee, newFile);

            //Add the Content sheet
            //  var ws = pck.Workbook.Worksheets.Add("Content");

            var ws = excel.Workbook.Worksheets.ElementAt(0);
            // var wss = excel.Workbook.Worksheets("");
            //var workSheet = excel.Workbook.Worksheets.Add("Sheet11");
            //var workSheet2 = excel.Workbook.Worksheets.Add("Sheet12");

            //workSheet.Row(1).Height = 90;
            //workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //workSheet.Row(1).Style.Font.Bold = true;

            // Header of the Excel sheet 
            //workSheet.Cells[1, 1].Value = "Mansour";
            //workSheet.Cells[1, 2].Value = "Id";
            //workSheet.Cells[1, 3].Value = "Name";


            var trr = _purchaseOrderService.GivePurchaseOrderById(SelectedPo.Id);
            int row = 13;

            foreach (var VARIABLE in trr.Items)
            {
                ws.Cells[row, 5, row, 9].Merge = true;
                ws.Cells[row, 5].Style.WrapText = true;
                ws.Cells[row, 2].Value = VARIABLE.ProductMaster.UPC;
                ws.Cells[row, 5].Value = VARIABLE.ProductMaster.Name;
                ws.Cells[row, 9].Value = VARIABLE.PoQuantity;
                ws.Cells[row, 10].Value = VARIABLE.PoPrice;
                ws.Cells[row, 11].Value = VARIABLE.PoItemsPrice;
                row++;
            }

            //workSheet.Column(1).AutoFit();
            //workSheet.Column(2).AutoFit();
            //workSheet.Column(3).AutoFit();

            FileStream objFileStrm = File.Create(filee);
            objFileStrm.Close();

            // Write content to excel file  
            File.WriteAllBytes(filee, excel.GetAsByteArray());
        }

        #region PurchasingMenu
        void SaveAndUpdate(bool done = false)
        {
            string message = "";
            bool IsSuccessSavedOrUpdated = false;
            switch (Purchasing.Mode)
            {
                case Mode.PO:
                    SelectedPo.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdatePoViewModel(SelectedPo, SelectedPo.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Asn:
                    SelectedAsn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdateAsnViewModel(SelectedAsn, SelectedAsn.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Grn:
                    SelectedGrn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdateGrnViewModel(SelectedGrn, SelectedGrn.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                default:
                    break;
            }

            if (IsSuccessSavedOrUpdated)
            {
                UpdatePurchaseList();

                switch (Purchasing.Mode)
                {
                    case Mode.PO:
                        if (Purchasing.IsPosting)
                            myMessageQueue.Enqueue("Purchase Order Posted.");
                        else
                            myMessageQueue.Enqueue("Purchase Order Saved.");
                        break;
                    case Mode.Asn:
                        if (Purchasing.IsPosting)
                            myMessageQueue.Enqueue("Goods In Transit Posted.");
                        else
                            myMessageQueue.Enqueue("Goods In Transit Saved.");

                        break;
                    case Mode.Grn:
                        if (Purchasing.IsPosting)
                            myMessageQueue.Enqueue("Goods Receive Note Posted.");
                        else
                            myMessageQueue.Enqueue("Goods Receive Note Saved");

                        break;
                    default:
                        break;


                }
            }
            else
            {
                MessageBox.Show("Error In Data Entry");
            }

        }

        private async Task UpdatePurchaseList()
        {
            try
            {
                await Task.Run(() =>
                {
                    PurchaseOrdersList.Clear();
                    PurchaseOrdersList.AddRange(_repositoryService.AllPurchaseOrder().ToList());

                });
            }
            catch (Exception e)
            {
                MessageBox.Show("Error In upload Image");
            }

        }
        private void BtnAddItem_OnClick(object? sender, EventArgs e)
        {
            var ttttr = Purchasing.txtSearch.Text;
            var SearchMasterProduct = _repositoryService.GiveMeProductMasterByUPC(ttttr);
            if (SearchMasterProduct == null)
            {
                MessageBox.Show("Item Not Found");
            }
            else
            {


                switch (Purchasing.Mode)
                {
                    case Mode.PO:
                        SelectedPo.ItemsOfPurchaseOrderViewModels.Add(new ItemsOfPurchaseOrderViewModel()
                        {
                            Price = SearchMasterProduct.FobPrice.Value,
                            ProductMaster = SearchMasterProduct,
                            ProductMaster_fk = SearchMasterProduct.Id
                        });
                        break;
                    case Mode.Asn:
                        SelectedAsn.ItemsOfPurchaseOrderViewModels.Add(new ItemsOfPurchaseOrderViewModel()
                        {
                            Price = SearchMasterProduct.FobPrice.Value,
                            ProductMaster = SearchMasterProduct,
                            ProductMaster_fk = SearchMasterProduct.Id
                        });
                        break;
                    case Mode.Grn:
                        break;
                    default:
                        break;
                }

            }

            Purchasing.txtSearch.Clear();
        }
        private void LvPurchasing_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (PurchasingListview)lvPurchasing.ItemContainerGenerator.ItemFromContainer(dep);

            PurachasingShow(wer.Alu);

        }

        private void PurachasingShow(int Id = 0)
        {
            var convertor = new PurchaseOrderConvertor();
            if (Id == 0)
            {
                SelectedPurchaseOrder = new PurchaseOrder() { };
                SelectedPurchaseOrder.Items = new List<Item>();
            }
            else
                SelectedPurchaseOrder = _purchaseOrderService.GivePurchaseOrderById(Id);

            SelectedPo = convertor.ConvertToPO(SelectedPurchaseOrder, vendors, warehouses);
            SelectedAsn = convertor.ConvertToAsn(SelectedPurchaseOrder, vendors, warehouses);
            SelectedGrn = convertor.ConvertToGrn(SelectedPurchaseOrder, vendors, warehouses);

            //Purchasing.ItemsOfPurchaseOrderViewModels = SelectedGrn.ItemsOfPurchaseOrderViewModels;
            Purchasing.PoViewModel = SelectedPo;
            Purchasing.AsnViewModel = SelectedAsn;
            Purchasing.GrnViewModel = SelectedGrn;
            Purchasing.Mode = Mode;
            Purchasing.ParaperForNewPurchasing();
            //Purchasing.DataContext = SelectedGrn;
            Bordermanagement.Child = Purchasing;
            Purchasing.HideOrShowChargesInPurchasing();
            SubPage.Visibility = Visibility.Visible;
        }


        private void BtnPurchasing_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideMenuTop();
            PurchasingMenue.Visibility = Visibility.Visible;
        }

        private void PurchasingMenue_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PurchasingMenue.Visibility = Visibility.Hidden;
        }

        private void BtnNewPurchasing_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            PurachasingShow();
        }
        private void BtnPurChaseorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.PoTotal, InvoiceNumber = p.PoNumber.ShowPoNumber() });
            Mode = Mode.PO;
            txtMode.Text = "Purchase Order";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnPurchaseorderInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == true & p.PoNumber != -1).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.PoTotal, InvoiceNumber = p.PoNumber.ShowPoNumber() });
            Mode = Mode.POInvoice;
            txtMode.Text = "Purchase Order Invoice";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }
        private void BtnGit_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == true && p.CreatedAsn == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.AsnTotal, InvoiceNumber = p.Asnumber.ShowAsnNumber() });
            Mode = Mode.Asn;
            txtMode.Text = "Goods In Transit";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnGitInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedAsn == true).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.AsnTotal, InvoiceNumber = p.Asnumber.ShowAsnNumber() });
            Mode = Mode.AsnInvoice;
            txtMode.Text = "GIT Invoice";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnGrn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedAsn == true && p.CreatedGrn == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.GrnTotal, InvoiceNumber = p.Grnumber.ShowGrnNumber() });
            Mode = Mode.Grn;
            txtMode.Text = "Goods Receive Note";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnGrnInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedGrn == true).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.GrnTotal, InvoiceNumber = p.Grnumber.ShowGrnNumber() });
            Mode = Mode.GrnInvoice;
            txtMode.Text = "Grn Invoice";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        #endregion



        #region SalesMenu
        private void BtnSales_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideMenuTop();
            SalesMenue.Visibility = Visibility.Visible;
        }
        private void SalesMenue_OnMouseLeave(object sender, MouseEventArgs e)
        {
            SalesMenue.Visibility = Visibility.Hidden;
        }
        private void BtnItem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideListview();
            lvProductMaster.Visibility = Visibility.Visible;
            lvProductMaster.ItemsSource = _repositoryService.AllProductMasterList().ToList();

        }


        private void LvProductMaster_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (ProductMaster)lvProductMaster.ItemContainerGenerator.ItemFromContainer(dep);

            itemCard.DataContext = wer;

            Bordermanagement.Child = itemCard;
            SubPage.Visibility = Visibility.Visible;
        }


        #endregion


    }

    public class Test
    {
        public Customer Customer { get; set; }
        public Vendor Vendor { get; set; }
        public ProductMaster ProductMaster { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public PoViewModel PoViewModel { get; set; }
    }
}
