﻿using System;
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
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Org.BouncyCastle.Crypto.Tls;
using Color = System.Drawing.Color;
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
        private SaleOrderService _saleOrderService;
        private ProductService _productService;
        private PurchaseOrder SelectedPurchaseOrder;
        private PoViewModel SelectedPo;
        private AsnViewModel SelectedAsn;
        private GrnViewModel SelectedGrn;
        private List<Vendor> vendors;
        private List<Warehouse> warehouses;
        private List<Customer> customers;
        private List<Province> provinces;
        private List<TaxRate> taxRates;
        private List<TaxRate> taxCodes;
        private List<Country> countries;
        private List<PaymentMethod> paymentMethods;
        private List<DepositTo> depositTos;
        private List<UOM> uoms;

        public Mode Mode = Mode.Nothong;
        public static User user;
        private List<PurchaseOrder> PurchaseOrdersList;
        private SnackbarMessageQueue myMessageQueue;
        private ucItemCard itemCard;
        private ucPurchasing Purchasing;
        private ucSaleOrder UCSaleOrder;
        private ucCustomerCard UCCustomer;
        private ucVendorCard UCVendorCard;
        private ucPayment UCPayment;
        private ucStockTake UCStockTake;
        private ucInnerMasterCarton UCInnerMasterCarton;


        private List<SalesOrderListview> salesOrderListview;
        private Customer customer;
        private Vendor vendor;
        private DataContextViewModel _dataContextVM;
        public Dashboard()
        {
            InitializeComponent();

            _repositoryService = new RepositoryService();
            _purchaseOrderService = new PerchaseOrderService();
            _userService = new UserService();
            _saleOrderService = new SaleOrderService();
            _productService = new ProductService();
            PurchaseOrdersList = new List<PurchaseOrder>();
            PurchaseOrdersList.AddRange(_repositoryService.AllPurchaseOrder().ToList());

            user = _userService.LoginUser();
            //TestBorder.Child=new ucTest();

            _dataContextVM = new DataContextViewModel();
            _dataContextVM.PurchaseOrder = _repositoryService.AllPurchaseOrder().FirstOrDefault();


            UCCustomer = new ucCustomerCard();
            UCVendorCard = new ucVendorCard();
            itemCard = new ucItemCard();
            Purchasing = new ucPurchasing();
            UCSaleOrder = new ucSaleOrder();
            UCPayment = new ucPayment();
            UCInnerMasterCarton = new ucInnerMasterCarton();

            UCVendorCard.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            itemCard.BtnSaveOnClick += BtnSaveForItem_OnBtnSaveOnClick;
            UCCustomer.BtnSaveOnClick += BtnSaveForCustomer_OnBtnSaveOnClick;
            UCSaleOrder.BtnSaveOnClick += BtnSaveSalesOrder_OnBtnSaveOnClick;
            Purchasing.BtnSaveOnClick += BtnSavePurchasing_OnBtnSaveOnClick;
            Purchasing.BtnAddItemOnClick += BtnAddItem_OnClick;
            Purchasing.BtnImportItem += BtnImportItem_OnClick;
            Purchasing.BtnPostPurchasing += BtnSavePurchasing_OnBtnSaveOnClick;
            Purchasing.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            Purchasing.BtnPrintOrSend += BtnPrintOrSend_OnBtnPrintOrSendOnClick;
            UCSaleOrder.BtnAddItemOnClick += BtnAddItem_OnClick;
            itemCard.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            itemCard.BtnCreateInner += BtnCreateInner_OnClick;
            UCInnerMasterCarton.BtnFindProductForInner += BtnFindProductForInner_OnClick;
            UCInnerMasterCarton.BtnAddInner += BtnAddInner_OnClick;
            UCInnerMasterCarton.BtnAddMaster += BtnAddMaster_OnClick;
            UCInnerMasterCarton.BtnCloseInnerPage += BtnCloseInnerPage_OnClick;
            UCInnerMasterCarton.CheckExistITF14 += CheckExistInnerITF14;
            UCInnerMasterCarton.CheckExistITF14Master += CheckExistMasterITF14;
            UCInnerMasterCarton.BtnAddInnerForMaster += BtnAddInnerForMaster_OnClick;
            UCInnerMasterCarton.SearchITF += BtnSearchITF_OnClick;
            UCInnerMasterCarton.LoadMasterCarton += LoadMasterCaarton_OnClick;
            UCInnerMasterCarton.UpdateMasterCarton += UpdateMasterCaarton_OnClick;
            UCSaleOrder.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            UCSaleOrder.BtnPostSalesOrder += BtnPostSalesOrder_OnBtnCloseSubPageOnClick;
            UCSaleOrder.BtnImportItem += BtnImportItem_OnClick;
            UCCustomer.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            UCVendorCard.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            UCSaleOrder.BtnRecivePayment += BtnbtnRecivePayment_OnbtnRecivePaymentOnClick;
            UCPayment.BtnCloseSecendPage += BtnCloseSecendPage_OnCloseSecendPageOnClick;

            DataContext = _dataContextVM;


            vendors = _repositoryService.AllVendor().ToList();
            warehouses = _repositoryService.AllWarehouse().ToList();
            customers = _repositoryService.AllCustomers().ToList();
            provinces = _repositoryService.AllProvinces().ToList();
            provinces = _repositoryService.AllProvinces().ToList();
            taxRates = _repositoryService.AlltaTaxRates().ToList();
            taxCodes = _repositoryService.AlltaTaxRates().Where(p => p.Code != null && p.TaxRateId == 0).ToList();
            paymentMethods = _repositoryService.AllPaymentMethods().ToList();
            depositTos = _repositoryService.AllDepositTos().ToList();
            uoms = _repositoryService.AllUoms().ToList();
            UCCustomer.cmbCountries.ItemsSource = countries;
            UCCustomer.cmbProvinces.ItemsSource = provinces;

            UCPayment.cmbPaymentMethod.ItemsSource = paymentMethods;
            UCPayment.cmbPaymentMethod.SelectedValue = 1;
            UCPayment.cmbDepositTo.ItemsSource = depositTos;
            UCPayment.cmbDepositTo.SelectedValue = 1;

            UCSaleOrder.CustomersList = customers;
            UCSaleOrder.cmbSalesPerson.ItemsSource = _userService.AllSalesPeople();
            UCSaleOrder.cmbTerms.ItemsSource = _repositoryService.AllTerms().ToList();
            UCSaleOrder.cmbDepositTo.ItemsSource = depositTos;
            UCSaleOrder.cmbPaymentMethod.ItemsSource = paymentMethods;
            itemCard.cmbUOM.ItemsSource = uoms;
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
            lvCustomers.Visibility = Visibility.Hidden;
            lvVendors.Visibility = Visibility.Hidden;
            lvSalesOrder.Visibility = Visibility.Hidden;
        }
        private void HideMenuTop()
        {
            PurchasingMenue.Visibility = Visibility.Hidden;
            SalesMenue.Visibility = Visibility.Hidden;
        }
        private void BtnSaveForCustomer_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            int x = _dataContextVM.Customer.Id;
            string mes = "";
            _repositoryService.AddAndUpdateCustomer(_dataContextVM.Customer);
            mes = (x == 0) ? "Customer Created" : "Customer Updated";
            myMessageQueue.Enqueue(mes);
        }
        private void BtnSaveSalesOrder_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            if (UCSaleOrder.SaleOrderViewModel.IsRefund)
            {
                int IDd = _saleOrderService.AddRefund(UCSaleOrder.SaleOrderViewModel.Refund, UCSaleOrder.SaleOrderViewModel.RefundItems.ToList());
                myMessageQueue.Enqueue("Refund Receipt Created.");
                UCSaleOrder.btnSaveSalesOrder.Visibility = Visibility.Hidden;
            }
            else
            {
                int x = UCSaleOrder.SaleOrderViewModel.Id;
                string mes = "";
                var SalesOrderFinal = UCSaleOrder.SaleOrderViewModel.SoItems.ToList();
                foreach (var VARIABLE in UCSaleOrder.RemoveSoItemViewModel)
                {
                    UCSaleOrder.SaleOrderViewModel.SoItems.Add(VARIABLE);
                }

                int ID = _saleOrderService.SaveAndUpdateSaleOrder(UCSaleOrder.SaleOrderViewModel);

                //mes = (x == 0) ? "Sales Order Created" : "Sales Order Updated";
                UCSaleOrder.SaleOrderViewModel.SoItems = new ObservableCollection<SoItemVeiwModel>(SalesOrderFinal);
                if (x == 0)
                {
                    mes = "Sales Order Created";
                    UCSaleOrder.txtNum.Text = ID.ShowSaleOrderNumber();

                    salesOrderListview.Add(new SalesOrderListview()
                    {
                        No = ID,
                        CustomerName = UCSaleOrder.SaleOrderViewModel.Customer.CompanyName,
                        OpenBalance = 0,
                        TotalBeforeTax = UCSaleOrder.SaleOrderViewModel.Subtotal,
                        Total = UCSaleOrder.SaleOrderViewModel.SoTotalPrice
                    });
                    lvSalesOrder.ItemsSource = salesOrderListview;
                    lvSalesOrder.Items.Refresh();
                    UCSaleOrder.SaleOrderViewModel.Id = ID;
                }
                else if (UCSaleOrder.SaleOrderViewModel.InvoiceNumber != null)
                {
                    mes = "Invoice Updated.";
                }
                else
                {
                    mes = "Sales Order Updated";
                }
                UCSaleOrder.dgSoItems.Items.Refresh();
                UCSaleOrder.RemoveSoItemViewModel.Clear();
                myMessageQueue.Enqueue(mes);
            }

        }

        private void BtnSaveForVendor_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            int x = _dataContextVM.Vendor.Id;
            string mes = "";
            _repositoryService.AddAndUpdateVendor(_dataContextVM.Vendor);
            mes = (x == 0) ? "Vendor Created" : "Vendor Updated";
            myMessageQueue.Enqueue(mes);

        }
        private void BtnSaveForItem_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            _productService.AddOrUpdateProduct(_dataContextVM.ProductMaster);
            myMessageQueue.Enqueue("Product Updated");
        }

        private void BtnSavePurchasing_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            if (Mode == Mode.Asn || Mode == Mode.Grn)
                Purchasing.CalculateCost();

            SaveAndUpdate(Purchasing.IsPosting);
            RefreshLvPurchasing();

        }
        private async Task RefreshLvPurchasing()
        {
            await Task.Run(() =>
            {
                PurchaseOrdersList.Clear();
                PurchaseOrdersList.AddRange(_repositoryService.AllPurchaseOrder().ToList());

                if (Purchasing.IsPosting && Mode == Mode.PO)
                {
                    this.Dispatcher.Invoke(() =>
                            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.PoTotal, InvoiceNumber = p.PoNumber.ShowPoNumber() }).OrderByDescending(p => p.Alu)
                            );
                    this.Dispatcher.Invoke(() => lvPurchasing.Items.Refresh());
                }
                else if (Purchasing.IsPosting && Mode == Mode.Asn)
                {
                    this.Dispatcher.Invoke(() =>
                            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == true && p.CreatedAsn == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.AsnTotal, InvoiceNumber = p.Asnumber.ShowAsnNumber() }).OrderByDescending(p => p.Alu)

                    );
                    this.Dispatcher.Invoke(() => lvPurchasing.Items.Refresh());
                }
                else if (Purchasing.IsPosting && Mode == Mode.Grn)
                {
                    this.Dispatcher.Invoke(() =>
                            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedAsn == true && p.CreatedGrn == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.GrnTotal, InvoiceNumber = p.Grnumber.ShowGrnNumber() }).OrderByDescending(p => p.Alu)

                    );
                    this.Dispatcher.Invoke(() => lvPurchasing.Items.Refresh());
                }
                else
                {

                }
            });

        }
        private void BtnPostSalesOrder_OnBtnCloseSubPageOnClick(object? sender, EventArgs e)
        {
            try
            {
                _saleOrderService.SaveAndUpdateSaleOrder(UCSaleOrder.SaleOrderViewModel);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error 12343");
            }
            int ID = _saleOrderService.CreateInvoice(UCSaleOrder.SaleOrderViewModel.Id);
            UCSaleOrder.txtNum.Text = ID.ShowInvoceNumber();
            UCSaleOrder.txtName.Text = "Invoice";
            myMessageQueue.Enqueue("Invoice Created.");
            DeleteSalesOrderFromList(UCSaleOrder.SaleOrderViewModel.Id);
            // UCSaleOrder.btnSaveSalesOrder.Visibility = Visibility.Hidden;
            UCSaleOrder.btnPostSalesOrder.Visibility = Visibility.Collapsed;

        }

        private async Task DeleteSalesOrderFromList(int Id)
        {
            int IndexOfLvSalesorder = -1;
            await Task.Run(() =>
            {
                IndexOfLvSalesorder = salesOrderListview.ToList().FindIndex(p => p.No == Id);
                salesOrderListview.RemoveAt(IndexOfLvSalesorder);
                this.Dispatcher.Invoke(() => lvSalesOrder.ItemsSource = salesOrderListview);
                this.Dispatcher.Invoke(() => lvSalesOrder.Items.Refresh());
            });

        }
        private void BtnCloseSubPage_OnBtnCloseSubPageOnClick(object? sender, EventArgs e)
        {
            SubPage.Visibility = Visibility.Hidden;

        }
        private void BtnbtnRecivePayment_OnbtnRecivePaymentOnClick(object? sender, EventArgs e)
        {
            UCPayment.txtCusomerEmail.Text = _dataContextVM.SaleOrderViewModel.Customer.Email;
            UCPayment.txtCustomerName.Text = _dataContextVM.SaleOrderViewModel.Customer.FullName;
            UCPayment.txtCompanyName.Text = _dataContextVM.SaleOrderViewModel.Customer.CompanyName;
            UCPayment.InvoiceList = _saleOrderService.GiveMeAllOpenInvoiceForCustomer(_dataContextVM.SaleOrderViewModel.Customer.Id);
            UCPayment.dgInvoicesOpen.ItemsSource = UCPayment.InvoiceList;
            UCPayment.NewPayment.AmountReceived = _dataContextVM.SaleOrderViewModel.SoTotalPrice;



            SecoundBordermanagement.Child = UCPayment;
            SecoundBordermanagement.Visibility = Visibility.Visible;

        }
        private void BtnCloseSecendPage_OnCloseSecendPageOnClick(object? sender, EventArgs e)
        {
            SecoundBordermanagement.Visibility = Visibility.Hidden;

        }
        private void BtnPrintOrSend_OnBtnPrintOrSendOnClick(object? sender, EventArgs e)
        {
            PrintOrSendPurchasing();
            //   PrintOrSendSalesOrder();
        }

        void PrintOrSendSalesOrder()
        {

            var Path = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo newFile = new FileInfo(Path + "ExcelTemplate\\" + "SalesOrderSample.xlsx");
            //FileInfo newFile = new FileInfo(System.IO.Path.Combine(Path, @"\PurchaseOrderSample.xlsx"));
            string filee = Path + "SalesOrders" + @"\" + DateTime.Today.ToShortDateString().Replace("/", "") + ".xlsx";
            FileInfo newFilee = new FileInfo(filee);
            if (newFilee.Exists)
                newFilee.Delete();

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

            //var SalesOrderPrint = _saleOrderService.GiveSaleOrderById(UCSaleOrder.SaleOrderViewModel.Id);
            //int row = 13;
            //switch (UCSaleOrder.Mode)
            //{
            //    case Mode.Sale:
            //    case Mode.SaleOrderPosted:
            //        ws.Cells[8, 2].Value = "Sales Order";
            //        ws.Cells[10, 10].Value = "SO.NO.";
            //        break;
            //    case Mode.Invoice:
            //    case Mode.AsnInvoice:
            //        ws.Cells[8, 2].Value = "Invoice";
            //        ws.Cells[10, 10].Value = "Invoice.NO.";
            //        break;
            //}


            //foreach (var VARIABLE in SalesOrderPrint.SoItems)
            //{

            //    ws.Cells[row, 5, row, 8].Merge = true;
            //    ws.Cells[row, 5].Style.WrapText = true;
            //    ws.Cells[row, 2].Value = VARIABLE.ProductMaster.UPC;
            //    ws.Cells[row, 5].Value = VARIABLE.ProductMaster.Name;
            //    ws.Cells[row, 9].Value = VARIABLE.PoQuantity.ToString();
            //    ws.Cells[row, 10].Value = VARIABLE.PoPrice.ToString();
            //    ws.Cells[row, 11].Value = VARIABLE.PoItemsPrice.ToString();
            //    var CountLine = VARIABLE.ProductMaster.Name.Replace("\r\n", "@").Count(x => x == '@');
            //    ws.Row(row).Height = 20 * (CountLine + 1);
            //    row++;
            //}
            //using (ExcelRange Rng = ws.Cells[row, 1, row, 11])
            //{
            //    //Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            //    //Rng.Style.Border.Top.Color.SetColor(Color.Red);
            //    //Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            //    //Rng.Style.Border.Left.Color.SetColor(Color.Green);
            //    //Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            //    //Rng.Style.Border.Right.Color.SetColor(Color.Green);
            //    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            //    Rng.Style.Border.Bottom.Color.SetColor(Color.DarkGray);
            //}

            //int TotalRow = row + 2;
            //ws.Row(TotalRow).Style.Font.Bold = true;
            //ws.Row(TotalRow).Style.Font.Size = 12;
            //ws.Cells[TotalRow, 8].Value = "Total CAD ";
            //ws.Cells[TotalRow, 9].Value = salesOrderListview..ToString();
            //ws.Cells[TotalRow, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            ////workSheet.Column(1).AutoFit();
            ////workSheet.Column(2).AutoFit();
            ////workSheet.Column(3).AutoFit();


            //FileStream objFileStrm = File.Create(filee);
            //objFileStrm.Close();

            //// Write content to excel file  
            //File.WriteAllBytes(filee, excel.GetAsByteArray());
        }

        void PrintOrSendPurchasing()
        {

            var Path = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo newFile = new FileInfo(Path + "ExcelTemplate\\" + "PurchaseOrderSample.xlsx");
            //FileInfo newFile = new FileInfo(System.IO.Path.Combine(Path, @"\PurchaseOrderSample.xlsx"));
            string filee = Path + "Purchasing" + @"\" + DateTime.Today.ToShortDateString().Replace("/", "") + ".xlsx";
            FileInfo newFilee = new FileInfo(filee);
            if (newFilee.Exists)
                newFilee.Delete();

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

            var PurchasingPrint = _purchaseOrderService.GivePurchaseOrderById(SelectedPo.Id);
            int row = 13;
            switch (Purchasing.Mode)
            {
                case Mode.PO:
                case Mode.POInvoice:
                    ws.Cells[8, 2].Value = "Purchase Order";
                    ws.Cells[10, 10].Value = "PO.NO.";
                    break;
                case Mode.Asn:
                case Mode.AsnInvoice:
                    ws.Cells[8, 2].Value = "Advanced Shipment Notice";
                    ws.Cells[10, 10].Value = "ASN.NO.";
                    break;
                case Mode.Grn:
                case Mode.GrnInvoice:
                    ws.Cells[8, 2].Value = "Goods Receive Note";
                    ws.Cells[10, 10].Value = "GRN.NO.";
                    break;
            }


            foreach (var VARIABLE in PurchasingPrint.Items)
            {

                ws.Cells[row, 5, row, 8].Merge = true;
                ws.Cells[row, 5].Style.WrapText = true;
                ws.Cells[row, 2].Value = VARIABLE.ProductMaster.UPC;
                ws.Cells[row, 5].Value = VARIABLE.ProductMaster.Name;
                ws.Cells[row, 9].Value = VARIABLE.PoQuantity.ToString();
                ws.Cells[row, 10].Value = VARIABLE.PoPrice.ToString();
                ws.Cells[row, 11].Value = VARIABLE.PoItemsPrice.ToString();
                var CountLine = VARIABLE.ProductMaster.Name.Replace("\r\n", "@").Count(x => x == '@');
                ws.Row(row).Height = 20 * (CountLine + 1);
                row++;
            }
            using (ExcelRange Rng = ws.Cells[row, 1, row, 11])
            {
                //Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //Rng.Style.Border.Top.Color.SetColor(Color.Red);
                //Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //Rng.Style.Border.Left.Color.SetColor(Color.Green);
                //Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                //Rng.Style.Border.Right.Color.SetColor(Color.Green);
                Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                Rng.Style.Border.Bottom.Color.SetColor(Color.DarkGray);
            }

            int TotalRow = row + 2;
            ws.Row(TotalRow).Style.Font.Bold = true;
            ws.Row(TotalRow).Style.Font.Size = 12;
            ws.Cells[TotalRow, 8].Value = "Total CAD ";
            ws.Cells[TotalRow, 9].Value = PurchasingPrint.PoTotal.ToString();
            ws.Cells[TotalRow, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

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
            int NumOfPurchase = -1;
            switch (Purchasing.Mode)
            {
                case Mode.PO:
                    SelectedPo.ApproveUser_fk = user.Id;
                    NumOfPurchase = _purchaseOrderService.AddOrUpdatePoViewModel(SelectedPo, SelectedPo.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Asn:
                    SelectedAsn.ApproveUser_fk = user.Id;
                    NumOfPurchase = _purchaseOrderService.AddOrUpdateAsnViewModel(SelectedAsn, SelectedAsn.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Grn:
                    SelectedGrn.ApproveUser_fk = user.Id;
                    NumOfPurchase = _purchaseOrderService.AddOrUpdateGrnViewModel(SelectedGrn, SelectedGrn.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                default:
                    break;
            }

            if (NumOfPurchase != -1)
            {
                switch (Purchasing.Mode)
                {
                    case Mode.PO:
                        if (Purchasing.IsPosting)
                        {
                            Purchasing.btnSavePurchasing.Visibility = Visibility.Hidden;
                            myMessageQueue.Enqueue("Purchase Order Posted.");
                            Purchasing.txtNum.Text = NumOfPurchase.ShowPoNumber();
                        }
                        else
                            myMessageQueue.Enqueue("Purchase Order Saved.");
                        break;
                    case Mode.Asn:
                        if (Purchasing.IsPosting)
                        {
                            Purchasing.btnSavePurchasing.Visibility = Visibility.Hidden;
                            myMessageQueue.Enqueue("Advanced Shipment Notice Posted.");
                            Purchasing.txtNum.Text = NumOfPurchase.ShowAsnNumber();
                        }
                        else
                            myMessageQueue.Enqueue("Advanced Shipment Notice Saved.");

                        break;
                    case Mode.Grn:
                        if (Purchasing.IsPosting)
                        {
                            Purchasing.btnSavePurchasing.Visibility = Visibility.Hidden;
                            myMessageQueue.Enqueue("Goods Receive Note Posted.");
                            Purchasing.txtNum.Text = NumOfPurchase.ShowGrnNumber();
                        }
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


        private void BtnAddItem_OnClick(object? sender, EventArgs e)
        {
            string UPCForSearch = "";

            if (Mode == Mode.Sale || Mode == Mode.Invoice)
            {
                UPCForSearch = UCSaleOrder.txtSearch.Text;
            }
            else
            {
                UPCForSearch = Purchasing.txtSearch.Text;
            }

            AddItem(UPCForSearch,null,null);


        }
        private void BtnImportItem_OnClick(object? sender, EventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Excel |*.xlsx"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {

                FileInfo existingFile = new FileInfo(dlg.FileName);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    List<string> rrr = new List<string>();
                    for (int row = 1; row <= rowCount; row++)
                    {
                        //for (int col = 1; col <= colCount; col++)
                        //{
                        //   rrr.Add(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value.ToString().Trim());

                        //}
                        try
                        {
                            AddItem(worksheet.Cells[row, 1].Value.ToString().Trim(), worksheet.Cells[row, 2].Value.ToString().Trim(), worksheet.Cells[row, 3].Value.ToString().Trim());
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("Row " + row + " is Null");
                        }
                      
                    }
                }
            }

        }

        private void AddItem(string Barcode, [CanBeNull] string Quantity,[CanBeNull] string Rate)
        {
            var SearchMasterProduct = _repositoryService.GiveMeProductMasterByUPC(Barcode);
            if (SearchMasterProduct == null)
            {
                MessageBox.Show("Item Not Found");
            }
            else
            {
                switch (Mode)
                {

                    case Mode.Sale:
                    case Mode.Invoice:
                        SoItemVeiwModel Newitem = new SoItemVeiwModel()
                        {
                            ProductMaster = SearchMasterProduct,
                            ProductMaster_fk = SearchMasterProduct.Id,
                            Discount = 0,
                            Quantity = (Quantity == null) ? 0 : Convert.ToInt32(Quantity.Replace(",", "")),
                            TotalPrice = 0m,
                            Price = (Rate == null) ? SearchMasterProduct.WholesalePrice.Value : Convert.ToDecimal(Rate.Replace(",", "")),
                            Cost = (SearchMasterProduct.Cost == null) ? SearchMasterProduct.FobPrice.Value : SearchMasterProduct.Cost.Value,
                        };

                        UCSaleOrder.SaleOrderViewModel.SoItems.Add(Newitem);
                        UCSaleOrder.AddInventoryInformation(Newitem, _dataContextVM.SaleOrderViewModel.Warehouse_fk.Value);
                        UCSaleOrder.txtSearch.Clear();
                        break;
                    case Mode.PO:
                        SelectedPo.ItemsOfPurchaseOrderViewModels.Add(new ItemsOfPurchaseOrderViewModel()
                        {
                            Price =(Rate==null) ? SearchMasterProduct.FobPrice.Value : Convert.ToDecimal(Rate.Replace(",", "")),
                            Quantity = (Quantity==null)?0: Convert.ToInt32(Quantity.Replace(",","")),
                            ProductMaster = SearchMasterProduct,
                            ProductMaster_fk = SearchMasterProduct.Id
                        });
                        Purchasing.txtSearch.Clear();
                        break;
                    case Mode.Asn:
                        SelectedAsn.ItemsOfPurchaseOrderViewModels.Add(new ItemsOfPurchaseOrderViewModel()
                        {
                            Price = (Rate == null) ? SearchMasterProduct.FobPrice.Value : Convert.ToDecimal(Rate.Replace(",", "")),
                            Quantity = (Quantity == null) ? 0 : Convert.ToInt32(Quantity.Replace(",", "")),
                            ProductMaster = SearchMasterProduct,
                            ProductMaster_fk = SearchMasterProduct.Id
                        });
                        Purchasing.txtSearch.Clear();
                        break;
                    case Mode.Grn:
                    case Mode.POInvoice:
                    case Mode.AsnInvoice:
                    case Mode.GrnInvoice:
                        break;
                    default:
                        break;
                }

            }
        }
        private void BtnCreateInner_OnClick(object? sender, EventArgs e)
        {
            UCInnerMasterCarton.productMaster = _productService.GetProductMasterById(_dataContextVM.ProductMaster.Id);
            UCInnerMasterCarton.DataContext = UCInnerMasterCarton.productMaster;
            UCInnerMasterCarton.ShowInnerPage();
            SecoundBordermanagement.Child = UCInnerMasterCarton;
            SecoundBordermanagement.Visibility = Visibility.Visible;
        }
        private void BtnFindProductForInner_OnClick(object? sender, EventArgs e)
        {
            var tt = _productService.GetProductMasterByUPC(UCInnerMasterCarton.txtProductFind.Text);
            if (tt == null)
            {
                myMessageQueue.Enqueue("Product Not Find");
            }
            else
            {
                UCInnerMasterCarton.productMaster = _productService.GetProductMasterByUPC(UCInnerMasterCarton.txtProductFind.Text);
                UCInnerMasterCarton.DataContext = UCInnerMasterCarton.productMaster;
                UCInnerMasterCarton.innerMasterCartons.Clear();
                UCInnerMasterCarton.dgInnersForMaster.ItemsSource = UCInnerMasterCarton.innerMasterCartons;
                UCInnerMasterCarton.dgInnersForMaster.Items.Refresh();
            }

        }

        private void BtnAddInner_OnClick(object? sender, EventArgs e)
        {
            if (UCInnerMasterCarton.txtInnerQuantity.Text.Length == 0)
            {
                MessageBox.Show("Please Inter Quantity");
                UCInnerMasterCarton.txtInnerQuantity.Focus();
            }
            else
            {
                Inner newInner = new Inner()
                {
                    ITF14 = UCInnerMasterCarton.txtInnerITF14.Text,
                    Quantity = Convert.ToInt16(UCInnerMasterCarton.txtInnerQuantity.Text),
                    ProductMasterFK = UCInnerMasterCarton.productMaster.Id
                };
                _productService.AddInner(newInner);
                UCInnerMasterCarton.productMaster.Inners.Add(newInner);
                UCInnerMasterCarton.dgInners.Items.Refresh();
                myMessageQueue.Enqueue("ITF-14 Inner Added. ");
            }


        }
        private void BtnAddMaster_OnClick(object? sender, EventArgs e)
        {
            List<InnerMasterCarton> list = UCInnerMasterCarton.dgInnersForMaster.Items.OfType<InnerMasterCarton>().ToList();
            //foreach (var item in UCInnerMasterCarton.dgInnersForMaster.Items)
            //{
            //    list.Add(item as InnerMasterCarton);
            //}
            MasterCarton newMasterCarton = new MasterCarton()
            {
                ITF14 = UCInnerMasterCarton.txtMasterITF14.Text,
                InnerMasterCartons = list,
                Lenght = Convert.ToDecimal(UCInnerMasterCarton.txtLengthMaster.Text),
                Width = Convert.ToDecimal(UCInnerMasterCarton.txtWidthMaster.Text),
                Height = Convert.ToDecimal(UCInnerMasterCarton.txtHeightMaster.Text),
                Weight = Convert.ToDecimal(UCInnerMasterCarton.txtWeightMaster.Text),
            };

            _productService.AddMaster(newMasterCarton);
            myMessageQueue.Enqueue("ITF-14 Master Added. ");
            UCInnerMasterCarton.txtAddInnerForMaster.Clear();
            UCInnerMasterCarton.txtMasterITF14.Clear();
            UCInnerMasterCarton.txtWidthMaster.Clear();
            UCInnerMasterCarton.txtWeightMaster.Clear();
            UCInnerMasterCarton.txtLengthMaster.Clear();
            UCInnerMasterCarton.txtHeightMaster.Clear();
            UCInnerMasterCarton.innerMasterCartons.Clear();
            UCInnerMasterCarton.dgInnersForMaster.ItemsSource = null;
            UCInnerMasterCarton.dgInnersForMaster.Items.Refresh();
        }
        private void BtnSearchITF_OnClick(object? sender, EventArgs e)
        {
            Inner t = _productService.GetInnerByITF(UCInnerMasterCarton.txtSearchITF.Text);
            MasterCarton master = new MasterCarton();
            if (t != null)
                UCInnerMasterCarton.ShowInner(t);
            else
            {
                master = _productService.GetMasterByITF(UCInnerMasterCarton.txtSearchITF.Text);
            }

            if (master == null)
            {
                myMessageQueue.Enqueue("ITF14 Not Found.");
            }
            else if (master.Id != 0)
            {
                UCInnerMasterCarton.ShowMaster(master);
            }
            else if (t != null)
            {

            }
            else
            {
                myMessageQueue.Enqueue("ITF14 Not Found.");
            }
            //List<InnerMasterCarton> list = UCInnerMasterCarton.dgInnersForMaster.Items.OfType<InnerMasterCarton>().ToList();
            ////foreach (var item in UCInnerMasterCarton.dgInnersForMaster.Items)
            ////{
            ////    list.Add(item as InnerMasterCarton);
            ////}
            //MasterCarton newMasterCarton = new MasterCarton()
            //{
            //    ITF14 = UCInnerMasterCarton.txtMasterITF14.Text,
            //    InnerMasterCartons = list,
            //    Lenght = Convert.ToDecimal(UCInnerMasterCarton.txtLengthMaster.Text),
            //    Width = Convert.ToDecimal(UCInnerMasterCarton.txtWidthMaster.Text),
            //    Height = Convert.ToDecimal(UCInnerMasterCarton.txtHeightMaster.Text),
            //    Weight = Convert.ToDecimal(UCInnerMasterCarton.txtWeightMaster.Text),
            //};

            //_productService.AddMaster(newMasterCarton);
            //myMessageQueue.Enqueue("ITF-14 Master Added. ");
            //UCInnerMasterCarton.txtAddInnerForMaster.Clear();
            //UCInnerMasterCarton.txtMasterITF14.Clear();
            //UCInnerMasterCarton.txtWidthMaster.Clear();
            //UCInnerMasterCarton.txtWeightMaster.Clear();
            //UCInnerMasterCarton.txtLengthMaster.Clear();
            //UCInnerMasterCarton.txtHeightMaster.Clear();
            //UCInnerMasterCarton.dgInnersForMaster.ItemsSource = null;
            //UCInnerMasterCarton.dgInnersForMaster.Items.Refresh();
        }
        private void LoadMasterCaarton_OnClick(object? sender, EventArgs e)
        {
            var master = _productService.GetMasterByITF(UCInnerMasterCarton.txtMasterITF14.Text);
            if (master != null)
            {
                UCInnerMasterCarton.txtLengthMaster.Text = master.Lenght.ToString();
                UCInnerMasterCarton.txtWidthMaster.Text = master.Width.ToString();
                UCInnerMasterCarton.txtHeightMaster.Text = master.Height.ToString();
                UCInnerMasterCarton.txtWeightMaster.Text = master.Weight.ToString();
                if (UCInnerMasterCarton.btnUpdateMasterCarton.Visibility == Visibility.Hidden)
                {
                    UCInnerMasterCarton.btnUpdateMasterCarton.Visibility = Visibility.Visible;
                    UCInnerMasterCarton.btnUpdateMasterCarton.IsEnabled = true;
                }
                else
                    UCInnerMasterCarton.btnUpdateMasterCarton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("It is Inner's ITF14");
            }
        }
        private void UpdateMasterCaarton_OnClick(object? sender, EventArgs e)
        {
            var master = _productService.GetMasterByITF(UCInnerMasterCarton.txtMasterITF14.Text);
            master.Lenght = Convert.ToDecimal(UCInnerMasterCarton.txtLengthMaster.Text);
            master.Width = Convert.ToDecimal(UCInnerMasterCarton.txtWidthMaster.Text);
            master.Height = Convert.ToDecimal(UCInnerMasterCarton.txtHeightMaster.Text);
            master.Weight = Convert.ToDecimal(UCInnerMasterCarton.txtWeightMaster.Text);
            _productService.UpdateMasterCarton(master);
            UCInnerMasterCarton.btnUpdateMasterCarton.IsEnabled = false;
            myMessageQueue.Enqueue(UCInnerMasterCarton.txtMasterITF14.Text + " updated.");
        }
        private void CheckExistInnerITF14(object? sender, EventArgs e)
        {
            if (UCInnerMasterCarton.txtInnerITF14.Text.Length == 14)
                CheckExistInner(UCInnerMasterCarton.txtInnerITF14.Text);
            else if (UCInnerMasterCarton.txtInnerITF14.Text.Length == 0)
            {

            }
            else
            {
                myMessageQueue.Enqueue("Enter 14");
                UCInnerMasterCarton.txtInnerITF14.Clear();
            }

        }
        private void CheckExistMasterITF14(object? sender, EventArgs e)
        {
            if (UCInnerMasterCarton.txtMasterITF14.Text.Length == 14)
                CheckExistMaster(UCInnerMasterCarton.txtMasterITF14.Text);
            else if (UCInnerMasterCarton.txtMasterITF14.Text.Length == 0)
            {

            }
            else
            {
                myMessageQueue.Enqueue("Enter 14");
                UCInnerMasterCarton.txtMasterITF14.Clear();
            }

        }

        private async Task CheckExistInner(string itf)
        {
            await Task.Run(() =>
            {
                bool IsExistMaster = _productService.CheckMasterCartonITF(itf);
                bool IsExistInner = _productService.CheckInnerITF(itf);

                if (IsExistInner || IsExistMaster)
                {
                    this.Dispatcher.Invoke(() =>
                        UCInnerMasterCarton.imgDuplicateITF.Visibility = Visibility.Visible
                    );
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                        UCInnerMasterCarton.imgApproval.Visibility = Visibility.Visible
                    );
                }
            });

        }
        private async Task CheckExistMaster(string itf)
        {
            await Task.Run(() =>
            {
                bool IsExistMaster = _productService.CheckMasterCartonITF(itf);
                bool IsExistInner = _productService.CheckInnerITF(itf);

                if (IsExistInner || IsExistMaster)
                {
                    this.Dispatcher.Invoke(() =>
                        UCInnerMasterCarton.imgDuplicateITFMaster.Visibility = Visibility.Visible
                    );
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                        UCInnerMasterCarton.imgApprovalMaster.Visibility = Visibility.Visible
                    );
                }
            });

        }
        private void BtnCloseInnerPage_OnClick(object? sender, EventArgs e)
        {
            SecoundBordermanagement.Visibility = Visibility.Hidden;
        }
        private void BtnAddInnerForMaster_OnClick(object? sender, EventArgs e)
        {
            var inner = _productService.GetInnerByITF(UCInnerMasterCarton.txtAddInnerForMaster.Text.Trim());
            if (inner == null)
                myMessageQueue.Enqueue("Inner Not Found.");
            else
            {
                UCInnerMasterCarton.innerMasterCartons.Add(new InnerMasterCarton()
                {
                    Inner = inner,
                    InnerQuntity = 0,
                    InnerFK = inner.Id,
                    Id = UCInnerMasterCarton.innerMasterCartons.Count + 1
                });
                UCInnerMasterCarton.dgInnersForMaster.ItemsSource = UCInnerMasterCarton.innerMasterCartons;
                UCInnerMasterCarton.dgInnersForMaster.Items.Refresh();
                myMessageQueue.Enqueue("Inner Add To List.");
            }
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

        private void CustomerShow(int Id = 0)
        {
            if (Id == 0)
            {
                _dataContextVM.Customer = new Customer()
                {
                    Id = 0,
                    BalanceDueLCY = 0,
                    BalanceLCY = 0,
                    CostsLCY = 0,
                    CreatedBy_fk = 3,
                    CreditLimitLCY = 0,
                    TotalSales = 0,
                    CountryFK = 48,
                    ProvinceFK = 2,
                };
            }
            else
                _dataContextVM.Customer = _repositoryService.GiveMeCustomerById(Id);

            UCCustomer.DataContext = _dataContextVM.Customer;
            Bordermanagement.Child = UCCustomer;
            SubPage.Visibility = Visibility.Visible;
        }
        private void VendorShow(int Id = 0)
        {
            if (Id == 0)
            {
                _dataContextVM.Vendor = new Vendor()
                {
                    Id = 0,
                };
            }
            else
                _dataContextVM.Vendor = _repositoryService.GiveMeVendorById(Id);

            UCVendorCard.DataContext = _dataContextVM.Vendor;
            Bordermanagement.Child = UCVendorCard;
            SubPage.Visibility = Visibility.Visible;
        }
        private void SalesOrderShow(int Id = 0)
        {

            if (Id == 0)
            {
                _dataContextVM.SaleOrderViewModel = new SaleOrderViewModel()
                {
                    Id = 0,
                    TaxArea_fk = 2,
                    TaxArea = provinces.SingleOrDefault(p => p.Id == 2),
                    term_fk = 3,
                    Taxes = new List<Tax>(),
                    ShippingTaxCode = 1,
                    HandlingTaxCode = 1,
                    SoDate = DateTime.Today,
                    Warehouse_fk = 3,
                    Deposit = new Payment() { Id = 0, DepositToFK = 1, PaymentMethodFK = 1, AmountReceived = 0, PaymentInvoices = new List<PaymentInvoice>() }
                };
                _dataContextVM.SaleOrderViewModel.SoItems = new ObservableCollection<SoItemVeiwModel>();
                UCSaleOrder.CollapsedTax(0);
                UCSaleOrder.btnSaveSalesOrder.Visibility = Visibility.Visible;
            }
            else
                _dataContextVM.SaleOrderViewModel = _saleOrderService.GiveSaleOrderById(Id);
            _dataContextVM.SaleOrderViewModel.AllowToCalculate = false;
            _dataContextVM.SaleOrderViewModel.Provinces = provinces;
            _dataContextVM.SaleOrderViewModel.TaxRates = taxRates;
            UCSaleOrder.SaleOrderViewModel = _dataContextVM.SaleOrderViewModel;
            UCSaleOrder.cmbTaxAreaSo.ItemsSource = taxRates;
            UCSaleOrder.cmbTaxShipping.ItemsSource = taxRates;
            UCSaleOrder.cmbTaxShipping.SelectedValue = _dataContextVM.SaleOrderViewModel.ShippingTaxCode;
            UCSaleOrder.cmbTaxHandling.ItemsSource = taxRates;
            UCSaleOrder.cmbTaxHandling.SelectedValue = _dataContextVM.SaleOrderViewModel.HandlingTaxCode;
            UCSaleOrder.CmbTaxCode.ItemsSource = taxCodes;
            UCSaleOrder.cmbDepositTo.ItemsSource = depositTos;
            UCSaleOrder.cmbRefundFrom.ItemsSource = depositTos;
            UCSaleOrder.cmbPaymentMethod.ItemsSource = paymentMethods;
            UCSaleOrder.cmbPaymentMethodRefund.ItemsSource = paymentMethods;
            UCSaleOrder.cmbShipFrom.ItemsSource = warehouses;
            UCSaleOrder.cmbRefundToWarehouse.ItemsSource = warehouses;
            if (_dataContextVM.SaleOrderViewModel.InvoiceNumber == null)
            {
                UCSaleOrder.txtNum.Text = _dataContextVM.SaleOrderViewModel.Id.ShowSaleOrderNumber();
                UCSaleOrder.txtName.Text = "Sales Order";
                UCSaleOrder.btnPostSalesOrder.Visibility = Visibility.Visible;
                UCSaleOrder.btnRecivePayment.Visibility = Visibility.Collapsed;
                UCSaleOrder.btnDeposited.Visibility = Visibility.Collapsed;
                UCSaleOrder.btnSaveSalesOrder.Visibility = Visibility.Visible;
            }
            else
            {
                UCSaleOrder.txtNum.Text = _dataContextVM.SaleOrderViewModel.InvoiceNumber.Value.ShowInvoceNumber();
                UCSaleOrder.txtName.Text = "Invoice";
                UCSaleOrder.btnPostSalesOrder.Visibility = Visibility.Collapsed;
                UCSaleOrder.btnRecivePayment.Visibility = Visibility.Visible;
                if (_dataContextVM.SaleOrderViewModel.Refunds.Count != 0)
                    UCSaleOrder.txtbtnRefund.Text = "Refund (" + _dataContextVM.SaleOrderViewModel.Refunds.Count + ")";
                else
                    UCSaleOrder.txtbtnRefund.Text = "Refund";
            }
            Bordermanagement.Child = UCSaleOrder;
            UCSaleOrder.SaleOrderViewModel.AllowToCalculate = true;
            UCSaleOrder.ShowTaxes();
            UCSaleOrder.GrdRefuand.Visibility = Visibility.Hidden;

            UCSaleOrder.btnSaveSalesOrder.Visibility = Visibility.Visible;
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
            switch (Mode)
            {
                case Mode.PO:
                case Mode.POInvoice:
                case Mode.Asn:
                case Mode.AsnInvoice:
                case Mode.Grn:
                case Mode.GrnInvoice:
                    PurachasingShow();
                    break;
                case Mode.Customer:
                    CustomerShow();
                    break;
                case Mode.Vendor:
                    VendorShow();
                    break;
                case Mode.Sale:
                    SalesOrderShow();
                    break;
            }


        }
        private void BtnPurChaseorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.PoTotal, InvoiceNumber = p.PoNumber.ShowPoNumber() }).OrderByDescending(p => p.Alu);
            Mode = Mode.PO;
            txtMode.Text = "Purchase Order";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnPurchaseorderInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == true & p.PoNumber != -1).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.PoTotal, InvoiceNumber = p.PoNumber.ShowPoNumber() }).OrderByDescending(p => p.Alu);
            Mode = Mode.POInvoice;
            txtMode.Text = "Purchase Order Invoice";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }
        private void BtnGit_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedPO == true && p.CreatedAsn == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.AsnTotal, InvoiceNumber = p.Asnumber.ShowAsnNumber() }).OrderByDescending(p => p.Alu);
            Mode = Mode.Asn;
            txtMode.Text = "Advanced Shipment Notice";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnGitInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedAsn == true).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.AsnTotal, InvoiceNumber = p.Asnumber.ShowAsnNumber() }).OrderByDescending(p => p.Alu);
            Mode = Mode.AsnInvoice;
            txtMode.Text = "ASN Invoice";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnGrn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedAsn == true && p.CreatedGrn == false).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.GrnTotal, InvoiceNumber = p.Grnumber.ShowGrnNumber() }).OrderByDescending(p => p.Alu);
            Mode = Mode.Grn;
            txtMode.Text = "Goods Receive Note";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnGrnInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedGrn == true).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.GrnTotal, InvoiceNumber = p.Grnumber.ShowGrnNumber() }).OrderByDescending(p => p.Alu);
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
            txtMode.Text = "Product Master";
            btnExportInventory.Visibility = Visibility.Visible;
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

            var itemSelected = (ProductMaster)lvProductMaster.ItemContainerGenerator.ItemFromContainer(dep);
            _dataContextVM.ProductMaster = _productService.GetProductMasterById(itemSelected.Id);
            itemCard.DataContext = _dataContextVM.ProductMaster;
            Bordermanagement.Child = itemCard;
            SubPage.Visibility = Visibility.Visible;
        }


        #endregion


        private void BtnSalesOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            salesOrderListview = _saleOrderService.SalesOrdersListView();
            lvSalesOrder.ItemsSource = salesOrderListview;
            Mode = Mode.Sale;
            txtMode.Text = "Sales Orders";
            HideListview();
            lvSalesOrder.Visibility = Visibility.Visible;
        }

        private void LvCustomers_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (CustomerListview)lvCustomers.ItemContainerGenerator.ItemFromContainer(dep);
            CustomerShow(wer.No);
        }

        private void BtnCustomer_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvCustomers.ItemsSource = _repositoryService.AllCustomers().Select(p => new CustomerListview() { No = p.Id, Name = p.CompanyName, LocationCode = p.Province.Name, PhoneNo = p.Phone1, Contact = p.ContactName }).ToList();
            Mode = Mode.Customer;
            txtMode.Text = "Customers";
            HideListview();
            lvCustomers.Visibility = Visibility.Visible;
        }

        private void LvVendors_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (VendorListview)lvVendors.ItemContainerGenerator.ItemFromContainer(dep);
            VendorShow(wer.No);
        }

        private void BtnVendor_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvVendors.ItemsSource = _repositoryService.AllVendor().Select(p => new VendorListview() { No = p.Id, Name = p.DisplayName, PhoneNo = p.Phone1, Contact = p.LastName }).ToList();
            Mode = Mode.Customer;
            txtMode.Text = "Vendors";
            HideListview();
            lvVendors.Visibility = Visibility.Visible;
        }

        private void LvSalesOrder_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (SalesOrderListview)lvSalesOrder.ItemContainerGenerator.ItemFromContainer(dep);
            SalesOrderShow(wer.No);
        }

        private void BtnSalesInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            salesOrderListview = _saleOrderService.SalesInvoceListView();
            lvSalesOrder.ItemsSource = salesOrderListview;
            Mode = Mode.Invoice;
            txtMode.Text = "Sales Invoice";
            HideListview();
            lvSalesOrder.Visibility = Visibility.Visible;
        }

        private void BtnStockTake_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            UCStockTake = new ucStockTake();
            Bordermanagement.Child = UCStockTake;
            SubPage.Visibility = Visibility.Visible;
        }

        private void BtnExportInventory_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Inv "+DateTime.Today.ToShortDateString(); // Default file name
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel |*.xlsx"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            string filename="";
            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
                ExportInv();
                
            }

            void ExportInv()
            {
                var list = _repositoryService.AllProductMasterWithInventoryList().ToList();


                var Path = AppDomain.CurrentDomain.BaseDirectory;
                FileInfo newFile = new FileInfo(Path + "ExcelTemplate\\" + "InvExort.xlsx");

                string filee = filename;
                FileInfo newFilee = new FileInfo(filee);
                try
                {
                    if (newFilee.Exists)
                        newFilee.Delete();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error in Export");
                }

                ExcelPackage excel = new ExcelPackage(newFilee, newFile);

                var wss = excel.Workbook.Worksheets;
                var ws = excel.Workbook.Worksheets.ElementAt(0);
                var ws2 = excel.Workbook.Worksheets.ElementAt(1);
                var ws3 = excel.Workbook.Worksheets.ElementAt(2);
                int i = 2;
                StringBuilder Description;
                ProductInventoryWarehouse OfficItem;
                ProductInventoryWarehouse BenchmarkItem;
                foreach (var item in list)
                {
                    ws.Cells[i, 1].Value = item.Id;
                    ws.Cells[i, 2].Value = item.Name;
                    ws.Cells[i, 3].Value = item.UPC;
                    ws.Cells[i, 4].Value = item.SKU;
                    ws.Cells[i, 5].Value = item.StockOnHand;
                    ws.Cells[i, 6].Value = item.GoodsReserved.ToString();
                    ws.Cells[i, 7].Value = item.Transit.ToString();
                    ws.Cells[i, 8].Value = item.RefundQuantity.ToString();
                    ws.Cells[i, 9].Value = item.Income.ToString();
                    ws.Cells[i, 10].Value = item.Outcome.ToString();
                    ws.Row(i).Height = 66;

                    OfficItem = item.ProductInventoryWarehouses.SingleOrDefault(p => p.Warehouse_fk == 3);
                    ws2.Cells[i, 1].Value = item.Id;
                    ws2.Cells[i, 2].Value = item.Name;
                    ws2.Cells[i, 3].Value = item.UPC;
                    ws2.Cells[i, 4].Value = item.SKU;
                    ws2.Row(i).Height = 66;
                    if (OfficItem!=null)
                    {
                        ws2.Cells[i, 5].Value = OfficItem.Inventory;
                        ws2.Cells[i, 6].Value = OfficItem.OnTheWayInventory.ToString();
                        ws2.Cells[i, 7].Value = OfficItem.RefundQuantity.ToString();
                        ws2.Cells[i, 8].Value = OfficItem.Income.ToString();
                        ws2.Cells[i, 9].Value = OfficItem.OutCome.ToString();
                    }
                    else
                    {
                        ws2.Cells[i, 5].Value = 0;
                        ws2.Cells[i, 6].Value = 0;
                        ws2.Cells[i, 7].Value = 0;
                        ws2.Cells[i, 8].Value = 0;
                        ws2.Cells[i, 9].Value = 0;
                    }

                    BenchmarkItem = item.ProductInventoryWarehouses.SingleOrDefault(p => p.Warehouse_fk == 2);
                    ws3.Cells[i, 1].Value = item.Id;
                    ws3.Cells[i, 2].Value = item.Name;
                    ws3.Cells[i, 3].Value = item.UPC;
                    ws3.Cells[i, 4].Value = item.SKU;
                    ws3.Row(i).Height = 66;
                    if (BenchmarkItem != null)
                    {
                        ws3.Cells[i, 5].Value = BenchmarkItem.Inventory;
                        ws3.Cells[i, 6].Value = BenchmarkItem.OnTheWayInventory.ToString();
                        ws3.Cells[i, 7].Value = BenchmarkItem.RefundQuantity.ToString();
                        ws3.Cells[i, 8].Value = BenchmarkItem.Income.ToString();
                        ws3.Cells[i, 9].Value = BenchmarkItem.OutCome.ToString();
                    }
                    else
                    {
                        ws3.Cells[i, 5].Value = 0;
                        ws3.Cells[i, 6].Value = 0;
                        ws3.Cells[i, 7].Value = 0;
                        ws3.Cells[i, 8].Value = 0;
                        ws3.Cells[i, 9].Value = 0;
                    }     


                    ++i;
                }

                FileStream objFileStrm = File.Create(filee);
                objFileStrm.Close();

                // Write content to excel file  
                File.WriteAllBytes(filee, excel.GetAsByteArray());
            }
            myMessageQueue.Enqueue("Inventory Excel Created.");

        }
    }

    public class DataContextViewModel : DependencyObject
    {
        public Customer Customer { get; set; }

        public Vendor Vendor { get; set; }
        public ProductMaster ProductMaster { get; set; }

        public SaleOrderViewModel SaleOrderViewModel { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
        public PoViewModel PoViewModel { get; set; }
    }
}
