using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using OfficeOpenXml;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucStockTake.xaml
    /// </summary>
    public partial class ucStockTake : UserControl
    {
        private ObservableCollection<ItemStockViewMode> itemsStockList;
        private List<ItemStockViewMode> NewItemsStockList;
        private int count = 0;
        public ucStockTake()
        {
            InitializeComponent();
            itemsStockList=new ObservableCollection<ItemStockViewMode>();
            NewItemsStockList=new List<ItemStockViewMode>();
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


        private void BtnExport_OnClick(object sender, RoutedEventArgs e)
        {
            var res = itemsStockList.GroupBy(x => x.Barcode);
            int Id = 0;
            NewItemsStockList.Clear();
            foreach (var item in res)
            {
                Id++;
                NewItemsStockList.Add(new ItemStockViewMode(){Id =Id,Barcode = item.Key,Quantity = item.Count()});
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Excel |*.xlsx"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {

                FileInfo existingFile = new FileInfo(dlg.FileName);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    List<string> rrr = new List<string>();
                    int Id = 0;
                    for (int row = 1; row <= rowCount; row++)
                    {
                        Id++;

                        itemsStockList.Add(new ItemStockViewMode() { Id = Id, Barcode = worksheet.Cells[row, 1].Value.ToString().Trim() });
                        dgItems.ItemsSource = itemsStockList.Reverse();
                        dgItems.Items.Refresh();
                    }
                }
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
