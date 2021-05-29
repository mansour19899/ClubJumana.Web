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
using BackupClubJummana.QuickBookModel;
using ClubJumana.Core.Services;
using OfficeOpenXml;
using Newtonsoft.Json;

namespace BackupClubJummana
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RepositoryService _repositoryService;
        private ProductInformationService _productInformationService;
        public MainWindow()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _productInformationService = new ProductInformationService();
        }

        private void BtnStartBackup_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Start");
            _repositoryService.UpdateLocalDb();
            MessageBox.Show("Finish");
        }

        private void BtnImportProduct_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
           // dlg.DefaultExt = ".png";
           // dlg.Filter = "Excel |*.xlsx"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                StreamReader r = new StreamReader(dlg.FileName);
                string jsonString = r.ReadToEnd();
                Example m = JsonConvert.DeserializeObject<Example>(jsonString);

                //String JSONtxt = File.ReadAllText(dlg.FileName);
                //var accounts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice>>(JSONtxt);


                //FileInfo existingFile = new FileInfo(dlg.FileName);
                //using (ExcelPackage package = new ExcelPackage(existingFile))
                //{
                //    //get the first worksheet in the workbook
                //    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                //    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                //    int rowCount = worksheet.Dimension.End.Row;     //get row count
                //    List<string> rrr = new List<string>();
                //    //for (int row = 1; row <= rowCount; row++)
                //    //{
                //    //    //for (int col = 1; col <= colCount; col++)
                //    //    //{
                //    //    //   rrr.Add(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value.ToString().Trim());

                //    //    //}
                //    //    try
                //    //    {
                //    //        AddItem(worksheet.Cells[row, 1].Value.ToString().Trim(), worksheet.Cells[row, 2].Value.ToString().Trim(), worksheet.Cells[row, 3].Value.ToString().Trim());
                //    //    }
                //    //    catch (Exception exception)
                //    //    {
                //    //        MessageBox.Show("Row " + row + " is Null");
                //    //    }

                //    //}
                //}
            }
        }
    }
}
