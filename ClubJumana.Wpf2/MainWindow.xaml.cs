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
using ClubJumana.Core.Services;

namespace ClubJumana.Wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private RepositoryService _repositoryService;
        private ProductInformationService _productInformationService;
        private bool IsConnectToServer = true;
        private string ErrorConection = "";

        public MainWindow()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _productInformationService = new ProductInformationService();
            CheckConectionToServe();
            if (IsConnectToServer)
                btnSHowAddProduct.IsEnabled = true;
            else
                btnSHowAddProduct.IsEnabled = false;
        }

        private void BtnShowSearchProduct_OnClick(object sender, RoutedEventArgs e)
        {
            SearchProduct frm=new SearchProduct(IsConnectToServer);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void BtnSHowAddProduct_OnClick(object sender, RoutedEventArgs e)
        {
            AddProduct frm2=new AddProduct();
            this.Hide();
            frm2.ShowDialog();
            this.Show();
        }

        private void CheckConectionToServe()
        {
            if (CheckInternetConnection.IsConnectedToServer() == true)
            {
                _repositoryService.UpdateLocalDb();
                IsConnectToServer = true;
                ErrorConection = "";
                //txtMagicStyle.Foreground = new SolidColorBrush(Colors.Navy);
            }
            else
            {
                IsConnectToServer = false;
                if (CheckInternetConnection.IsConnectedToInternet() == true)
                    ErrorConection = "Mode is Offline.Please Check Server";
                else
                    ErrorConection = "Mode is Offline.Check your internet connection";
            }
        }
        private void ShowErrorMassegeToConectionInternet()
        {
            if (ErrorConection != "")
            {
                //txtMagicStyle.Foreground = new SolidColorBrush(Color.FromRgb(191, 155, 48));
                MessageBox.Show(ErrorConection);
            }

        }
        private async Task CheckImageForDownload()
        {
            await Task.Run(() =>
            {
                var ImageOfFTPList = _productInformationService.GiveCountOfImagesVariant();
                string Path = AppDomain.CurrentDomain.BaseDirectory + "Images\\VariantsImage\\";
                DirectoryInfo dir = new DirectoryInfo(Path);
                var ImageOfLocalList = dir.GetFiles();
                List<string> ImageNameListOfLocal = new List<string>();
                foreach (var VARIABLE in ImageOfLocalList)
                {
                    ImageNameListOfLocal.Add(VARIABLE.Name);
                }

                var ImagesNameForAdd = ImageOfFTPList.Except(ImageNameListOfLocal);
                var ImagesNameForDelete = ImageNameListOfLocal.Except(ImageOfFTPList);
                var CountImageOfLocal = ImageOfLocalList.Count();
                if (CountImageOfLocal != ImageOfFTPList.Count + 1)
                {
                    foreach (var VARIABLE in ImagesNameForAdd)
                    {
                        _repositoryService.DownloadFileFromFTP(System.IO.Path.Combine(Path, VARIABLE), "/VariantsImage");
                    }

                    foreach (var VARIABLE in ImagesNameForDelete)
                    {
                        if (VARIABLE.CompareTo("not-found.JPG") != 0)
                            File.Delete(System.IO.Path.Combine(Path, VARIABLE));
                    }
                }
            });
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ShowErrorMassegeToConectionInternet();
           // CheckImageForDownload();
        }
    }
}
