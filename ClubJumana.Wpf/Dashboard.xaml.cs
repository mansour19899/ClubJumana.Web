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
using System.Windows.Shapes;
using ClubJumana.Core.Services;
using ClubJumana.DataLayer.Entities;
using ClubJumana.Wpf.UserControls;

namespace ClubJumana.Wpf
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private RepositoryService _repositoryService;
        public Dashboard()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            //TestBorder.Child=new ucTest();
            var tt=new Test();
            tt.Customer = _repositoryService.AllCustomers().FirstOrDefault();


            DataContext = tt;
        }

        private void BtnChange_OnClick(object sender, RoutedEventArgs e)
        {
           //TestBorder.Child=new ucTest2();
        }

    }

    public class Test
    {
        public Customer Customer { get; set; }
        public Vendor Vendor { get; set; }
    }
}
