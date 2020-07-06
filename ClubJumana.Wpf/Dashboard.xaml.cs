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
using MaterialDesignThemes.Wpf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
        private PurchaseOrder SelectedPurchaseOrder;
        private PoViewModel SelectedPo;
        private AsnViewModel SelectedAsn;
        private GrnViewModel SelectedGrn;
        private List<Vendor> vendors;
        private List<Warehouse> warehouses;
        private List<Customer> customers;
        private List<Province> provinces;

        public Mode Mode = Mode.Nothong;
        public static User user;
        private List<PurchaseOrder> PurchaseOrdersList;
        private SnackbarMessageQueue myMessageQueue;
        private ucItemCard itemCard;
        private ucPurchasing Purchasing;
        private ucSaleOrder UCSaleOrder;

        private SaleOrderViewModel saleOrderViewModel;
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
            UCSaleOrder = new ucSaleOrder();

            vendorCard.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            itemCard.BtnSaveOnClick += BtnSaveForItem_OnBtnSaveOnClick;
            customerCard.BtnSaveOnClick += BtnSaveForCustomer_OnBtnSaveOnClick;
            Purchasing.BtnSaveOnClick += BtnSavePurchasing_OnBtnSaveOnClick;
            Purchasing.BtnAddItemOnClick += BtnAddItem_OnClick;
            Purchasing.BtnPostPurchasing += BtnSavePurchasing_OnBtnSaveOnClick;
            Purchasing.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            Purchasing.BtnPrintOrSend += BtnPrintOrSend_OnBtnPrintOrSendOnClick;
            UCSaleOrder.BtnAddItemOnClick += BtnAddItem_OnClick;
            itemCard.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            UCSaleOrder.BtnCloseSubPage += BtnCloseSubPage_OnBtnCloseSubPageOnClick;
            DataContext = tt;

            customerCard.DataContext = tt.Customer;
            vendorCard.DataContext = tt.Vendor;







            vendors = _repositoryService.AllVendor().ToList();
            warehouses = _repositoryService.AllWarehouse().ToList();
            customers = _repositoryService.AllCustomers().ToList();
            provinces = _repositoryService.AllProvinces().ToList();


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
            _repositoryService.AddAndUpdateItem(tt.ProductMaster);
            myMessageQueue.Enqueue("Product Updated");

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
            if(newFilee.Exists)
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
                var CountLine = VARIABLE.ProductMaster.Name.Replace("\r\n", "@").Count(x=>x=='@');
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
                            myMessageQueue.Enqueue("Advanced Shipment Notice Posted.");
                        else
                            myMessageQueue.Enqueue("Advanced Shipment Notice Saved.");

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
            string UPCForSearch = "";

            if (Mode == Mode.Sale)
            {
                UPCForSearch= UCSaleOrder.txtSearch.Text;
            }
            else
            {
                UPCForSearch = Purchasing.txtSearch.Text;
            }

            var SearchMasterProduct = _repositoryService.GiveMeProductMasterByUPC(UPCForSearch);
            if (SearchMasterProduct == null)
            {
                MessageBox.Show("Item Not Found");
            }
            else
            {
                switch (Mode)
                {

                    case Mode.Sale:
                        saleOrderViewModel.SoItems.Add(new SoItemVeiwModel()
                        {
                            ProductMaster = SearchMasterProduct,
                            ProductMaster_fk = SearchMasterProduct.Id,
                            Discount = 0,
                            Quantity = 0,
                            TotalPrice = 0m,
                            Price = SearchMasterProduct.WholesalePrice.Value,
                            Cost = SearchMasterProduct.Cost.Value
                        });

                        UCSaleOrder.txtSearch.Clear();
                        break;
                    case Mode.PO:
                        SelectedPo.ItemsOfPurchaseOrderViewModels.Add(new ItemsOfPurchaseOrderViewModel()
                        {
                            Price = SearchMasterProduct.FobPrice.Value,
                            ProductMaster = SearchMasterProduct,
                            ProductMaster_fk = SearchMasterProduct.Id
                        });
                        Purchasing.txtSearch.Clear();
                        break;
                    case Mode.Asn:
                        SelectedAsn.ItemsOfPurchaseOrderViewModels.Add(new ItemsOfPurchaseOrderViewModel()
                        {
                            Price = SearchMasterProduct.FobPrice.Value,
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
            txtMode.Text = "Advanced Shipment Notice";
            HideListview();
            lvPurchasing.Visibility = Visibility.Visible;
        }

        private void BtnGitInvoice_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            lvPurchasing.ItemsSource = PurchaseOrdersList.Where(p => p.CreatedAsn == true).Select(p => new PurchasingListview() { Alu = p.Id, LastModified = p.RowVersion, TotalPrice = p.AsnTotal, InvoiceNumber = p.Asnumber.ShowAsnNumber() });
            Mode = Mode.AsnInvoice;
            txtMode.Text = "ASN Invoice";
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


        private void BtnSalesOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mode = Mode.Sale;

            saleOrderViewModel = new SaleOrderViewModel();
            saleOrderViewModel.SoItems = new ObservableCollection<SoItemVeiwModel>();
            saleOrderViewModel.Id = 1358;
            saleOrderViewModel.TaxArea_fk = 2;
            saleOrderViewModel.TaxArea = provinces.SingleOrDefault(p => p.Id == 2);
            UCSaleOrder.SaleOrderViewModel = saleOrderViewModel;
            UCSaleOrder.cmbTaxAreaSo.ItemsSource = provinces;
            Bordermanagement.Child = UCSaleOrder;
            SubPage.Visibility = Visibility.Visible;
        }
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
