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
using PurchaseOrder = ClubJumana.DataLayer.Entities.PurchaseOrder;
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
            // dlg.DefaultExt = ".png";
            // dlg.Filter = "Excel |*.xlsx"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";
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
                    default:
                        MessageBox.Show("khariyat");
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
                    this.Dispatcher.Invoke(() => pbStatus.Minimum =0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.Customer.Count*2);
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
                        i ++;
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
                var variants = _repositoryService.allVariants().Where(p=>p.Barcode!=null).ToList();
                Variant findProduct;
                QbItem mm = JsonConvert.DeserializeObject<QbItem>(jsonString);
                List<ProductMaster> ProductList = new List<ProductMaster>();
                if (mm.QueryResponse.Item.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
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
                            _productService.AddProductMaster(VARIABLE,false);

                        }
                        else
                        {
                            ww.Name = w.Name;
                            ww.WholesalePrice = w.WholesalePrice;
                            ww.RetailPrice = w.RetailPrice;
                            ww.Active = w.Active;
                            ww.Size = w.Size;
                            ww.Color = w.Color;
                            ww.Bundle = w.Bundle;
                            ww.Taxable = w.Taxable;
                            ww.LastUpdateTime = w.LastUpdateTime;
                            ww.IsRetail = w.IsRetail;
                            ww.IsWholesale = w.IsWholesale;
                            ww.Image = w.Image;

                            _productService.UpdateProductMaster(ww,false);
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
                List<ProductMaster> ProductList = new List<ProductMaster>();
                if (mm.QueryResponse.Invoice.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.Invoice.Count * 2);
                    int i = 0;

                    foreach (var item in mm.QueryResponse.Invoice)
                    {

                        //ProductList.Add(new ProductMaster()
                        //{
                        //    Id = Convert.ToInt32(item.Id),
                        //    Name = item.Description.Trim(),
                        //    UPC = findProduct.Barcode.BarcodeNumber.Trim(),
                        //    SKU = findProduct.Sku.Trim(),
                        //    VariantFK = findProduct.Id,
                        //    WholesalePrice = Convert.ToDecimal(item.UnitPrice),
                        //    RetailPrice = findProduct.RetailPrice,
                        //    Size = findProduct.Size.Trim(),
                        //    Active = item.Active,
                        //    Color = findProduct.Colour?.Name.Trim(),
                        //    StyleNumber = findProduct.Product.StyleNumber.Trim(),
                        //    Bundle = findProduct.Bundle?.Trim(),
                        //    Taxable = item.Taxable,
                        //    UOMFK = 1,
                        //    LastUpdateTime = item.MetaData.LastUpdatedTime,
                        //    CreatedTime = item.MetaData.CreateTime,
                        //    IsRetail = findProduct.IsRetail,
                        //    IsWholesale = findProduct.IsWholesale,
                        //    Image = findProduct.Images.Count == 0
                        //        ? ""
                        //        : findProduct.Images.FirstOrDefault().ImageName.Trim()
                        //});
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
        private async Task ImportPurchaseOrder(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();
                var variants = _repositoryService.allVariants().Where(p => p.Barcode != null).ToList();
                Variant findProduct;
                QBPurchaseOrder mm = JsonConvert.DeserializeObject<QBPurchaseOrder>(jsonString);

                List<PurchaseOrder> PurchaseOrderList = new List<PurchaseOrder>();

                if (mm.QueryResponse.PurchaseOrder.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    this.Dispatcher.Invoke(() => pbStatus.Minimum = 0);
                    this.Dispatcher.Invoke(() => pbStatus.Maximum = mm.QueryResponse.PurchaseOrder.Count * 2);
                    int i = 0;
                    List<Item> temp = new List<Item>();
                    foreach (var purchaseOrder in mm.QueryResponse.PurchaseOrder)
                    {
                        temp.Clear();
                        foreach (var VARIABLE in purchaseOrder.Line)
                        {
                            temp.Add(new Item(){AsnQuantity = VARIABLE.ItemBasedExpenseLineDetail.Qty,GrnQuantity = VARIABLE.Received
                                ,AsnPrice = Convert.ToDecimal(VARIABLE.ItemBasedExpenseLineDetail.UnitPrice),GrnPrice = Convert.ToDecimal(VARIABLE.ItemBasedExpenseLineDetail.UnitPrice),
                                AsnItemsPrice = Convert.ToDecimal(VARIABLE.Amount),ProductMaster_fk = Convert.ToInt32(VARIABLE.ItemBasedExpenseLineDetail.ItemRef.value),
                                Alert = VARIABLE.Received!=VARIABLE.ItemBasedExpenseLineDetail.Qty,Checked = false,Diffrent = VARIABLE.ItemBasedExpenseLineDetail.Qty-VARIABLE.Received,
                                GrnItemsPrice = Convert.ToDecimal(Math.Round(VARIABLE.Received * VARIABLE.ItemBasedExpenseLineDetail.UnitPrice,2,MidpointRounding.ToEven))
                            });
                        }
                        PurchaseOrderList.Add(new PurchaseOrder()
                        {
                            Id = Convert.ToInt32(purchaseOrder.Id),
                            FromWarehouse_fk = 1,
                            ToWarehouse_fk = Convert.ToInt32(purchaseOrder.DepartmentRef.value),
                            DocNumber = purchaseOrder.DocNumber,
                            Asnumber = Convert.ToInt32(purchaseOrder.DocNumber),
                            Grnumber = Convert.ToInt32(purchaseOrder.DocNumber),
                            AsnTotal =Convert.ToDecimal(purchaseOrder.TotalAmt),
                            PrivateNote = purchaseOrder.PrivateNote,
                            Note = purchaseOrder.Memo,
                            PoStatus = purchaseOrder.POStatus,
                            Vendor_fk = Convert.ToInt32(purchaseOrder.VendorRef.value),
                            CreateTime = purchaseOrder.MetaData.CreateTime,
                            LastUpdateTime = purchaseOrder.MetaData.LastUpdatedTime,
                            ShipAddress = purchaseOrder.ShipAddr?.Line1+purchaseOrder.ShipAddr?.City+purchaseOrder.ShipAddr?.Country,
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

                    PurchaseOrder w;
                    PurchaseOrder ww;
                    foreach (var VARIABLE in DiffrentItems)
                    {
                        w = PurchaseOrderList.SingleOrDefault(p => p.Id == VARIABLE.Id);
                        ww = ExistPurchaseOrder.SingleOrDefault(p => p.Id == VARIABLE.Id);

                        if (IdForAdd.Contains(VARIABLE.Id))
                        {
                            _purchaseOrderService.AddPurchaseOrder(VARIABLE, true);

                        }
                        else
                        {
                            ww.DocNumber = w.DocNumber;
                            ww.FromWarehouse_fk = w.FromWarehouse_fk;
                            ww.ToWarehouse_fk = w.ToWarehouse_fk;
                            ww.Asnumber = w.Asnumber;
                            ww.Grnumber = w.Grnumber;
                            ww.AsnTotal = w.AsnTotal;
                            ww.PrivateNote = w.PrivateNote;
                            ww.Note = w.Note;
                            ww.LastUpdateTime = w.LastUpdateTime;
                            ww.PoStatus = w.PoStatus;
                            ww.Vendor_fk = w.Vendor_fk;
                            ww.LastUpdateTime = w.LastUpdateTime;
                            ww.ShipAddress = w.ShipAddress;
                            
                            _purchaseOrderService.UpdatePurchaseOrder(ww, false);
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

    }
}
