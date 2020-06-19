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
using System.Windows.Shapes;
using ClubJumana.Core.Convertors;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Enums;
using ClubJumana.Core.Services;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Entities;
using ClubJumana.DataLayer.Entities.Users;
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
        private UserService _userService;
        private PurchaseOrder SelectedPurchaseOrder;
        private PoViewModel SelectedPo;
        private AsnViewModel SelectedAsn;
        private GrnViewModel SelectedGrn;
        private List<Vendor> vendors;
        private List<Warehouse> warehouses;
        public Mode Mode = Mode.Nothong;
        public static User user;

        public ucPurchasing Purchasing;
        private Test tt;
        public Dashboard()
        {
            InitializeComponent();

            _repositoryService = new RepositoryService();
            _purchaseOrderService = new PerchaseOrderService();
            _userService = new UserService();

            user = _userService.LoginUser();
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
           
            Purchasing.CalculateCost();
            SaveAndUpdate();
            
        }

        void SaveAndUpdate(bool done = false)
        {
            string message = "";
            bool IsSuccessSavedOrUpdated = false;
            Mode = Mode.Asn;
            switch (Mode)
            {
                case Mode.PO:
                    SelectedPo.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdatePoViewModel(SelectedPo, SelectedPo.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Asn:
                    SelectedAsn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdateAsnViewModel(SelectedAsn, SelectedAsn.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
                case Mode.Grn:
                    SelectedGrn.ApproveUser_fk = user.Id;
                    IsSuccessSavedOrUpdated = _purchaseOrderService.AddOrUpdateGrnViewModel(SelectedGrn, SelectedGrn.ItemsOfPurchaseOrderViewModels.Concat(Purchasing.RemoveItemsOfPurchaseOrderViewModel), done);
                    break;
            }

            if (IsSuccessSavedOrUpdated)
            {
                if (done)
                {
                    message = "Purchase Order Done";
                }
                else if (SelectedAsn != null)
                {

                    if (SelectedAsn.Id == 0)
                        message = "Goods Transit Saved";

                }
                else if (SelectedPo != null)
                {
                    if (SelectedPo.Id == 0)
                        message = "Purchase Order Saved";
                }
                else
                {
                    message = "Purchase Order Updated";
                }

                //myMessageQueue.Enqueue(message);
            }


        }

        private void BtnAddItem_OnClick(object? sender, EventArgs e)
        {
            var ttttr = Purchasing.txtSearch.Text;
            var SearchMasterProduct = _repositoryService.GiveMeProductMasterByUPC(ttttr);
            if (SearchMasterProduct == null)
            {
                MessageBox.Show("Item Not Found");
            }
            else
            {
                SelectedAsn.ItemsOfPurchaseOrderViewModels.Add(new ItemsOfPurchaseOrderViewModel()
                {
                    Price = SearchMasterProduct.FobPrice.Value,
                    ProductMaster = SearchMasterProduct,
                    ProductMaster_fk = SearchMasterProduct.Id
                });

            }


            var ttt = SelectedAsn.ItemsOfPurchaseOrderViewModels;

        }

        private void Dashboard_OnLoaded(object sender, RoutedEventArgs e)
        {
           var convertor = new PurchaseOrderConvertor();
           // SelectedPurchaseOrder = _purchaseOrderService.GivePurchaseOrderById(9);
            SelectedPurchaseOrder=new PurchaseOrder(){};
            SelectedPurchaseOrder.Items=new List<Item>();
            SelectedPo = convertor.ConvertToPO(SelectedPurchaseOrder, vendors, warehouses);
            SelectedAsn = convertor.ConvertToAsn(SelectedPurchaseOrder, vendors, warehouses);
            SelectedGrn = convertor.ConvertToGrn(SelectedPurchaseOrder, vendors, warehouses);

            Purchasing = new ucPurchasing();
            Purchasing.BtnSaveOnClick += BtnSavePurchasing_OnBtnSaveOnClick;
            Purchasing.BtnAddItemOnClick += BtnAddItem_OnClick;
            //Purchasing.ItemsOfPurchaseOrderViewModels = SelectedGrn.ItemsOfPurchaseOrderViewModels;
            Purchasing.PoViewModel = SelectedPo;
            Purchasing.AsnViewModel = SelectedAsn;
            Purchasing.GrnViewModel = SelectedGrn;
            Purchasing.Mode = Mode.Asn;
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
