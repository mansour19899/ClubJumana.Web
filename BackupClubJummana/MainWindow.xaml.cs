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
using ClubJumana.DataLayer.Entities;
using OfficeOpenXml;
using Newtonsoft.Json;
using Customer = ClubJumana.DataLayer.Entities.Customer;


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
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                ImportCustomerList(dlg.FileName);
            }
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private async Task ImportCustomerList(string fileName)
        {
            await Task.Run(() =>
            {
                StreamReader r = new StreamReader(fileName);
                string jsonString = r.ReadToEnd();
                // QBInvoice m = JsonConvert.DeserializeObject<QBInvoice>(jsonString);

                QBCustomer mm = JsonConvert.DeserializeObject<QBCustomer>(jsonString);
                List<Customer> customerList = new List<Customer>();
                if (mm.QueryResponse.Customer.Count == 0)
                    MessageBox.Show("List Empty");
                else
                {
                    var step = 100 / (mm.QueryResponse.Customer.Count+4);
                    int i = 0;

                    foreach (var item in mm.QueryResponse.Customer)
                    {
                        customerList.Add(new Customer()
                        {
                            Id = Convert.ToInt32(item.Id),
                            ContactName = item.GivenName,
                            ContactLastName = item.FamilyName,
                            Phone1 = item.PrimaryPhone == null ? "" : item.PrimaryPhone.FreeFormNumber,
                            Email = item.PrimaryEmailAddr == null ? "" : item.PrimaryEmailAddr.Address,
                            City = item.BillAddr == null ? "" : item.BillAddr.City,
                            CompanyName = item.CompanyName == null ? "" : item.CompanyName,
                            PostalCode = item.BillAddr == null ? "" : item.BillAddr.PostalCode,
                            DisplayShipAddress = item.BillAddr == null ? "" : item.GivenName + " " + item.FamilyName + "\n" + item.BillAddr.Line1 + "\n" + item.BillAddr.City + ", " + item.BillAddr.CountrySubDivisionCode + " " +
                                                                              item.BillAddr.PostalCode + "\n" + item.BillAddr.Country,
                            BalanceDueLCY = Convert.ToDecimal(item.Balance),
                            BalanceLCY = Convert.ToDecimal(item.BalanceWithJobs),
                            Active = item.Active,
                            Note = item.Notes,
                            CreatedBy_fk = 1
                        });
                    }

                    foreach (var customer in customerList.OrderBy(p => p.Id))
                    {
                        _repositoryService.AddAndUpdateCustomer(customer, false);
                        i += step;
                        this.Dispatcher.Invoke(() => pbStatus.Value = i);
                    }
                    bool res = _repositoryService.SaveDatabase();
                    this.Dispatcher.Invoke(() => pbStatus.Value = 100);
                    this.Dispatcher.Invoke(() => pbStatus.Foreground = Brushes.DarkGreen);
                    if (!res)
                        MessageBox.Show("Error");

                }

            });

        }

        private void BtnImportCustomer_OnClick(object sender, RoutedEventArgs e)
        {
                        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // dlg.DefaultExt = ".png";
            // dlg.Filter = "Excel |*.xlsx"; //"Excel Files|(*.xlsx, *.xls)|*.xlsx;*.xls";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                ImportCustomerList(dlg.FileName);
            }
        }
    }
}
