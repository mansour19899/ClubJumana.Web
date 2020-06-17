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
        private Test tt;
        public Dashboard()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            //TestBorder.Child=new ucTest();

             tt=new Test();
            tt.Customer = _repositoryService.AllCustomers().FirstOrDefault();
            tt.Vendor = _repositoryService.AllVendor().FirstOrDefault();
            tt.ProductMaster = _repositoryService.AllProductMasterList().FirstOrDefault();
            
            var t= new ucCustomerCard();
            var yy=new ucVendorCard();
            var yyy=new ucItemCard();
            var yyyy=new ucPurchasing();

            yy.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            yyy.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            t.BtnSaveOnClick += BtnSaveForCustomer_OnBtnSaveOnClick;

            DataContext = tt;
            t.DataContext = tt.Customer;
            yy.DataContext = tt.Vendor;
            yyy.DataContext = tt.ProductMaster;
            Bordermanagement.Child = yyyy ;


        }

        private void BtnSaveForCustomer_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            MessageBox.Show(tt.Customer.FirstName);
            _repositoryService.AddAndUpdateCustomer(tt.Customer);
        }

        private void BtnSaveForVendor_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            MessageBox.Show(tt.Customer.FirstName);
            _repositoryService.AddAndUpdateVendor(tt.Vendor);
        }
        private void BtnSaveForItem_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            MessageBox.Show(tt.Customer.FirstName);
            _repositoryService.AddAndUpdateItem(tt.ProductMaster);
        }
    }

    public class Test
    {
        public Customer Customer { get; set; }
        public Vendor Vendor { get; set; }
        public ProductMaster ProductMaster { get; set; }
    }
}
