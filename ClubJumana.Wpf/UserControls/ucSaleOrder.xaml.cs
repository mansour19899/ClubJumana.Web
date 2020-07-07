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
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucSaleOrder.xaml
    /// </summary>
    public partial class ucSaleOrder : UserControl
    {
        private SaleOrderViewModel _saleOrderViewModel;

        public SaleOrderViewModel SaleOrderViewModel
        {
            get { return _saleOrderViewModel; }
            set
            {
                _saleOrderViewModel = value;
                DataContext = SaleOrderViewModel;
            }
        }

        public ucSaleOrder()
        {
            InitializeComponent();
            SaleOrderViewModel=new SaleOrderViewModel();
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
            e.Handled = true;
            if (BtnCloseSubPage != null)
                BtnCloseSubPage(sender, e);
        }

        private void BtnPostSalesOrder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnPrintOrSend_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TxtSearch_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnAddItemOnClick(sender, e);
            }
        }

        public event EventHandler<EventArgs> BtnAddItemOnClick;
        private void BtnAddItem_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (btnAddItem != null)
                BtnAddItemOnClick(sender, e);
        }

        private void BtnDeleteItem_OnClick(object sender, RoutedEventArgs e)
        {

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
    }
}
