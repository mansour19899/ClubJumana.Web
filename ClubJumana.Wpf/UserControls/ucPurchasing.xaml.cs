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
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucPurchasing.xaml
    /// </summary>
    public partial class ucPurchasing : UserControl
    {
        public List<Vendor> vendors;
        public List<Warehouse> Warehouses;
        public ObservableCollection<ItemsOfPurchaseOrderViewModel> ItemsOfPurchaseOrderViewModels { get; set; }
        public PoViewModel PoViewModel { get; set; }
        public AsnViewModel AsnViewModel { get; set; }
        private GrnViewModel _grnViewModel;

        public GrnViewModel GrnViewModel
        {
            get
            {
                return _grnViewModel;
            }
            set
            {
                _grnViewModel = value;
            }
        }

        public Mode Mode = Mode.Nothong;
        public bool IsPosting = false;
        public List<ItemsOfPurchaseOrderViewModel> RemoveItemsOfPurchaseOrderViewModel;



        public ucPurchasing()
        {
            InitializeComponent();
            
        }
        private void UcPurchasing_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        public void ParaperForNewPurchasing()
        {
            RemoveItemsOfPurchaseOrderViewModel = new List<ItemsOfPurchaseOrderViewModel>();

            switch (Mode)
            {
                case Mode.PO:
                    this.DataContext = PoViewModel;
                    btnSavePurchasing.Visibility = Visibility.Visible;
                    break;
                case Mode.Asn:
                    this.DataContext = AsnViewModel;
                    btnSavePurchasing.Visibility = Visibility.Visible;

                    break;
                case Mode.Grn:
                    this.DataContext = GrnViewModel;
                    btnSavePurchasing.Visibility = Visibility.Visible;
                    break;
                case Mode.POInvoice:
                    this.DataContext = PoViewModel;
                    btnSavePurchasing.Visibility = Visibility.Hidden;
                    break;
                case Mode.AsnInvoice:
                    this.DataContext = AsnViewModel;
                    btnSavePurchasing.Visibility = Visibility.Hidden;
                    break;
                case Mode.GrnInvoice:
                    this.DataContext = GrnViewModel;
                    btnSavePurchasing.Visibility = Visibility.Hidden;
                    break;
            }

            switch (Mode)
            {
                case Mode.PO:
                    dgItems.Columns[6].Header = "Quantity (PO)";
                    dgItems.Columns[5].Header = "Previous Quantity";
                    break;
                case Mode.Asn:
                    dgItems.Columns[6].Header = "Quantity (GIT)";
                    dgItems.Columns[5].Header = "Quantity (PO)";
                    break;
                case Mode.Grn:
                    dgItems.Columns[6].Header = "Quantity (GRN)";
                    dgItems.Columns[5].Header = "Quantity (GIT)";
                    break;
            }


        }

        public event EventHandler<EventArgs> BtnAddItemOnClick;
        private void BtnAddItem_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (btnAddItem != null)
                BtnAddItemOnClick(sender, e);
        }

        public event EventHandler<EventArgs> BtnSaveOnClick;
        private void BtnSavePurchasing_OnClick(object sender, RoutedEventArgs e)
        {
            IsPosting = false;
            e.Handled = true;
            if (BtnSaveOnClick != null)
                BtnSaveOnClick(sender, e);
        }
        public event EventHandler<EventArgs> BtnPostPurchasing;
        private void BtnPostPurchasing_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsPosting = true;
            e.Handled = true;
            if (BtnPostPurchasing != null)
                BtnPostPurchasing(sender, e);
        }

        public event EventHandler<EventArgs> BtnCloseSubPage;
        private void BtnCloseSubPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnCloseSubPage != null)
                BtnCloseSubPage(sender, e);
        }

        private void DgItems_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            decimal sum = 0;
            DataGridColumn col1 = e.Column;
            //DataGridRow row1 = e.Row;
            //ItemsOfPurchaseOrderViewModel t = e.Row.DataContext as ItemsOfPurchaseOrderViewModel;
            //TextBox tt = e.EditingElement as TextBox;  // Assumes columns are all TextBoxes
            //DataGridColumn dgc = e.Column;
            SumItemsprice();
            //string header = col1.Header as string;

            //if (header.CompareTo("UPC") == 0)
            //{
            //    //MessageBox.Show(tt.Text);
            //    //t.ProductMaster_fk = 1;
            //}
            //else
            //{
            //    switch (Mode)
            //    {
            //        case Mode.PO:
            //            foreach (var VARIABLE in PoViewModel.ItemsOfPurchaseOrderViewModels)
            //            {
            //                sum = VARIABLE.TotalItemPrice + sum;
            //            }
            //            PoViewModel.SubtotalPrice = sum;
            //            break;
            //        case Mode.Asn:
            //            foreach (var VARIABLE in AsnViewModel.ItemsOfPurchaseOrderViewModels)
            //            {
            //                sum = VARIABLE.TotalItemPrice + sum;
            //            }
            //            AsnViewModel.SubtotalPrice = sum;
            //            break;
            //        case Mode.Grn:
            //            foreach (var VARIABLE in GrnViewModel.ItemsOfPurchaseOrderViewModels)
            //            {
            //                sum = VARIABLE.TotalItemPrice + sum;
            //            }
            //            GrnViewModel.SubtotalPrice = sum;
            //            break;
            //    }
            //}
            
        }

        public void SumItemsprice()
        {
            decimal sum = 0;
            switch (Mode)
            {
                case Mode.PO:
                    foreach (var VARIABLE in PoViewModel.ItemsOfPurchaseOrderViewModels)
                    {
                        sum = VARIABLE.TotalItemPrice + sum;
                    }
                    PoViewModel.SubtotalPrice = sum;
                    break;
                case Mode.Asn:
                    foreach (var VARIABLE in AsnViewModel.ItemsOfPurchaseOrderViewModels)
                    {
                        sum = VARIABLE.TotalItemPrice + sum;
                    }
                    AsnViewModel.SubtotalPrice = sum;
                    break;
                case Mode.Grn:
                    foreach (var VARIABLE in GrnViewModel.ItemsOfPurchaseOrderViewModels)
                    {
                        sum = VARIABLE.TotalItemPrice + sum;
                    }
                    GrnViewModel.SubtotalPrice = sum;
                    break;
            }
        }

        private void BtnCalculateCost_OnClick(object sender, RoutedEventArgs e)
        {
           CalculateCost();
        }

        public void CalculateCost()
        {
            AsnViewModel.CalculateCost();
        }

        private void BtnDeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            RemoveItemsOfPurchaseOrderViewModel.AddRange(dgItems.SelectedItems.Cast<ItemsOfPurchaseOrderViewModel>().ToList());

            switch (Mode)
            {
                case Mode.PO:
                    foreach (var VARIABLE in RemoveItemsOfPurchaseOrderViewModel)
                    {
                        VARIABLE.IsDeleted = true;
                        PoViewModel.ItemsOfPurchaseOrderViewModels.Remove(VARIABLE);
                    }
                    break;
                case Mode.Asn:
                    foreach (var VARIABLE in RemoveItemsOfPurchaseOrderViewModel)
                    {
                        VARIABLE.IsDeleted = true;
                        AsnViewModel.ItemsOfPurchaseOrderViewModels.Remove(VARIABLE);
                    }
                    break;
                case Mode.Grn:
                    break;
            }

            SumItemsprice();
        }

        private void UIElement_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }

        private void UIElement_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }



    }
}
