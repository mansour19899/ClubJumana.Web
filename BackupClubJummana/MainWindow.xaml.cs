using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BackupClubJummana.ComparerQB;
using BackupClubJummana.QuickBookModel;
using ClubJumana.Core.Services;
using ClubJumana.DataLayer.Entities;
using OfficeOpenXml;
using Newtonsoft.Json;
using Customer = ClubJumana.DataLayer.Entities.Customer;
using Item = ClubJumana.DataLayer.Entities.Item;
using Line = BackupClubJummana.QuickBookModel.Line;
using PurchaseOrder = ClubJumana.DataLayer.Entities.PurchaseOrder;
using TaxRate = ClubJumana.DataLayer.Entities.TaxRate;
using Term = ClubJumana.DataLayer.Entities.Term;
using Vendor = ClubJumana.DataLayer.Entities.Vendor;


namespace BackupClubJummana
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RepositoryService _repositoryService;
        private PerchaseOrderService _purchaseOrderService;
        private ProductInformationService _productInformationService;
        private ProductService _productService;
        public MainWindow()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _purchaseOrderService = new PerchaseOrderService();
            _productInformationService = new ProductInformationService();
            _productService = new ProductService();
        }

        private void BtnStartBackup_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Start");
            _repositoryService.UpdateLocalDb();
            MessageBox.Show("Finish");
        }

        private void BtnImport_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".json";
            dlg.Filter = "Json |*.json"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                switch (cmbImport.SelectedIndex)
                {
                    case 0:
                        ImportItemList(dlg.FileName);
                        break;
                    case 1:
                        ImportVendorList(dlg.FileName);
                        break;
                    case 2:
                        ImportCustomerList(dlg.FileName);
                        break;
                    case 3:
                        ImportInvoiceList(dlg.FileName);
                        break;
                    case 4:
                        ImportPurchaseOrder(dlg.FileName);
                        break;
                    case 5:
                        Microsoft.Win32.OpenFileDialog dlgg = new Microsoft.Win32.OpenFileDialog();
                        dlgg.DefaultExt = ".json";
                        dlgg.Filter = "Json |*.json"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";
                        bool? resultt = dlgg.ShowDialog();
                        if (resultt != true)
                        {
                            MessageBox.Show("Import TaxCode File");
                            break;
                        }
                        ImportTaxRate(dlg.FileName, dlgg.FileName);
                        break;
                    case 6:
                        ImportTerm(dlg.FileName);
                        break;
                    default:
                        MessageBox.Show("Fusma");
                        break;
                }


            }
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private async Task ImportVendorList(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();
                QBVendor mm = JsonConvert.DeserializeObject<QBVendor>(jsonString);
                List<Vendor> VendorsList = new List<Vendor>();

                if (mm.QueryResponse.Vendor.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.Navy);
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.Vendor.Count * 2);
                    int i = 0;

                    foreach (var item in mm.QueryResponse.Vendor)
                    {
                        VendorsList.Add(new Vendor()
                        {
                            Id = Convert.ToInt32(item.Id),
                            FirstName = item.GivenName,
                            LastName = item.FamilyName,
                            DisplayName = item.DisplayName,
                            MiddleName = item.MiddleName,
                            PrintOnCheckName = item.PrintOnCheckName,
                            Balance = item.Balance,
                            Active = item.Active,
                            CompanyName = item.CompanyName,
                            Currency = item.CurrencyRef.value,
                            CreateTime = item.MetaData.CreateTime,
                            LastUpDateTime = item.MetaData.LastUpdatedTime,
                            Email = item.PrimaryEmailAddr?.Address,
                            PostalCode = item.BillAddr?.PostalCode,
                            Phone1 = item.Mobile?.FreeFormNumber,
                        });
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    foreach (var vendor in VendorsList.OrderBy(p => p.Id))
                    {
                        _repositoryService.AddAndUpdateVendor(vendor, false);
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }
                    bool res = _repositoryService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = pbStatus.Maximum);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    if (!res)
                        MessageBox.Show("Error");

                }

            });

        }

        private async Task ImportCustomerList(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();
                QBCustomer mm = JsonConvert.DeserializeObject<QBCustomer>(jsonString);
                List<Customer> customerList = new List<Customer>();

                if (mm.QueryResponse.Customer.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.Navy);
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.Customer.Count * 2);
                    int i = 0;

                    foreach (var item in mm.QueryResponse.Customer)
                    {
                        customerList.Add(new Customer()
                        {
                            Id = Convert.ToInt32(item.Id),
                            ContactName = item.GivenName,
                            ContactLastName = item.FamilyName,
                            Phone1 = item.PrimaryPhone == null ? "" : item.PrimaryPhone.FreeFormNumber,
                            Email = item.PrimaryEmailAddr == null ? "" : item.PrimaryEmailAddr.Address,
                            City = item.BillAddr == null ? "" : item.BillAddr.City,
                            CompanyName = item.CompanyName ?? "",
                            PostalCode = item.BillAddr == null ? "" : item.BillAddr.PostalCode,
                            DisplayBillAddress = item.BillAddr == null ? "" : item.GivenName + " " + item.FamilyName + "\n" + item.BillAddr.Line1 + "\n" + item.BillAddr.City + ", " + item.BillAddr.CountrySubDivisionCode + " " +
                                                                              item.BillAddr.PostalCode + "\n" + item.BillAddr.Country,
                            DisplayShipAddress = item.ShipAddr == null ? "" : item.GivenName + " " + item.FamilyName + "\n" + item.BillAddr.Line1 + "\n" + item.BillAddr.City + ", " + item.BillAddr.CountrySubDivisionCode + " " +
                                                                              item.BillAddr.PostalCode + "\n" + item.BillAddr.Country,
                            BalanceDueLCY = Convert.ToDecimal(item.Balance),
                            BalanceLCY = Convert.ToDecimal(item.BalanceWithJobs),
                            Active = item.Active,
                            Note = item.Notes,
                            CreatedBy_fk = 1
                        });
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }
                    foreach (var customer in customerList.OrderBy(p => p.Id))
                    {
                        _repositoryService.AddAndUpdateCustomer(customer, false);
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }
                    bool res = _repositoryService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = pbStatus.Maximum);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    if (!res)
                        MessageBox.Show("Error");

                }

            });

        }
        private async Task ImportItemList(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();
                var variants = _repositoryService.allVariants().Where(p => p.Barcode != null).ToList();
                Variant findProduct;
                QbItem mm = JsonConvert.DeserializeObject<QbItem>(jsonString);
                List<ProductMaster> ProductList = new List<ProductMaster>();
                if (mm.QueryResponse.Item.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.Navy);
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.Item.Count * 2);
                    int i = 0;

                    foreach (var item in mm.QueryResponse.Item)
                    {
                        findProduct = variants.FirstOrDefault(p => p.Barcode.BarcodeNumber.CompareTo(item.Name) == 0);
                        if (findProduct == null)
                        {
                            ProductList.Add(new ProductMaster()
                            {
                                Id = Convert.ToInt32(item.Id),
                                Name = item.Description == null ? "" : item.Description.Trim(),
                                UPC = item.Name.Trim(),
                                WholesalePrice = 0,
                                Active = item.Active,
                            });
                        }
                        else
                        {
                            ProductList.Add(new ProductMaster()
                            {
                                Id = Convert.ToInt32(item.Id),
                                Name = item.Description.Trim(),
                                UPC = findProduct.Barcode.BarcodeNumber.Trim(),
                                SKU = findProduct.Sku.Trim(),
                                VariantFK = findProduct.Id,
                                WholesalePrice = Convert.ToDecimal(item.UnitPrice),
                                RetailPrice = findProduct.RetailPrice,
                                FobPrice = findProduct.FobPrice,
                                Size = findProduct.Size.Trim(),
                                Active = item.Active,
                                Color = findProduct.Colour?.Name.Trim(),
                                StyleNumber = findProduct.Product.StyleNumber.Trim(),
                                Bundle = findProduct.Bundle?.Trim(),
                                Taxable = item.Taxable,
                                UOMFK = 1,
                                LastUpdateTime = item.MetaData.LastUpdatedTime,
                                CreatedTime = item.MetaData.CreateTime,
                                IsRetail = findProduct.IsRetail,
                                IsWholesale = findProduct.IsWholesale,
                                Image = findProduct.Images.Count == 0
                                    ? ""
                                    : findProduct.Images.FirstOrDefault().ImageName.Trim()
                            });
                        }
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);

                    }

                    var ExistProduct = _repositoryService.AllProductMasterList().ToList();
                    ProductMaster CheckItem = new ProductMaster();
                    List<ProductMaster> removList = new List<ProductMaster>();


                    var comparer = new ProductMasterComparerQB();
                    var DiffrentItems = ProductList.Except(ExistProduct, comparer).ToList();

                    var IdForAdd = ProductList.Select(p => p.Id).Except(ExistProduct.Select(p => p.Id));

                    ProductMaster w;
                    ProductMaster ww;
                    foreach (var VARIABLE in DiffrentItems)
                    {
                        w = ProductList.SingleOrDefault(p => p.Id == VARIABLE.Id);
                        ww = ExistProduct.SingleOrDefault(p => p.Id == VARIABLE.Id);

                        if (IdForAdd.Contains(VARIABLE.Id))
                        {
                            _productService.AddProductMaster(VARIABLE, false);

                        }
                        else
                        {
                            ww.Name = w.Name;
                            ww.WholesalePrice = w.WholesalePrice;
                            ww.RetailPrice = w.RetailPrice;
                            ww.FobPrice = w.FobPrice;
                            ww.Active = w.Active;
                            ww.Size = w.Size;
                            ww.Color = w.Color;
                            ww.Bundle = w.Bundle;
                            ww.Taxable = w.Taxable;
                            ww.LastUpdateTime = w.LastUpdateTime;
                            ww.IsRetail = w.IsRetail;
                            ww.IsWholesale = w.IsWholesale;
                            ww.Image = w.Image;

                            _productService.UpdateProductMaster(ww, false);
                        }

                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    bool res = _productService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = pbStatus.Maximum);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    if (!res)
                        MessageBox.Show("Error");
                }

            });

        }
        private async Task ImportInvoiceList(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();
                QBInvoice mm = JsonConvert.DeserializeObject<QBInvoice>(jsonString);
                var tttr = mm.QueryResponse.Invoice
                    .Where(p => p.Line?.ElementAt(0).SalesItemLineDetail?.TaxCodeRef?.value == "27").ToList();
                var ttqtr = mm.QueryResponse.Invoice
                    .Where(p => p.Line?.ElementAt(0).SalesItemLineDetail?.TaxCodeRef?.value == "18").ToList();
                List<SaleOrder> SalesOrderList = new List<SaleOrder>();
                if (mm.QueryResponse.Invoice.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.Invoice.Count * 2);
                    int i = 0;
                    double subAmt = 0;
                    string PoNum = "";
                    List<SoItem> temp = new List<SoItem>();
                    var TaxCodeList = _repositoryService.AlltaTaxRates().ToList();
                    foreach (var saleorder in mm.QueryResponse.Invoice)
                    {

                        temp.Clear();
                        int TaxCodeTemp = 0;
                        int TaxCodeShipping = 0;
                        decimal ShippingAmt = 0m;
                        foreach (var VARIABLE in saleorder.Line)
                        {
                            if (VARIABLE.DetailType.CompareTo("SalesItemLineDetail") == 0&&VARIABLE.SalesItemLineDetail.ItemRef.value.CompareTo("SHIPPING_ITEM_ID") != 0)
                            {
                                TaxCodeTemp = TaxCodeList.FirstOrDefault(p=>p.TaxCodeId==Convert.ToInt32(VARIABLE.SalesItemLineDetail.TaxCodeRef.value)).Id;
                                temp.Add(new SoItem()
                                {
                                    ProductMaster_fk = Convert.ToInt32(VARIABLE.SalesItemLineDetail.ItemRef.value),
                                    Quantity = Convert.ToInt32(VARIABLE.SalesItemLineDetail.Qty),
                                    Price = Convert.ToDecimal(VARIABLE.SalesItemLineDetail.UnitPrice),
                                    TotalPrice = Convert.ToDecimal(VARIABLE.Amount), 
                                    TaxCode =Convert.ToByte(TaxCodeTemp),
                                    IsAbaleToRefund = true,
                                    So_fk = Convert.ToInt32(saleorder.Id),
                                });
                            }
                            else if (VARIABLE.SalesItemLineDetail?.ItemRef.value.CompareTo("SHIPPING_ITEM_ID") == 0)
                            {
                                TaxCodeShipping = TaxCodeList.FirstOrDefault(p => p.TaxCodeId == Convert.ToInt32(VARIABLE.SalesItemLineDetail.TaxCodeRef.value)).Id;
                                ShippingAmt = Convert.ToDecimal(VARIABLE.Amount);
                            }
                            else
                            {
                                
                            }
                        }



                        subAmt = saleorder.Line.FirstOrDefault(p => p.DetailType.CompareTo("SubTotalLineDetail") == 0)
                            .Amount;
                        PoNum = saleorder.CustomField.FirstOrDefault(p => p.Name.CompareTo("P.O. Number") == 0)?
                            .StringValue;
                        SalesOrderList.Add(new SaleOrder()
                        {
                            Id = Convert.ToInt32(saleorder.Id),
                            DocNumber = saleorder.DocNumber,
                            SoDate = saleorder.MetaData.CreateTime,
                            InvoiceDate = saleorder.TxnDate == null ? (DateTime?)null : DateTime.Parse(saleorder.TxnDate),
                            DueDate = saleorder.DueDate == null ? (DateTime?)null : DateTime.Parse(saleorder.DueDate),
                            ShipDate = saleorder.ShipDate == null ? (DateTime?)null : DateTime.Parse(saleorder.ShipDate),
                            InvoiceNumber = -1,
                            Cashier_fk = 1,
                            Customer_fk = Convert.ToInt32(saleorder.CustomerRef.value),
                            Warehouse_fk = Convert.ToInt32(saleorder.DepartmentRef.value),
                            term_fk = Convert.ToInt32(saleorder.SalesTermRef.value),
                            TermPercent = 0,
                            Subtotal = Convert.ToDecimal(subAmt),
                            SoTotalPrice = Convert.ToDecimal(saleorder.TotalAmt),
                            HomeBalance = Convert.ToDecimal(saleorder.HomeBalance),
                            OpenBalance = Convert.ToDecimal(saleorder.Balance),
                            HomeTotalAmt = Convert.ToDecimal(saleorder.HomeTotalAmt),
                            PoNumber = PoNum,
                            ShipVia = saleorder.ShipMethodRef?.value,
                            TrackingNo = saleorder.TrackingNum,
                            CreatedTime = saleorder.MetaData.CreateTime,
                            LastUpdateTime = saleorder.MetaData.LastUpdatedTime,
                            Currency = saleorder.CurrencyRef?.value,
                            CustomerMemo = saleorder.CustomerMemo?.value,
                            ExchangeRate = Convert.ToDecimal(saleorder.ExchangeRate),
                            Shipping = ShippingAmt,
                            ShippingTaxCode = Convert.ToByte(TaxCodeShipping),
                            SoItems = temp
                        });
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);

                    }

                    var ExistProduct = _repositoryService.AllProductMasterList().ToList();
                    ProductMaster CheckItem = new ProductMaster();
                    List<ProductMaster> removList = new List<ProductMaster>();


                    //var comparer = new ProductMasterComparerQB();
                    //var DiffrentItems = ProductList.Except(ExistProduct, comparer).ToList();

                    //var IdForAdd = ProductList.Select(p => p.Id).Except(ExistProduct.Select(p => p.Id));

                    //ProductMaster w;
                    ////ProductMaster ww;
                    //foreach (var VARIABLE in DiffrentItems)
                    //{
                    //    w = ProductList.SingleOrDefault(p => p.Id == VARIABLE.Id);
                    //    ww = ExistProduct.SingleOrDefault(p => p.Id == VARIABLE.Id);

                    //    if (IdForAdd.Contains(VARIABLE.Id))
                    //    {
                    //        _productService.AddProductMaster(VARIABLE, false);

                    //    }
                    //    else
                    //    {
                    //        ww.Name = w.Name;
                    //        ww.WholesalePrice = w.WholesalePrice;
                    //        ww.RetailPrice = w.RetailPrice;
                    //        ww.Active = w.Active;
                    //        ww.Size = w.Size;
                    //        ww.Color = w.Color;
                    //        ww.Bundle = w.Bundle;
                    //        ww.Taxable = w.Taxable;
                    //        ww.LastUpdateTime = w.LastUpdateTime;
                    //        ww.IsRetail = w.IsRetail;
                    //        ww.IsWholesale = w.IsWholesale;
                    //        ww.Image = w.Image;

                    //        _productService.UpdateProductMaster(ww, false);
                    //    }

                    //    i++;
                    //    this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    //}

                 //   bool res = _productService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = pbStatus.Maximum);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    //if (!res)
                    //    MessageBox.Show("Error");
                }

            });

        }
        private async Task ImportPurchaseOrder(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();
                QBPurchaseOrder mm = JsonConvert.DeserializeObject<QBPurchaseOrder>(jsonString);

                List<PurchaseOrder> PurchaseOrderList = new List<PurchaseOrder>();

                if (mm.QueryResponse.PurchaseOrder.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.Navy);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.PurchaseOrder.Count * 2);
                    int i = 0;
                    List<Item> temp = new List<Item>();
                    foreach (var purchaseOrder in mm.QueryResponse.PurchaseOrder)
                    {
                        temp.Clear();
                        foreach (var VARIABLE in purchaseOrder.Line)
                        {
                            temp.Add(new Item()
                            {
                                PoQuantity = VARIABLE.ItemBasedExpenseLineDetail.Qty,
                                AsnQuantity = VARIABLE.Received,
                                GrnQuantity = 0,
                                PoPrice = Convert.ToDecimal(VARIABLE.ItemBasedExpenseLineDetail.UnitPrice),
                                AsnPrice = Convert.ToDecimal(VARIABLE.ItemBasedExpenseLineDetail.UnitPrice),
                                GrnPrice = Convert.ToDecimal(VARIABLE.ItemBasedExpenseLineDetail.UnitPrice),
                                PoItemsPrice = Convert.ToDecimal(VARIABLE.Amount),
                                ProductMaster_fk = Convert.ToInt32(VARIABLE.ItemBasedExpenseLineDetail.ItemRef.value),
                                Alert = VARIABLE.Received != VARIABLE.ItemBasedExpenseLineDetail.Qty,
                                Checked = false,
                                Diffrent = VARIABLE.ItemBasedExpenseLineDetail.Qty - VARIABLE.Received,
                                AsnItemsPrice = Convert.ToDecimal(Math.Round(VARIABLE.Received * VARIABLE.ItemBasedExpenseLineDetail.UnitPrice, 2, MidpointRounding.ToEven))
                            });
                        }
                        PurchaseOrderList.Add(new PurchaseOrder()
                        {
                            Id = Convert.ToInt32(purchaseOrder.Id),
                            FromWarehouse_fk = 1,
                            ToWarehouse_fk = Convert.ToInt32(purchaseOrder.DepartmentRef.value),
                            DocNumber = purchaseOrder.DocNumber,
                            PoNumber = Convert.ToInt32(purchaseOrder.DocNumber),
                            Asnumber = Convert.ToInt32(purchaseOrder.DocNumber),
                            Grnumber = Convert.ToInt32(purchaseOrder.DocNumber),
                            PoSubtotal = Convert.ToDecimal(purchaseOrder.TotalAmt),
                            CreatedPO = true,
                            CreatedAsn = false,
                            ApprovePoUser_fk = 1,
                            ApproveAsnUser_fk = 1,
                            ExchangeRate = Convert.ToDecimal(purchaseOrder.ExchangeRate),
                            AsnDate = purchaseOrder.MetaData.CreateTime,
                            PrivateNote = purchaseOrder.PrivateNote,
                            Note = purchaseOrder.Memo,
                            PoStatus = purchaseOrder.POStatus,
                            Vendor_fk = Convert.ToInt32(purchaseOrder.VendorRef.value),
                            CreateTime = purchaseOrder.MetaData.CreateTime,
                            LastUpdateTime = purchaseOrder.MetaData.LastUpdatedTime,
                            ShipAddress = purchaseOrder.ShipAddr?.Line1 + purchaseOrder.ShipAddr?.City + purchaseOrder.ShipAddr?.Country,
                            Items = temp.ToList()

                        });
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    var ExistPurchaseOrder = _repositoryService.AllPurchaseOrder().ToList();
                    ProductMaster CheckItem = new ProductMaster();
                    List<ProductMaster> removList = new List<ProductMaster>();


                    var comparer = new PurchaseOrderComparerQB();
                    var DiffrentItems = PurchaseOrderList.Except(ExistPurchaseOrder, comparer).ToList();

                    var IdForAdd = PurchaseOrderList.Select(p => p.Id).Except(ExistPurchaseOrder.Select(p => p.Id));

                    int resualt = 0;
                    foreach (var VARIABLE in DiffrentItems)
                    {

                        if (IdForAdd.Contains(VARIABLE.Id))
                        {
                            resualt = _purchaseOrderService.AddPurchaseOrder(VARIABLE, true);
                            if (resualt == -11)
                                MessageBox.Show("Error Not Find Product");
                        }
                        else
                        {
                            _purchaseOrderService.UpdatePurchaseOrder(VARIABLE, false);
                        }

                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    bool res = _productService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = pbStatus.Maximum);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    if (!res)
                        MessageBox.Show("Error");
                }

            });
        }
        private async Task ImportTaxRate(string fileNameTaxRate, string fileNameTaxCode)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileNameTaxRate);
                string jsonString = r.ReadToEnd();

                StreamReader rr = new StreamReader(fileNameTaxCode);
                string jsonStringg = rr.ReadToEnd();

                QBTaxRate mm = JsonConvert.DeserializeObject<QBTaxRate>(jsonString);
                QBTaxCode mmm = JsonConvert.DeserializeObject<QBTaxCode>(jsonStringg);





                List<TaxRate> TaxRateList = new List<TaxRate>();
                if (mm.QueryResponse.TaxRate.Count == 0)
                    MessageBox.Show("List Tax Rate Empty");
                else if (mmm.QueryResponse.TaxCode.Count == 0)
                    MessageBox.Show("List Tax Code Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.Navy);
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.TaxRate.Count * 2 + mmm.QueryResponse.TaxCode.Count * 2);
                    int i = 0;

                    foreach (var taxRate in mm.QueryResponse.TaxRate)
                    {
                        TaxRateList.Add(new TaxRate()
                        {
                            Id = 0,
                            Active = taxRate.Active,
                            Name = taxRate.Description,
                            Description = taxRate.Name,
                            Rate = Convert.ToDecimal(taxRate.RateValue),
                            TaxRateId = Convert.ToInt32(taxRate.Id),
                            TaxCodeId = 0
                        });
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    foreach (var taxRate in mmm.QueryResponse.TaxCode)
                    {
                        TaxRateList.Add(new TaxRate()
                        {
                            Id = 0,
                            Active = taxRate.Active,
                            Name = taxRate.Name,
                            Description = taxRate.Description,
                            Rate = 0,
                            TaxRateId = 0,
                            TaxCodeId = Convert.ToInt32(taxRate.Id),
                        });
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    foreach (var tax in TaxRateList)
                    {
                        _repositoryService.AddAndUpdateTaxRate(tax, true);
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }
                    bool res = _repositoryService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = pbStatus.Maximum);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    if (!res)
                        MessageBox.Show("Error");

                }

            });

        }

        private async Task ImportTerm(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();


                QBTerm mm = JsonConvert.DeserializeObject<QBTerm>(jsonString);

                List<Term> TermList = new List<Term>();

                if (mm.QueryResponse.Term.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.Navy);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.Term.Count * 2);
                    int i = 0;
                    foreach (var term in mm.QueryResponse.Term)
                    {
                        TermList.Add(new Term()
                        {
                            Id = Convert.ToInt32(term.Id),
                            Name = term.Name,
                            Active = term.Active,
                            Description = "",
                            DueDateCalculation = term.DueDays,
                            CreatedTime = term.MetaData.CreateTime,
                            LastUpdateTime = term.MetaData.LastUpdatedTime,

                        });
                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    var ExistTerm = _repositoryService.AllTerms().ToList();


                    var comparer = new TermComparerQB();
                    var DiffrentItems = TermList.Except(ExistTerm, comparer).ToList();

                    var IdForAdd = TermList.Select(p => p.Id).Except(ExistTerm.Select(p => p.Id));

                    int resualt = 0;
                    foreach (var VARIABLE in DiffrentItems)
                    {

                        if (IdForAdd.Contains(VARIABLE.Id))
                        {
                            _repositoryService.AddTerm(VARIABLE, true);
                        }
                        else
                        {
                            _repositoryService.UpdateTerm(VARIABLE, true);
                        }

                        i++;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }

                    bool res = _repositoryService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = pbStatus.Maximum);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    if (!res)
                        MessageBox.Show("Error");
                }

            });
        }


    }
}
