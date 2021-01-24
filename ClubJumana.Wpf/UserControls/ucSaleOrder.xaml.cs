using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucSaleOrder.xaml
    /// </summary>
    public partial class ucSaleOrder : UserControl
    {
        public List<Customer> CustomersList;
        private SaleOrderViewModel _saleOrderViewModel;
        public Mode Mode = Mode.Nothong;
        private bool AllowSearch = false;
        private List<InventoryProduct> inventoryProducts;
        public List<SoItemVeiwModel> RemoveSoItemViewModel;
        public SaleOrderViewModel SaleOrderViewModel
        {
            get { return _saleOrderViewModel; }
            set
            {
                _saleOrderViewModel = value;
                DataContext = SaleOrderViewModel;
                if (_saleOrderViewModel.SoItems != null)
                {
                    inventoryProducts.Clear();
                    RemoveSoItemViewModel.Clear();
                    foreach (var item in _saleOrderViewModel.SoItems)
                    {
                        AddInventoryInformation(item, _saleOrderViewModel.Warehouse_fk);
                    }
                }

            }
        }

        public ucSaleOrder()
        {
            InitializeComponent();
            SaleOrderViewModel = new SaleOrderViewModel();
            inventoryProducts = new List<InventoryProduct>();
            RemoveSoItemViewModel = new List<SoItemVeiwModel>();
        }

        private void UcSaleOrder_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
        public event EventHandler<EventArgs> BtnSaveOnClick;
        private void BtnSaveSalesOrder_OnClick(object sender, RoutedEventArgs e)
        {
            if (SaleOrderViewModel.OpenBalance < 0)
            {
                MessageBox.Show("You need to specify an account to credit the deposit to.");
            }
            else
            {
                e.Handled = true;
                if (BtnSaveOnClick != null)
                    BtnSaveOnClick(sender, e);
            }

        }

        public event EventHandler<EventArgs> BtnCloseSubPage;
        private void BtnCloseSubPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (GrdRefuand.Visibility == Visibility.Visible)
            {
                GrdRefuand.Visibility = Visibility.Hidden;
                SaleOrderViewModel.IsRefund = false;
            }
            else
            {
                AllowSearch = false;
                e.Handled = true;
                if (BtnCloseSubPage != null)
                    BtnCloseSubPage(sender, e);
            }
            
        }

        public event EventHandler<EventArgs> BtnPostSalesOrder;
        private void BtnPostSalesOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnSaveOnClick != null)
                BtnSaveOnClick(sender, e);
            if (BtnPostSalesOrder != null)
                BtnPostSalesOrder(sender, e);
        }
        public event EventHandler<EventArgs> BtnPrintOrSend;
        private void BtnPrintOrSend_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnPrintOrSend != null)
                BtnPrintOrSend(sender, e);
        }

        private void TxtSearch_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnAddItem_OnClick(sender, e);
            }
        }

        public event EventHandler<EventArgs> BtnAddItemOnClick;
        private void BtnAddItem_OnClick(object sender, RoutedEventArgs e)
        {
            var IsExist =
                SaleOrderViewModel.SoItems.Any(p => p.ProductMaster.UPC.Trim().CompareTo(txtSearch.Text) == 0);
            if (IsExist)
            {
                MessageBox.Show("Product is Exist ");
                txtSearch.Clear();
                txtSearch.Focus();
            }
            else
            {
                e.Handled = true;
                if (btnAddItem != null)
                    BtnAddItemOnClick(sender, e);
            }

        }

        private void BtnDeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (dgSoItems.SelectedItems.Cast<SoItemVeiwModel>().ToList().Count == 0)
            {

            }
            else
            {
                RemoveSoItemViewModel.AddRange(dgSoItems.SelectedItems.Cast<SoItemVeiwModel>().ToList());
                foreach (var VARIABLE in RemoveSoItemViewModel)
                {
                    VARIABLE.IsDeleted = true;
                    SaleOrderViewModel.SoItems.Remove(VARIABLE);
                    if (VARIABLE.Id == 0)
                        RemoveSoItemViewModel.Remove(VARIABLE);
                    var tt = inventoryProducts.FirstOrDefault(p => p.IdProduct == VARIABLE.ProductMaster_fk);
                    if (tt != null)
                        inventoryProducts.Remove(tt);
                }
                dgSoItems.Items.Refresh();
                SumSalesOrder();
            }

        }

        private void DgSoItems_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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
                        SaleOrderViewModel.SoItems.ElementAt(row_index).Quantity = 0;
                        break;
                    }
                    int Qyt = (!string.IsNullOrWhiteSpace(qytStr)) ? Convert.ToInt32(qytStr) : 0;
                    if (Qyt < 0)
                        Qyt = Qyt * -1;
                    SaleOrderViewModel.SoItems.ElementAt(row_index).Quantity = Qyt;
                    if (SaleOrderViewModel.InvoiceNumber == null)
                    {
                        inventoryProducts.ElementAt(row_index).Reserved -= inventoryProducts.ElementAt(row_index).Quantity;
                        inventoryProducts.ElementAt(row_index).Reserved += Qyt;
                        inventoryProducts.ElementAt(row_index).Quantity = Qyt;
                    }
                    else
                    {
                        inventoryProducts.ElementAt(row_index).StockOnHand += inventoryProducts.ElementAt(row_index).Quantity;
                        inventoryProducts.ElementAt(row_index).StockOnHand -= Qyt;
                        inventoryProducts.ElementAt(row_index).Quantity = Qyt;
                    }

                    break;
                case "Discount":
                    if (SaleOrderViewModel.SoItems.ElementAt(row_index).Quantity == 0)
                    {
                        MessageBox.Show("First Input Quantity");
                        SaleOrderViewModel.SoItems.ElementAt(row_index).Discount = 0;
                    }
                    else
                    {
                        var discountStr = (((TextBox)e.EditingElement).Text).Replace("%", "");
                        if (discountStr.Count(x => x == '.') > 1)
                        {
                            MessageBox.Show("Enter Correct Number");
                            SaleOrderViewModel.SoItems.ElementAt(row_index).Discount = 0;
                            break;
                        }
                        else
                        {
                            decimal Discount = (!string.IsNullOrWhiteSpace(discountStr)) ? Convert.ToDecimal(discountStr) : 0m;
                            SaleOrderViewModel.SoItems.ElementAt(row_index).Discount = Discount;
                        }

                    }
                    break;

            }


            SumSalesOrder();

        }

        private void SumSalesOrder()
        {
            SaleOrderViewModel.SumPrice();
            ShowTaxes();

        }
        private void SumRefund()
        {
            SaleOrderViewModel.SumRefundPrice();
            ShowRefundTaxes();

        }

        public void ShowTaxes()
        {
            int i = 0;
            foreach (var itemTax in SaleOrderViewModel.Taxes.Where(p => p.Amount != 0).OrderBy(p => p.Code))
            {
                i++;
                switch (i)
                {
                    case 1:
                        lblTax1.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax1.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax1.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        lblTax2.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax2.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax2.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        lblTax3.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax3.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax3.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        lblTax4.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax4.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax4.Visibility = Visibility.Visible;
                        break;
                    case 5:
                        lblTax5.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax5.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax5.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        lblTax6.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax6.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax6.Visibility = Visibility.Visible;
                        break;
                }

            }
            CollapsedTax(i);
        }
        public void CollapsedTax(int i)
        {
            if (i == 0)
                Tax1.Visibility = Visibility.Collapsed;
            if (i < 2)
                Tax2.Visibility = Visibility.Collapsed;
            if (i < 3)
                Tax3.Visibility = Visibility.Collapsed;
            if (i < 4)
                Tax4.Visibility = Visibility.Collapsed;
            if (i < 5)
                Tax5.Visibility = Visibility.Collapsed;
            if (i < 6)
                Tax6.Visibility = Visibility.Collapsed;
        }
        public void ShowRefundTaxes()
        {
            int i = 0;
            foreach (var itemTax in SaleOrderViewModel.Refund.TaxesRefunds.Where(p => p.Amount != 0).OrderBy(p => p.Code))
            {
                i++;
                switch (i)
                {
                    case 1:
                        lblTax1Refund.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax1Refund.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax1Refund.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        lblTax2Refund.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax2Refund.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax2Refund.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        lblTax3Refund.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax3Refund.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax3Refund.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        lblTax4Refund.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax4Refund.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax4Refund.Visibility = Visibility.Visible;
                        break;
                    case 5:
                        lblTax5Refund.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax5Refund.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax5Refund.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        lblTax6Refund.Content = itemTax.Code + " @ " + itemTax.Rate.RoundNumtoStr() + "% on " + itemTax.Amount.RoundNumtoStr();
                        txtTax6Refund.Text = itemTax.TaxAmount.RoundNumtoStr();
                        Tax6Refund.Visibility = Visibility.Visible;
                        break;
                }

            }
            CollapsedRefundTax(i);
        }
        public void CollapsedRefundTax(int i)
        {
            if (i == 0)
                Tax1Refund.Visibility = Visibility.Collapsed;
            if (i < 2)
                Tax2Refund.Visibility = Visibility.Collapsed;
            if (i < 3)
                Tax3Refund.Visibility = Visibility.Collapsed;
            if (i < 4)
                Tax4Refund.Visibility = Visibility.Collapsed;
            if (i < 5)
                Tax5Refund.Visibility = Visibility.Collapsed;
            if (i < 6)
                Tax6Refund.Visibility = Visibility.Collapsed;
        }
        private void CmbTaxAreaSo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // PrepareTaxitem(cmbTaxAreaSo.SelectedItem as Province);

        }


        private void TxtCustomerLookup_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (AllowSearch)
            {
                var resalt = CustomersList.Where(p => p.CompanyName.ToLower().Contains(txtCustomerLookup.Text.ToLower())
                                                      || p.ContactName.ToLower().Contains(txtCustomerLookup.Text.ToLower())
                                                      || p.Phone1.ToLower().Contains(txtCustomerLookup.Text.ToLower()));
                if (resalt.Count() == 0)
                    lvCustomerLookup.ItemsSource = new List<Customer>() { new Customer() { CompanyName = "Not Find Any Customer" } };
                else
                    lvCustomerLookup.ItemsSource = resalt;

                lvCustomerLookupBorder.Visibility = Visibility.Visible;
            }

        }

        private void TxtCustomerLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                lvCustomerLookupBorder.Visibility = Visibility.Hidden;
                dpiSoDate.Focus();
            }
        }

        private void LvCustomerLookupBorder_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (lvCustomerLookup.SelectedIndex != -1)
            {
                SaleOrderViewModel.Customer = lvCustomerLookup.SelectedItem as Customer;
                SetBillingShippingAddress();
            }
            else
            {
                SaleOrderViewModel.Customer = lvCustomerLookup.ItemsSource.Cast<Customer>().FirstOrDefault();
                SetBillingShippingAddress();
            }
            lvCustomerLookupBorder.Visibility = Visibility.Hidden;
            dpiSoDate.Focus();
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
            SaleOrderViewModel.Customer_fk = wer.Id;
            SaleOrderViewModel.Customer = wer;
            SetBillingShippingAddress();
            lvCustomerLookupBorder.Visibility = Visibility.Hidden;
            dpiSoDate.Focus();
        }

        private void SetBillingShippingAddress()
        {
            SaleOrderViewModel.BillingAddress = SaleOrderViewModel.Customer.DisplayBillAddress;
            SaleOrderViewModel.ShippingAddress = SaleOrderViewModel.Customer.DisplayShipAddress;
            txtShppingAddress.Text = SaleOrderViewModel.Customer.DisplayShipAddress;
            txtbillingAddress.Text = SaleOrderViewModel.Customer.DisplayBillAddress;
        }

        private void TxtCustomerLookup_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            AllowSearch = true;

        }

        private void TxtCustomerLookup_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TxtCustomerLookup_OnGotMouseCapture(null, null);
        }

        private void DpiSoDate_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                dpiSoDate.SelectedDate = DateTime.Now;
                dpiExpiration.Focus();
                e.Handled = true;
            }


        }


        private void TxtTermPercent_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtTermPercent_OnLostFocus(null, null);
            }

        }

        private void TxtTermPercent_OnLostFocus(object sender, RoutedEventArgs e)
        {
            foreach (var VARIABLE in SaleOrderViewModel.SoItems)
            {
                VARIABLE.TermPercent = SaleOrderViewModel.TermPercent;
            }
        }

        private void dgSoItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowInventory();
        }

        private void dgSoItems_LostFocus(object sender, RoutedEventArgs e)
        {
            txtShowInventory.Text = "Lines";
            txtShowInventory.FontSize = 25;
        }


        private void ShowInventory()
        {
            if (dgSoItems.SelectedIndex != -1)
            {
                var inventory = inventoryProducts.SingleOrDefault(i =>
                    i.IdProduct == SaleOrderViewModel.SoItems.ElementAt(dgSoItems.SelectedIndex).ProductMaster_fk);
                txtShowInventory.Text = "Stock On Hand: " + inventory.StockOnHand + "   Reserved: " + inventory.Reserved + "   Transit: " + inventory.Transit;
                txtShowInventory.FontSize = 20;
            }
        }

        public void AddInventoryInformation(SoItemVeiwModel soItem, int? WarehouseId)
        {
            ProductInventoryWarehouse tt = new ProductInventoryWarehouse() { Inventory = 0 };
            if (soItem.ProductMaster.ProductInventoryWarehouses != null)
                tt = soItem.ProductMaster.ProductInventoryWarehouses.FirstOrDefault(p => p.Warehouse_fk == WarehouseId);
            inventoryProducts.Add(new InventoryProduct()
            {
                IdProduct = soItem.ProductMaster_fk,
                Reserved = soItem.ProductMaster.GoodsReserved,
                StockOnHand = soItem.ProductMaster.StockOnHand,
                Transit = soItem.ProductMaster.Transit,
                Quantity = soItem.Quantity,
                InventoryInWarehouse = (tt == null) ? 0 : tt.Inventory,
            });
        }

        private class InventoryProduct
        {
            public int IdProduct { get; set; }
            public int StockOnHand { get; set; }
            public int Reserved { get; set; }
            public int Transit { get; set; }
            public int Quantity { get; set; }
            public int? InventoryInWarehouse { get; set; }
        }

        public event EventHandler<EventArgs> BtnRecivePayment;
        private void BtnRecivePayment_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnRecivePayment != null)
                BtnRecivePayment(sender, e);
        }

        private void TxtHandling_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaleOrderViewModel.AllowToCalculate)
                ShowTaxes();
        }

        private void CmbTaxHandling_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SaleOrderViewModel.AllowToCalculate)
                ShowTaxes();
        }

        private void CmbTaxShipping_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SaleOrderViewModel.AllowToCalculate)
                ShowTaxes();
        }

        private void TxtShipping_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaleOrderViewModel.AllowToCalculate)
                ShowTaxes();
        }

        private void BtnRefund_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var SelectedForRefund = dgSoItems.SelectedItems.Cast<SoItemVeiwModel>().ToList();
            SaleOrderViewModel.Refund = new Refund() { Shipping = 0, RefundDate = DateTime.Now,TypeOfDiscount = false,DiscountAmount = 0};
            SaleOrderViewModel.Refund.TaxesRefunds = new List<TaxRefund>();
            SaleOrderViewModel.RefundItems = new ObservableCollection<RefundItemsViewModel>();
            foreach (var model in SelectedForRefund)
            {
                SaleOrderViewModel.RefundItems.Add(new RefundItemsViewModel()
                {
                    ProductMaster = model.ProductMaster,
                    AbleReturn = model.Quantity - model.QuantityRefunded,
                    TaxCode = model.TaxCode,
                    TaxCodeName = SaleOrderViewModel.TaxRates.FirstOrDefault(p => p.Id == model.TaxCode).Name,
                    Cost = model.Cost,
                    Price = model.Price
                });
            }
            cmbDiscountType.ItemsSource = new[]
            {
                new { Id = 1, Name = "Value" },
                new { Id = 2, Name = "Percent" }
            }.ToList();
            cmbPaymentMethodRefund.SelectedIndex = 1;
            cmbRefundFrom.SelectedIndex = 0;
            cmbDiscountType.SelectedIndex = 0;
            SaleOrderViewModel.IsRefund = true;
            GrdRefuand.Visibility = Visibility.Visible;
        }

        private void DgRefundItems_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            DataGridColumn col1 = e.Column;
            DataGridRow row1 = e.Row;
            // SoItem t = e.Row.DataContext as SoItem;
            string header = col1.Header as string;

            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            int col_index = col1.DisplayIndex;

            var ableToreturn = SaleOrderViewModel.RefundItems.ElementAt(row_index).AbleReturn;

            var qytStr = ((TextBox)e.EditingElement).Text;
            if (qytStr.Count(x => x == '.') > 0)
            {
                MessageBox.Show("Enter Integer Number Please");
                SaleOrderViewModel.RefundItems.ElementAt(row_index).Quantity = 0;
            }
            int Qyt = (!string.IsNullOrWhiteSpace(qytStr)) ? Convert.ToInt32(qytStr) : 0;
            if (ableToreturn < Qyt)
            {
                MessageBox.Show("It is more than Able to return");
                SaleOrderViewModel.RefundItems.ElementAt(row_index).Quantity=0;
            }
            else
            {
                if (Qyt < 0)
                    Qyt = Qyt * -1;

                SaleOrderViewModel.RefundItems.ElementAt(row_index).Quantity = Qyt;

                SumRefund();
            }

        }

        private void CmbDiscountType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SaleOrderViewModel.AllowToCalculate)
            {
                if (cmbDiscountType.SelectedIndex == 0)
                {
                    SaleOrderViewModel.Refund.TypeOfDiscount = false;
                }
                else
                {
                    SaleOrderViewModel.Refund.TypeOfDiscount = true;
                }

                SaleOrderViewModel.Refund.Discount = 0;
                SaleOrderViewModel.Refund.DiscountAmount = 0;
                SaleOrderViewModel.SumRefundPrice();
                ShowRefundTaxes();
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaleOrderViewModel.AllowToCalculate && SaleOrderViewModel.Refund.DiscountAmount!=0)
            {
                SaleOrderViewModel.SumRefundPrice();
                ShowRefundTaxes();
            }
        }
    }
}
