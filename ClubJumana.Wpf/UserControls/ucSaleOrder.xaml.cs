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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                      AddInventoryInformation(item);
                    }
                }

            }
        }

        public ucSaleOrder()
        {
            InitializeComponent();
            SaleOrderViewModel=new SaleOrderViewModel();
            inventoryProducts=new List<InventoryProduct>();
            RemoveSoItemViewModel=new List<SoItemVeiwModel>();
        }

        private void UcSaleOrder_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
        public event EventHandler<EventArgs> BtnSaveOnClick;
        private void BtnSaveSalesOrder_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnSaveOnClick != null)
                BtnSaveOnClick(sender, e);
        }

        public event EventHandler<EventArgs> BtnCloseSubPage;
        private void BtnCloseSubPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllowSearch = false;
            e.Handled = true;
            if (BtnCloseSubPage != null)
                BtnCloseSubPage(sender, e);
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
               BtnAddItem_OnClick(sender,e);
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
            RemoveSoItemViewModel.AddRange(dgSoItems.SelectedItems.Cast<SoItemVeiwModel>().ToList());
            foreach (var VARIABLE in RemoveSoItemViewModel)
            {
                VARIABLE.IsDeleted = true;
                SaleOrderViewModel.SoItems.Remove(VARIABLE);
                if (VARIABLE.Id == 0)
                    RemoveSoItemViewModel.Remove(VARIABLE);
                inventoryProducts.Remove(inventoryProducts.First(p=>p.IdProduct==VARIABLE.ProductMaster_fk));
            }
            dgSoItems.Items.Refresh();
            SumSoItemPrice();
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
                    inventoryProducts.ElementAt(row_index).Reserved-= inventoryProducts.ElementAt(row_index).Quantity;
                    inventoryProducts.ElementAt(row_index).Reserved+= Qyt;
                    inventoryProducts.ElementAt(row_index).Quantity= Qyt;
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

            SumSoItemPrice();
        }

        private void SumSoItemPrice()
        {
            SaleOrderViewModel.Subtotal = 0;
            SaleOrderViewModel.SoTotalPrice = 0;
            SaleOrderViewModel.TotalDiscount = 0;
            foreach (var item in SaleOrderViewModel.SoItems)
            {
                SaleOrderViewModel.Subtotal = item.TotalPrice + SaleOrderViewModel.Subtotal;
                SaleOrderViewModel.TotalDiscount = (item.Quantity * item.Price - item.TotalPrice) + SaleOrderViewModel.TotalDiscount;
            }
        }
        private void CmbTaxAreaSo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PrepareTaxitem(cmbTaxAreaSo.SelectedItem as Province);
        }

        private void PrepareTaxitem(Province province)
        {
            SaleOrderViewModel.TaxName = "";
            var taxrate = new List<decimal>();
            if (province != null)
            {
                if (province.HST != 0 && province.HST != null)
                {
                    SaleOrderViewModel.TaxName = SaleOrderViewModel.TaxName + "HST";
                    taxrate.Add(province.HST.Value);
                    cmbTaxAreaSo.ToolTip = SaleOrderViewModel.TaxName + " : " + (province.HST.Value * 100).ToString() + " %";
                }

                if (province.GST != 0 && province.GST != null)
                {
                    SaleOrderViewModel.TaxName = SaleOrderViewModel.TaxName + "GST";
                    taxrate.Add(province.GST.Value);
                    cmbTaxAreaSo.ToolTip = SaleOrderViewModel.TaxName + " : " + (province.GST.Value * 100).ToString() + " %";
                }

                if (province.QST != 0 && province.QST != null)
                {
                    SaleOrderViewModel.TaxName = SaleOrderViewModel.TaxName + ",QST";
                    taxrate.Add(province.QST.Value);
                    cmbTaxAreaSo.ToolTip = SaleOrderViewModel.TaxName + " : " + (province.GST.Value * 100).ToString() + " %  ," + (province.QST.Value * 100).ToString() + " %";
                }
                SaleOrderViewModel.TaxRate = taxrate;
            }


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
           TxtCustomerLookup_OnGotMouseCapture(null,null);
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
                TxtTermPercent_OnLostFocus(null,null);
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
                txtShowInventory.Text = "Stock On Hand: " +inventory.StockOnHand + "   Reserved: " +inventory.Reserved + "   Transit: " + inventory.Transit;
                txtShowInventory.FontSize = 20;
            }
        }

        public void AddInventoryInformation(SoItemVeiwModel soItem)
        {
            inventoryProducts.Add(new InventoryProduct()
            {
                IdProduct = soItem.ProductMaster_fk,
                Reserved = soItem.ProductMaster.GoodsReserved,
                StockOnHand = soItem.ProductMaster.StockOnHand,
                Transit = soItem.ProductMaster.Transit,
                Quantity = soItem.Quantity
            });
        }

        private class InventoryProduct
        {
            public int IdProduct { get; set; }
            public int StockOnHand { get; set; }
            public int Reserved { get; set; }
            public int Transit { get; set; }
            public int Quantity { get; set; }
        }

        private void BtnRecivePayment_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("hii Mansour");
        }
    }
}
