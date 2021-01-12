﻿using System;
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
using ClubJumana.Core.Convertors;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucInnerMasterCarton.xaml
    /// </summary>
    public partial class ucInnerMasterCarton : UserControl
    {
        public ProductMaster productMaster;
        public List<InnerMasterCarton> innerMasterCartons;
        public ucInnerMasterCarton()
        {
            InitializeComponent();
            innerMasterCartons=new List<InnerMasterCarton>();
        }

        public event EventHandler<EventArgs> BtnAddInner;
        private void BtnAddInner_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnAddInner != null)
                BtnAddInner(sender, e);
        }
        public event EventHandler<EventArgs> BtnAddMaster;
        private void BtnAddMaster_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnAddMaster != null)
                BtnAddMaster(sender, e);
        }

        public event EventHandler<EventArgs> BtnCloseInnerPage;
        private void BtnCloseInnerPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnCloseInnerPage != null)
                BtnCloseInnerPage(sender, e);
        }

        public event EventHandler<EventArgs> CheckExistITF14;
        private void TxtInnerITF14_OnLostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (CheckExistITF14 != null)
                CheckExistITF14(sender, e);
        }

        private void TxtInnerITF14_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            imgDuplicateITF.Visibility = Visibility.Collapsed;
            imgApproval.Visibility = Visibility.Collapsed;
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

            if (e.Key == Key.OemPeriod)
            {
                result = true;
            }
            if (e.Key == Key.Decimal && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result = true;
            }
            return result;
        }
        private void TxtInnerITF14_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void TxtInnerQuantity_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        public event EventHandler<EventArgs> BtnAddInnerForMaster;
        private void BtnAddInnrForMaster_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnAddInnerForMaster != null)
                BtnAddInnerForMaster(sender, e);
        }

        private void TxtMasterITF14_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            imgDuplicateITFMaster.Visibility = Visibility.Collapsed;
            imgApprovalMaster.Visibility = Visibility.Collapsed;
        }

        public event EventHandler<EventArgs> CheckExistITF14Master;
        private void TxtMasterITF14_OnLostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (CheckExistITF14Master != null)
                CheckExistITF14Master(sender, e);
        }

        private void TxtMasterITF14_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e);
        }

        private void BtnShowInnerPage_OnClick(object sender, RoutedEventArgs e)
        {
            MasterPage.Visibility = Visibility.Hidden;
        }

        private void BtnShowMasterPage_OnClick(object sender, RoutedEventArgs e)
        {
            MasterPage.Visibility = Visibility.Visible;
        }
        public event EventHandler<EventArgs> SearchITF;
        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (SearchITF != null)
                SearchITF(sender, e);
        }

        public void ShowInner(Inner inner)
        {
            txtTypeSearch.Text = "Inner";
            txtInnerName.Text = inner.ProductMaster.Name;
            txtInnerUPC.Text = inner.ProductMaster.UPC;
            txtInnerSKU.Text = inner.ProductMaster.SKU;
            txtInnerQuantityPerInner.Text = inner.Quantity.ToString();
            lvMasterCartonInnerSearch.ItemsSource = inner.InnerMasterCartons;
            lvMasterCartonInnerSearch.Items .Refresh();
            MasterCartonSearch.Visibility = Visibility.Hidden;
            InnerSearch.Visibility = Visibility.Visible;
            //txtTypeSearch.Foreground=

        }
        public void ShowMaster(MasterCarton master)
        {
            txtTypeSearch.Text = "Master Carton";
            txtPcsPerCarton.Text = master.TotalQuantity.ToString();
            txtLenghtCTN.Text = master.Lenght.ToString();
            txtWeightCTN.Text = master.Weight.ToString();
            txtWidthCTN.Text = master.Width.Value.RoundNumtoStr();
            txtHeightCTN.Text = master.Height.ToString();
            lvInnersOfMaster.Items.Clear();
            lvInnersOfMaster.Items.Refresh();
            foreach (var item in master.InnerMasterCartons)
            {
                lvInnersOfMaster.Items.Add(item.Inner);
            }
            lvInnersOfMaster.Items.Refresh();
            if(master.InnerMasterCartons.Count!=0)
                ShowInnerOfMaster(master.InnerMasterCartons.ElementAt(0).Inner);
            MasterCartonSearch.Visibility = Visibility.Visible;
            InnerSearch.Visibility = Visibility.Hidden;
            // lvInnersOfMaster.ItemsSource = master.InnerMasterCartons;

            //txtTypeSearch.Foreground=

        }

        public void ShowInnerOfMaster(Inner inner)
        {
            txtInnerITF142.Text = inner.ITF14;
            txtInnerName2.Text = inner.ProductMaster.Name;
            txtInnerUPC2.Text = inner.ProductMaster.UPC;
            txtInnerSKU2.Text = inner.ProductMaster.SKU;
            txtInnerQuantityPerInner2.Text = inner.Quantity.ToString();
        }
        private void LvMasterCartonInnerSearch_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LvInnersOfMaster_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowInnerOfMaster(lvInnersOfMaster.SelectedItem as Inner);
        }

        private void TxtSearchITF_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_OnClick(sender,e);
            }
        }
    }
}
