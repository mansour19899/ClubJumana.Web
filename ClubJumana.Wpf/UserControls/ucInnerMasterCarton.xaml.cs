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
        public MasterCarton MasterSelected;
        public Inner InnerSelected;
        public Inner InnerSelectedOfMaster;
        public ucInnerMasterCarton()
        {
            InitializeComponent();
            innerMasterCartons = new List<InnerMasterCarton>();
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

        public bool SetNumeric(object sender, KeyEventArgs e,bool CanHaveOemPeriod =false)
        {
            bool result = false;

            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal) || (e.Key == Key.Tab))
            { result = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.OemPeriod))
            { result = false; }
            else
            { result = true; }

            if (e.Key == Key.OemPeriod&&!CanHaveOemPeriod)
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
            ClearDimensionMaserCarton();
            e.Handled = true;
            if (CheckExistITF14Master != null)
                CheckExistITF14Master(sender, e);
        }

        private void ClearDimensionMaserCarton()
        {
            txtLengthMaster.Clear();
            txtWidthMaster.Clear();
            txtHeightMaster.Clear();
            txtWeightMaster.Clear();
            btnUpdateMasterCarton.IsEnabled = false;
        }

        private void TxtMasterITF14_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtWeightMaster.Focus();
            }
            else
            {
                e.Handled = SetNumeric(sender, e);
            }

        }

        private void BtnShowInnerPage_OnClick(object sender, RoutedEventArgs e)
        {
            txtHeader.Text = "Inner";
            InnerPage.Visibility = Visibility.Visible;
            MasterPage.Visibility = Visibility.Hidden;
            SearchPage.Visibility = Visibility.Hidden;
        }

        public void ShowInnerPage()
        {
            BtnShowInnerPage_OnClick(null,null);
        }

        private void BtnShowMasterPage_OnClick(object sender, RoutedEventArgs e)
        {
            txtHeader.Text = "Master Carton";
            MasterPage.Visibility = Visibility.Visible;
            SearchPage.Visibility = Visibility.Hidden;
        }
        private void BtnShowSearch_OnClick(object sender, RoutedEventArgs e)
        {
            txtHeader.Text = "";
            SearchPage.Visibility = Visibility.Visible;
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
            InnerSelected = inner;
            txtInnerName.Text = inner.ProductMaster.Name;
            txtInnerUPC.Text = inner.ProductMaster.UPC;
            txtInnerSKU.Text = inner.ProductMaster.SKU;
            txtInnerQuantityPerInner.Text = inner.Quantity.ToString();
            txtUom.Text = inner.ProductMaster.Uom.Name;
            lvMasterCartonInnerSearch.ItemsSource = inner.InnerMasterCartons;
            lvMasterCartonInnerSearch.Items.Refresh();
            MasterCartonSearch.Visibility = Visibility.Hidden;
            InnerSearch.Visibility = Visibility.Visible;
            //txtTypeSearch.Foreground=

        }
        public void ShowMaster(MasterCarton master)
        {
            txtTypeSearch.Text = "Master Carton";
            MasterSelected = master;
            txtPcsPerCarton.Text = master.TotalQuantity.ToString();
            txtITF14Master.Text = master.ITF14;
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
            if (master.InnerMasterCartons.Count != 0)
                ShowInnerOfMaster(master.InnerMasterCartons.ElementAt(0).Inner);
            MasterCartonSearch.Visibility = Visibility.Visible;
            InnerSearch.Visibility = Visibility.Hidden;
            // lvInnersOfMaster.ItemsSource = master.InnerMasterCartons;

            //txtTypeSearch.Foreground=

        }


        public void ShowInnerOfMaster(Inner inner)
        {
            InnerSelectedOfMaster = inner;
            txtInnerITF142.Text = inner.ITF14;
            txtInnerName2.Text = inner.ProductMaster.Name;
            txtInnerUPC2.Text = inner.ProductMaster.UPC;
            txtInnerSKU2.Text = inner.ProductMaster.SKU;
            txtInnerQuantityPerInner2.Text = inner.Quantity.ToString();
            txtUom2.Text = inner.ProductMaster.Uom.Name;
            int QuantityInnerinMaster = inner.InnerMasterCartons
                .FirstOrDefault(p => p.MasterCartonFK == MasterSelected.Id).InnerQuntity;
            txtPackMaster.Text =QuantityInnerinMaster+"/"+MasterSelected.TotalQuantity ;
        }
        private void LvMasterCartonInnerSearch_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (InnerMasterCarton)lvMasterCartonInnerSearch.ItemContainerGenerator.ItemFromContainer(dep);
            txtSearchITF.Text = wer.MasterCarton.ITF14.Trim();
            BtnSearch_OnClick(sender,e);
            //txtSearchITF.Text=
        }

        private void LvInnersOfMaster_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = lvInnersOfMaster.SelectedItem as Inner;
            if (x != null)
                ShowInnerOfMaster(x);
        }

        private void TxtSearchITF_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_OnClick(sender, e);
                txtSearchITF.Clear();
            }
        }


        private void LvInnersOfMaster_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            var wer = (Inner)lvInnersOfMaster.ItemContainerGenerator.ItemFromContainer(dep);

            txtSearchITF.Text = wer.ITF14.Trim();
            BtnSearch_OnClick(sender, e);

        }
        public event EventHandler<EventArgs> BtnFindProductForInner;
        private void BtnFindProductForInner_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnFindProductForInner != null)
                BtnFindProductForInner(sender, e);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var row = (Inner) dgInners.SelectedItem;
            Clipboard.SetText(row.ITF14.Trim());
        }

        private void ConvertCmToInech_OnClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtWeightMaster.Text))
                txtWeightMaster.Text = (Math.Round(Convert.ToDouble(txtWeightMaster.Text) * 2.205, 1, MidpointRounding.AwayFromZero)).ToString();
            if (!String.IsNullOrWhiteSpace(txtLengthMaster.Text))
                txtLengthMaster.Text = (Math.Round(Convert.ToDouble(txtLengthMaster.Text) / 2.54,1, MidpointRounding.AwayFromZero)).ToString();
            if (!String.IsNullOrWhiteSpace(txtWidthMaster.Text))
                txtWidthMaster.Text = (Math.Round(Convert.ToDouble(txtWidthMaster.Text) / 2.54,1, MidpointRounding.AwayFromZero)).ToString();
            if (!String.IsNullOrWhiteSpace(txtHeightMaster.Text))
                txtHeightMaster.Text = (Math.Round(Convert.ToDouble(txtHeightMaster.Text) / 2.54,1, MidpointRounding.AwayFromZero)).ToString();

        }

        private void TxtWeightMaster_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e,true);
        }

        private void TxtLengthMaster_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e,true);
        }

        private void TxtWidthMaster_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e,true);
        }

        private void TxtHeightMaster_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e,true);
        }

        public event EventHandler<EventArgs> LoadMasterCarton;
        private void ImgDuplicateITFMaster_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (LoadMasterCarton != null)
                LoadMasterCarton(sender, e);
        }

        public event EventHandler<EventArgs> UpdateMasterCarton;
        private void BtnUpdateMasterCarton_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (UpdateMasterCarton != null)
                UpdateMasterCarton(sender, e);
        }
    }
}
