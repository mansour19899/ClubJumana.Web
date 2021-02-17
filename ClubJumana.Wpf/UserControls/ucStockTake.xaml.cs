using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using JetBrains.Annotations;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucStockTake.xaml
    /// </summary>
    public partial class ucStockTake : UserControl
    {
        private ObservableCollection<ItemStockViewMode> itemsStockList;
        private int count = 0;
        public ucStockTake()
        {
            InitializeComponent();
            itemsStockList=new ObservableCollection<ItemStockViewMode>();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UcStockTake_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void TxtInsertBarcode_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                count++;
               itemsStockList.Add(new ItemStockViewMode(){Id = count,Barcode = txtInsertBarcode.Text.Trim()});
                txtInsertBarcode.Clear();
                dgItems.ItemsSource = itemsStockList.Reverse();
                dgItems.Items.Refresh();
            }
        }


    }

    public class ItemStockViewMode : INotifyPropertyChanged
    {

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();

            }
        }

        private string _barcode;
        public string Barcode
        {
            get { return _barcode; }
            set
            {
                _barcode = value;
                OnPropertyChanged();

            }
        }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
