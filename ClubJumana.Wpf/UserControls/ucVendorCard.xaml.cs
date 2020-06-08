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

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucVendorCard.xaml
    /// </summary>
    public partial class ucVendorCard : UserControl
    {
        public ucVendorCard()
        {
            InitializeComponent();
        }

        private void BtnShowImageOfVendor_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            GridLength currentGridLength = new GridLength(0, GridUnitType.Pixel);

            if (ColumnOfImageVendor.Width == currentGridLength)
                ColumnOfImageVendor.Width = new GridLength(350, GridUnitType.Pixel);
            else
                ColumnOfImageVendor.Width = new GridLength(0, GridUnitType.Pixel);
        }
    }
}
