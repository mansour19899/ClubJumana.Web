using System;
using System.Collections.Generic;
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
using ClubJumana.DataLayer.Entities;
using ClubJumana.Core.Services;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucCustomerCard.xaml
    /// </summary>
    public partial class ucCustomerCard : UserControl
    {
        
        public ucCustomerCard()
        {
            InitializeComponent();
            
        }


        private void BtnShowImageOfCustomer_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            GridLength currentGridLength = new GridLength(0, GridUnitType.Pixel);

            if (ColumnOfImageCustomer.Width == currentGridLength)
                ColumnOfImageCustomer.Width = new GridLength(350, GridUnitType.Pixel);
            else
                ColumnOfImageCustomer.Width = new GridLength(0, GridUnitType.Pixel);
        }

        public event EventHandler<EventArgs> BtnSaveOnClick;
        public void BtnSaveCustomer_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnSaveOnClick != null)
                BtnSaveOnClick(sender, e);
        }
        public event EventHandler<EventArgs> BtnCloseSubPage;
        private void BtnCloseSubPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnCloseSubPage != null)
                BtnCloseSubPage(sender, e);
        }
    }
}
