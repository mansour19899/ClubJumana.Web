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
        public GrnViewModel GrnViewModel { get; set; }





        public ucPurchasing()
        {
            InitializeComponent();
            
        }
        private void UcPurchasing_OnLoaded(object sender, RoutedEventArgs e)
        {
            GrnViewModel.ItemsOfPurchaseOrderViewModels.RemoveAt(0);
            this.DataContext = GrnViewModel;
        }

        public event EventHandler<EventArgs> BtnSaveOnClick;
        private void BtnSavePurchasing_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnSaveOnClick != null)
                BtnSaveOnClick(sender, e);
        }

        private void DgItems_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            decimal sum = 0;
            foreach (var VARIABLE in GrnViewModel.ItemsOfPurchaseOrderViewModels)
            {
                sum = VARIABLE.TotalItemPrice + sum;
            }

            GrnViewModel.TotalPrice = sum;

        }

   
    }
}
