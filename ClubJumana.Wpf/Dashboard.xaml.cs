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
using ClubJumana.Core.Convertors;
using ClubJumana.Core.DTOs;
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
        private PerchaseOrderService _purchaseOrderService;
        private PurchaseOrder SelectedPurchaseOrder;
        private PoViewModel SelectedPo;
        private AsnViewModel SelectedAsn;
        private GrnViewModel SelectedGrn;
        private List<Vendor> vendors;
        private List<Warehouse> warehouses;

        public ucPurchasing Purchasing;
        private Test tt;
        public Dashboard()
        {
            InitializeComponent();
            _repositoryService = new RepositoryService();
            _purchaseOrderService = new PerchaseOrderService();
            //TestBorder.Child=new ucTest();

            tt =new Test();
            tt.Customer = _repositoryService.AllCustomers().FirstOrDefault();
            tt.Vendor = _repositoryService.AllVendor().FirstOrDefault();
            tt.ProductMaster = _repositoryService.AllProductMasterList().FirstOrDefault();
            tt.PurchaseOrder = _repositoryService.AllPurchaseOrder().FirstOrDefault();

            var customerCard= new ucCustomerCard();
            var vendorCard=new ucVendorCard();
            var itemCard=new ucItemCard();


            vendorCard.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            itemCard.BtnSaveOnClick += BtnSaveForVendor_OnBtnSaveOnClick;
            customerCard.BtnSaveOnClick += BtnSaveForCustomer_OnBtnSaveOnClick;


            DataContext = tt;
            customerCard.DataContext = tt.Customer;
            vendorCard.DataContext = tt.Vendor;
            itemCard.DataContext = tt.ProductMaster;


            vendors = _repositoryService.AllVendor().ToList();
            warehouses = _repositoryService.AllWarehouse().ToList();
          


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
        private void BtnSavePurchasing_OnBtnSaveOnClick(object? sender, EventArgs e)
        {
            var ttt = SelectedGrn.ItemsOfPurchaseOrderViewModels;

        }

        private void Dashboard_OnLoaded(object sender, RoutedEventArgs e)
        {
           var convertor = new PurchaseOrderConvertor();
            SelectedPurchaseOrder = _purchaseOrderService.GivePurchaseOrderById(6);
            SelectedPo = convertor.ConvertToPO(SelectedPurchaseOrder, vendors, warehouses);
            SelectedAsn = convertor.ConvertToAsn(SelectedPurchaseOrder, vendors, warehouses);
            SelectedGrn = convertor.ConvertToGrn(SelectedPurchaseOrder, vendors, warehouses);

            Purchasing = new ucPurchasing();
            Purchasing.BtnSaveOnClick += BtnSavePurchasing_OnBtnSaveOnClick;
            //Purchasing.ItemsOfPurchaseOrderViewModels = SelectedGrn.ItemsOfPurchaseOrderViewModels;
            Purchasing.GrnViewModel = SelectedGrn;
            //Purchasing.DataContext = SelectedGrn;
            Bordermanagement.Child = Purchasing;
        }
    }

    public class Test
    {
        public Customer Customer { get; set; }
        public Vendor Vendor { get; set; }
        public ProductMaster ProductMaster { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public PoViewModel PoViewModel { get; set; }
    }
}
